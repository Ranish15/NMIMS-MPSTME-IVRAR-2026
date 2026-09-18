using System;
using UnityEngine;

namespace IVRAR.AcrophobiaTherapy
{
    /// <summary>
    /// Bio-Adaptive Closed-Loop Virtual Reality Exposure Therapy (VRET) Controller for Acrophobia.
    /// Dynamically modulates vertical environmental elevation (0m to 60m) based on real-time
    /// patient gaze avoidance behavior and physiological head tremor frequency.
    /// 
    /// Student Roles:
    /// - B011 Mansi Bansal (Bio-Adaptive State Machine Lead): Closed-loop state machine, habituation thresholds, and height modulation
    /// - B122 Padminish Bakshi (XR Systems Architect): Virtual glass platform rendering, vertical physics elevator, and skybridge rigging
    /// </summary>
    public class BioAdaptiveExposureController : MonoBehaviour
    {
        public enum ExposureState
        {
            BaselineCalibration,   // Ground level resting physiological baseline
            GradualAscent,         // Controlled upward elevation
            HabituationPlateau,    // Sustained altitude while anxiety decreases
            AcutePanicDescent,     // Controlled descent upon acute stress spike
            EmergencyGrounding     // Rapid return to ground plane upon patient distress
        }

        [Header("Elevation Rig Configuration")]
        [SerializeField] private Transform virtualElevatorPlatform;
        [SerializeField] private float minimumHeightMeters = 0.0f;
        [SerializeField] private float maximumHeightMeters = 60.0f;
        [SerializeField] private float baselineAscentRateMps = 0.5f;

        [Header("Bio-Adaptive Tuning Parameters")]
        [SerializeField] private float stressThresholdElevated = 0.65f; // Threshold triggering plateau
        [SerializeField] private float stressThresholdPanic = 0.85f;    // Threshold triggering descent
        [SerializeField] private float habituationSustainSeconds = 30.0f;

        [Header("Runtime Telemetry Status")]
        [SerializeField] private ExposureState currentExposureState = ExposureState.BaselineCalibration;
        [SerializeField] private float currentAltitudeMeters = 0.0f;
        [SerializeField] private float activeCompositeStressIndex = 0.0f;
        [SerializeField] private float plateauTimer = 0.0f;

        public event Action<ExposureState, float, float> OnExposureMetricsUpdated;

        private void Start()
        {
            if (virtualElevatorPlatform != null)
            {
                currentAltitudeMeters = virtualElevatorPlatform.position.y;
            }
        }

        private void Update()
        {
            UpdateExposureStateMachine();
            ApplyPlatformMovement();
        }

        public void IngestStressMetrics(float gazeAvoidanceRatio, float headTremorPowerDensity)
        {
            // =========================================================================
            // TODO [B011 - Mansi Bansal - Bio-Adaptive State Machine Lead]:
            // 1. Compute composite normalized stress index: S = w_gaze * R_avoid + w_tremor * P_tremor.
            // 2. Filter out transient coughs or intentional lookaways using exponential moving average.
            // 3. Modulate ascent rate: v_ascent = baselineRate * max(0.0, 1.0 - (S / stressThresholdElevated)).
            // =========================================================================

            activeCompositeStressIndex = Mathf.Clamp01(0.5f * gazeAvoidanceRatio + 0.5f * headTremorPowerDensity);
        }

        private void UpdateExposureStateMachine()
        {
            switch (currentExposureState)
            {
                case ExposureState.BaselineCalibration:
                    if (Time.timeSinceLevelLoad > 10.0f)
                    {
                        currentExposureState = ExposureState.GradualAscent;
                    }
                    break;

                case ExposureState.GradualAscent:
                    if (activeCompositeStressIndex >= stressThresholdPanic)
                    {
                        currentExposureState = ExposureState.AcutePanicDescent;
                    }
                    else if (activeCompositeStressIndex >= stressThresholdElevated)
                    {
                        currentExposureState = ExposureState.HabituationPlateau;
                        plateauTimer = 0.0f;
                    }
                    break;

                case ExposureState.HabituationPlateau:
                    plateauTimer += Time.deltaTime;
                    if (activeCompositeStressIndex >= stressThresholdPanic)
                    {
                        currentExposureState = ExposureState.AcutePanicDescent;
                    }
                    else if (activeCompositeStressIndex < stressThresholdElevated * 0.75f && plateauTimer >= habituationSustainSeconds)
                    {
                        // Patient has successfully habituated to current altitude
                        currentExposureState = ExposureState.GradualAscent;
                    }
                    break;

                case ExposureState.AcutePanicDescent:
                    if (activeCompositeStressIndex < stressThresholdElevated)
                    {
                        currentExposureState = ExposureState.HabituationPlateau;
                        plateauTimer = 0.0f;
                    }
                    break;

                case ExposureState.EmergencyGrounding:
                    // Rapidly descend to zero
                    break;
            }

            OnExposureMetricsUpdated?.Invoke(currentExposureState, currentAltitudeMeters, activeCompositeStressIndex);
        }

        private void ApplyPlatformMovement()
        {
            float targetVelocity = 0.0f;

            if (currentExposureState == ExposureState.GradualAscent)
            {
                targetVelocity = baselineAscentRateMps * (1.0f - activeCompositeStressIndex);
            }
            else if (currentExposureState == ExposureState.AcutePanicDescent)
            {
                targetVelocity = -baselineAscentRateMps * 1.5f;
            }
            else if (currentExposureState == ExposureState.EmergencyGrounding)
            {
                targetVelocity = -baselineAscentRateMps * 3.0f;
            }

            currentAltitudeMeters += targetVelocity * Time.deltaTime;
            currentAltitudeMeters = Mathf.Clamp(currentAltitudeMeters, minimumHeightMeters, maximumHeightMeters);

            // =========================================================================
            // TODO [B122 - Padminish Bakshi - XR Systems Architect]:
            // 1. Translate virtualElevatorPlatform along the vertical Y-axis smoothly.
            // 2. Adjust transparent glass floor opacity dynamically to enhance exposure fidelity.
            // 3. Modulate 3D spatial wind audio volume and distant city traffic sounds proportionally to currentAltitudeMeters.
            // =========================================================================

            if (virtualElevatorPlatform != null)
            {
                Vector3 pos = virtualElevatorPlatform.position;
                pos.y = currentAltitudeMeters;
                virtualElevatorPlatform.position = pos;
            }
        }

        public void TriggerEmergencyGrounding()
        {
            currentExposureState = ExposureState.EmergencyGrounding;
            Debug.LogWarning("[VRET] Emergency Grounding triggered by patient or clinician.");
        }
    }
}
