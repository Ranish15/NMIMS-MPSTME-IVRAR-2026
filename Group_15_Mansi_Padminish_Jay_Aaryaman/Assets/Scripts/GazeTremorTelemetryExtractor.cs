using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.AcrophobiaTherapy
{
    /// <summary>
    /// Gaze Avoidance and Head Tremor Telemetry Extractor for Acrophobia VRET.
    /// Analyzes eye-gaze pitch direction and micro-tremor spectral power (4-10 Hz)
    /// from headset rotational telemetry to estimate physiological distress and height vertigo.
    /// 
    /// Student Roles:
    /// - B124 Jay Gandhi (Gaze & Head Tremor Telemetry Specialist): Gaze vector decomposition, tremor bandpass filtering, and PSD calculation
    /// - B130 Aaryaman Gehani (Human Factors & Clinical Usability Lead): SUDS score correlation, clinical CSV telemetry export, and habituation curves
    /// </summary>
    public class GazeTremorTelemetryExtractor : MonoBehaviour
    {
        [Header("Hardware Sensor Inputs")]
        [SerializeField] private Transform hmdCameraTransform;
        [SerializeField] private BioAdaptiveExposureController exposureController;

        [Header("Gaze Avoidance Tuning")]
        [SerializeField] private float downwardGazePitchThresholdDeg = -25.0f; // Looking down over balcony edge
        [SerializeField] private float samplingIntervalSeconds = 0.05f;       // 20 Hz telemetry extraction

        [Header("Tremor Analysis Window")]
        [SerializeField] private int tremorBufferCapacity = 64; // Sliding window for frequency analysis

        // Runtime telemetry metrics
        private Queue<float> angularVelocityPitchHistory = new Queue<float>();
        private float totalObservationTime = 0f;
        private float totalGazeAvoidanceTime = 0f;
        private float currentGazeAvoidanceRatio = 0f;
        private float currentTremorPowerDensity = 0f;
        private float timeSinceLastSample = 0f;

        private Quaternion previousRotation;

        private void Start()
        {
            if (hmdCameraTransform == null && Camera.main != null)
            {
                hmdCameraTransform = Camera.main.transform;
            }
            if (hmdCameraTransform != null)
            {
                previousRotation = hmdCameraTransform.rotation;
            }
        }

        private void Update()
        {
            timeSinceLastSample += Time.deltaTime;
            if (timeSinceLastSample >= samplingIntervalSeconds)
            {
                SampleKinematicsAndGaze(timeSinceLastSample);
                timeSinceLastSample = 0f;
            }
        }

        private void SampleKinematicsAndGaze(float dt)
        {
            if (hmdCameraTransform == null) return;

            totalObservationTime += dt;

            // 1. Gaze Direction & Downward Inspection
            float pitchAngle = hmdCameraTransform.eulerAngles.x;
            if (pitchAngle > 180f) pitchAngle -= 360f; // Convert [0, 360] to [-180, 180]

            // =========================================================================
            // TODO [B124 - Jay Gandhi - Gaze & Head Tremor Telemetry Specialist]:
            // 1. Calculate gaze avoidance condition: Patient avoiding looking down into the virtual abyss
            //    (pitchAngle > downwardGazePitchThresholdDeg indicates avoidance/looking up or eyes averted).
            // 2. Compute high-frequency head tremor power density by isolating 4-10 Hz angular oscillation.
            // 3. Normalize tremor PSD metric to [0.0, 1.0] relative to calibrated resting baseline.
            // =========================================================================

            bool isAvoidingDownwardGaze = pitchAngle > downwardGazePitchThresholdDeg;
            if (isAvoidingDownwardGaze)
            {
                totalGazeAvoidanceTime += dt;
            }
            currentGazeAvoidanceRatio = totalObservationTime > 0f ? (totalGazeAvoidanceTime / totalObservationTime) : 0f;

            // 2. Angular Velocity & Tremor Estimation
            Quaternion deltaRot = hmdCameraTransform.rotation * Quaternion.Inverse(previousRotation);
            deltaRot.ToAngleAxis(out float angleDeg, out Vector3 axis);
            float angularSpeedDegPerSec = (angleDeg / dt);
            previousRotation = hmdCameraTransform.rotation;

            angularVelocityPitchHistory.Enqueue(angularSpeedDegPerSec);
            if (angularVelocityPitchHistory.Count > tremorBufferCapacity)
            {
                angularVelocityPitchHistory.Dequeue();
            }

            // Estimate tremor power via root-mean-square of high-frequency variance
            float mean = 0f;
            foreach (var v in angularVelocityPitchHistory) mean += v;
            mean /= angularVelocityPitchHistory.Count;

            float variance = 0f;
            foreach (var v in angularVelocityPitchHistory) variance += (v - mean) * (v - mean);
            currentTremorPowerDensity = Mathf.Clamp01(Mathf.Sqrt(variance / angularVelocityPitchHistory.Count) / 15.0f);

            // Feed updated metrics to closed-loop bio-adaptive exposure controller
            if (exposureController != null)
            {
                exposureController.IngestStressMetrics(currentGazeAvoidanceRatio, currentTremorPowerDensity);
            }
        }

        public string GenerateClinicalTelemetryRow(string patientId, int sessionNumber, float reportedSudsScore)
        {
            // =========================================================================
            // TODO [B130 - Aaryaman Gehani - Human Factors & Clinical Usability Lead]:
            // 1. Correlate currentGazeAvoidanceRatio and currentTremorPowerDensity with clinical reportedSudsScore.
            // 2. Format clinical telemetry row: PatientID, SessionNum, Altitude, GazeAvoidance, TremorPSD, SUDS.
            // 3. Evaluate habituation trajectory against standard cognitive behavioral desensitization guidelines.
            // =========================================================================

            float altitude = exposureController != null ? transform.position.y : 0f;
            return $"{patientId},{sessionNumber},{altitude:F2},{currentGazeAvoidanceRatio:F3},{currentTremorPowerDensity:F3},{reportedSudsScore:F1}";
        }

        public float GetCurrentGazeAvoidanceRatio() => currentGazeAvoidanceRatio;
        public float GetCurrentTremorPowerDensity() => currentTremorPowerDensity;
    }
}
