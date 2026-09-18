using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.Biometrics
{
    /// <summary>
    /// Continuous Behavioral Biometric Authentication Manager for Collaborative VR.
    /// Analyzes continuous 6-DoF head and hand kinematic telemetry over sliding time windows
    /// to detect avatar identity spoofing and unauthorized headset transfers.
    /// 
    /// Student Roles:
    /// - I001 Kartik Agrawal (Biometric Authentication Lead): Feature extraction, classifier scoring, and EER thresholding
    /// - I007 Soha Chand (XR Systems Architect): Session state machine, avatar security lockout, and spatial warning UI
    /// </summary>
    public class ContinuousBiometricAuthManager : MonoBehaviour
    {
        public enum AuthenticationState
        {
            EnrollingBaseline,
            ContinuousVerified,
            AnomalyWarning,
            ImpostorLockedOut
        }

        [System.Serializable]
        public struct KinematicFrame
        {
            public float timestamp;
            public Vector3 headPosition;
            public Quaternion headRotation;
            public Vector3 leftHandPosition;
            public Quaternion leftHandRotation;
            public Vector3 rightHandPosition;
            public Quaternion rightHandRotation;
        }

        [Header("Telemetry Window Configuration")]
        [SerializeField] private float slidingWindowSeconds = 3.0f;
        [SerializeField] private float samplingFrequencyHz = 90.0f;
        [SerializeField] private float anomalyThresholdScore = 0.65f; // Distance threshold for impostor classification
        [SerializeField] private int consecutiveViolationsForLockout = 3;

        [Header("Live Authentication Status")]
        [SerializeField] private AuthenticationState currentState = AuthenticationState.EnrollingBaseline;
        [SerializeField] private float currentAnomalyScore = 0.0f;
        [SerializeField] private int activeViolationCount = 0;

        private Queue<KinematicFrame> slidingWindowBuffer = new Queue<KinematicFrame>();
        private float[] enrolledTemplateFeatureVector = null;

        public event Action<AuthenticationState, float> OnAuthenticationStateChanged;

        private void Start()
        {
            InitializeEnrolledBaseline();
        }

        private void InitializeEnrolledBaseline()
        {
            // Placeholder template feature vector: [mean_head_vel, mean_hand_vel, head_hand_dist, jerk_profile]
            enrolledTemplateFeatureVector = new float[] { 0.28f, 0.45f, 0.52f, 1.15f };
            currentState = AuthenticationState.ContinuousVerified;
            Debug.Log("[BiometricAuth] Baseline biometric template initialized. Continuous monitoring active.");
        }

        public void IngestKinematicFrame(KinematicFrame frame)
        {
            slidingWindowBuffer.Enqueue(frame);

            float windowDuration = (float)slidingWindowBuffer.Count / samplingFrequencyHz;
            if (windowDuration > slidingWindowSeconds)
            {
                slidingWindowBuffer.Dequeue();
                EvaluateWindowBiometrics();
            }
        }

        private void EvaluateWindowBiometrics()
        {
            if (currentState == AuthenticationState.ImpostorLockedOut) return;

            float[] extractedFeatures = ExtractWindowFeatures(slidingWindowBuffer);

            // =========================================================================
            // TODO [I001 - Kartik Agrawal - Biometric Authentication Lead]:
            // 1. Calculate Mahalanobis or Cosine distance between extractedFeatures and enrolledTemplateFeatureVector.
            // 2. Calibrate classification decision threshold to enforce Equal Error Rate (EER) strictly below 5.0%.
            // 3. Integrate temporal weighting so rapid involuntary head twitches do not cause false rejections.
            // =========================================================================

            currentAnomalyScore = ComputeDistanceMetric(extractedFeatures, enrolledTemplateFeatureVector);

            if (currentAnomalyScore > anomalyThresholdScore)
            {
                activeViolationCount++;
                if (activeViolationCount >= consecutiveViolationsForLockout)
                {
                    TransitionToState(AuthenticationState.ImpostorLockedOut);
                }
                else
                {
                    TransitionToState(AuthenticationState.AnomalyWarning);
                }
            }
            else
            {
                activeViolationCount = Mathf.Max(0, activeViolationCount - 1);
                if (currentState == AuthenticationState.AnomalyWarning)
                {
                    TransitionToState(AuthenticationState.ContinuousVerified);
                }
            }
        }

        private float[] ExtractWindowFeatures(Queue<KinematicFrame> window)
        {
            float totalHeadVel = 0f;
            float totalHandVel = 0f;
            float totalHeadHandDist = 0f;
            int count = window.Count;

            KinematicFrame prev = default;
            bool isFirst = true;

            foreach (var frame in window)
            {
                if (!isFirst)
                {
                    float dt = frame.timestamp - prev.timestamp;
                    if (dt > 0.0001f)
                    {
                        totalHeadVel += Vector3.Distance(frame.headPosition, prev.headPosition) / dt;
                        totalHandVel += Vector3.Distance(frame.rightHandPosition, prev.rightHandPosition) / dt;
                    }
                }
                totalHeadHandDist += Vector3.Distance(frame.headPosition, frame.rightHandPosition);
                prev = frame;
                isFirst = false;
            }

            return new float[]
            {
                totalHeadVel / count,
                totalHandVel / count,
                totalHeadHandDist / count,
                0.95f // baseline jerk profile
            };
        }

        private float ComputeDistanceMetric(float[] featA, float[] featB)
        {
            if (featA == null || featB == null || featA.Length != featB.Length) return 1.0f;

            float sumSq = 0f;
            for (int i = 0; i < featA.Length; i++)
            {
                float diff = featA[i] - featB[i];
                sumSq += diff * diff;
            }
            return Mathf.Sqrt(sumSq);
        }

        private void TransitionToState(AuthenticationState newState)
        {
            if (currentState == newState) return;

            currentState = newState;

            // =========================================================================
            // TODO [I007 - Soha Chand - XR Systems Architect]:
            // 1. If newState == ImpostorLockedOut, freeze avatar 6-DoF inverse kinematics and revoke voice stream.
            // 2. Render red perimeter boundary warning in VR headset viewport and trigger re-auth challenge modal.
            // 3. Dispatch security telemetry alert over enterprise socket to compliance monitoring service.
            // =========================================================================

            Debug.Log($"[BiometricAuth] State changed to: {newState} | Anomaly Score: {currentAnomalyScore:F3}");
            OnAuthenticationStateChanged?.Invoke(currentState, currentAnomalyScore);
        }

        public AuthenticationState GetCurrentState() => currentState;
        public float GetCurrentAnomalyScore() => currentAnomalyScore;
    }
}
