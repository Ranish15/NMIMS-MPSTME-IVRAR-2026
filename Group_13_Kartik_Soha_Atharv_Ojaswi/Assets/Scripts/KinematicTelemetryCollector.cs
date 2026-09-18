using System;
using System.IO;
using UnityEngine;

namespace IVRAR.Biometrics
{
    /// <summary>
    /// Collects and serializes high-frequency 90 Hz kinematic motion streams from the VR headset and hand controllers.
    /// Provides synthetic impostor replay injection to evaluate Equal Error Rates (EER) and false-acceptance vulnerabilities.
    /// 
    /// Student Roles:
    /// - I013 Atharv Dixit (Kinematic Telemetry Specialist): Stream ingestion, spatial normalization, and jerk derivatives
    /// - I019 Ojaswi Gondalia (Security QA & Threat Analyst): Impostor motion injection, adversarial replay, and EER benchmark logging
    /// </summary>
    public class KinematicTelemetryCollector : MonoBehaviour
    {
        [Header("Tracking Hardware Anchors")]
        [SerializeField] private Transform headHmdAnchor;
        [SerializeField] private Transform leftControllerAnchor;
        [SerializeField] private Transform rightControllerAnchor;

        [Header("Collection Parameters")]
        [SerializeField] private float sampleRateHz = 90.0f;
        [SerializeField] private bool isLoggingActive = false;
        [SerializeField] private bool injectImpostorSpoofStream = false;

        private ContinuousBiometricAuthManager authManager;
        private float sampleInterval;
        private float timeAccumulator = 0f;

        private void Awake()
        {
            sampleInterval = 1.0f / sampleRateHz;
            authManager = GetComponent<ContinuousBiometricAuthManager>();
        }

        private void Update()
        {
            if (!isLoggingActive) return;

            timeAccumulator += Time.deltaTime;
            while (timeAccumulator >= sampleInterval)
            {
                SampleAndDispatchFrame();
                timeAccumulator -= sampleInterval;
            }
        }

        private void SampleAndDispatchFrame()
        {
            if (headHmdAnchor == null || rightControllerAnchor == null) return;

            ContinuousBiometricAuthManager.KinematicFrame frame = new ContinuousBiometricAuthManager.KinematicFrame
            {
                timestamp = Time.time,
                headPosition = headHmdAnchor.position,
                headRotation = headHmdAnchor.rotation,
                leftHandPosition = leftControllerAnchor != null ? leftControllerAnchor.position : Vector3.zero,
                leftHandRotation = leftControllerAnchor != null ? leftControllerAnchor.rotation : Quaternion.identity,
                rightHandPosition = rightControllerAnchor.position,
                rightHandRotation = rightControllerAnchor.rotation
            };

            // =========================================================================
            // TODO [I013 - Atharv Dixit - Kinematic Telemetry Specialist]:
            // 1. Transform raw world-space coordinates into torso-relative normalized coordinates.
            // 2. Compute 1st, 2nd, and 3rd time derivatives (velocity, acceleration, jerk profile).
            // 3. Apply low-pass Butterworth filtering to suppress tracking sensor noise while preserving human motor style.
            // =========================================================================

            // =========================================================================
            // TODO [I019 - Ojaswi Gondalia - Security QA & Threat Analyst]:
            // 1. If injectImpostorSpoofStream is true, perturb frame kinematics with an adversarial replay trace.
            // 2. Measure elapsed latency between impostor stream onset and authManager lockout trigger.
            // 3. Export ground-truth classification labels and confidence scores to CSV for ROC/DET curve generation.
            // =========================================================================

            if (injectImpostorSpoofStream)
            {
                frame.headPosition += new Vector3(Mathf.Sin(Time.time * 4f) * 0.15f, 0f, 0f);
                frame.rightHandPosition += new Vector3(0f, Mathf.Cos(Time.time * 5f) * 0.25f, 0f);
            }

            if (authManager != null)
            {
                authManager.IngestKinematicFrame(frame);
            }
        }

        public void StartTelemetryLogging() => isLoggingActive = true;
        public void StopTelemetryLogging() => isLoggingActive = false;
        public void ToggleImpostorInjection(bool state) => injectImpostorSpoofStream = state;
    }
}
