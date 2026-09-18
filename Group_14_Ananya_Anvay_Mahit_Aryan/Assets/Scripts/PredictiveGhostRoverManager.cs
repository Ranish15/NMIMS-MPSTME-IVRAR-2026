using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.PlanetaryTeleoperation
{
    /// <summary>
    /// Predictive Ghost-Avatar Digital Twin Manager for High-Latency Planetary Rover Teleoperation.
    /// Computes instantaneous forward kinematic projections in response to operator inputs,
    /// rendering an anticipatory holographic avatar ahead of the delayed physical rover.
    /// 
    /// Student Roles:
    /// - I003 Ananya Baweja (Tele-Robotics & Digital Twin Lead): Forward kinematic integration, wheel slip modeling, and path planning
    /// - I006 Anvay Borade (XR Systems Architect): Holographic ghost shader rendering, 3D terrain projection, and path ribbon generation
    /// </summary>
    public class PredictiveGhostRoverManager : MonoBehaviour
    {
        [Header("Physical Rover Hardware Transforms")]
        [SerializeField] private Transform physicalRoverTransform;
        [SerializeField] private Transform ghostAvatarTransform;
        [SerializeField] private LineRenderer futurePathRibbon;

        [Header("Rover Kinematic Specs")]
        [SerializeField] private float maxForwardSpeedMps = 0.5f;
        [SerializeField] private float maxSteeringRateRps = 1.2f;
        [SerializeField] private float wheelBaseMeters = 1.1f;
        [SerializeField] private float trackWidthMeters = 0.85f;

        [Header("Predictive Projection State")]
        [SerializeField] private bool enablePredictiveGhost = true;
        [SerializeField] private float simulatedLatencySeconds = 2.5f;
        [SerializeField] private int projectionHorizonSteps = 25;
        [SerializeField] private float integrationTimestep = 0.1f;

        // Current simulated physical state
        private Vector3 physicalPosition;
        private float physicalHeadingAngle;

        // Predictive ghost avatar state
        private Vector3 ghostPosition;
        private float ghostHeadingAngle;

        private void Start()
        {
            if (physicalRoverTransform != null)
            {
                physicalPosition = physicalRoverTransform.position;
                physicalHeadingAngle = physicalRoverTransform.eulerAngles.y * Mathf.Deg2Rad;
                ghostPosition = physicalPosition;
                ghostHeadingAngle = physicalHeadingAngle;
            }

            if (futurePathRibbon != null)
            {
                futurePathRibbon.positionCount = projectionHorizonSteps;
            }
        }

        public void UpdateOperatorInput(float throttleInput, float steerInput)
        {
            // Clamp operator inputs
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
            steerInput = Mathf.Clamp(steerInput, -1f, 1f);

            // =========================================================================
            // TODO [I003 - Ananya Baweja - Tele-Robotics & Digital Twin Lead]:
            // 1. Solve forward differential-drive kinematics for instantaneous prediction:
            //    v_linear = throttle * maxForwardSpeed; omega = steer * maxSteeringRate.
            // 2. Extrapolate future state [x(t + tau), z(t + tau), theta(t + tau)] across integrationHorizonSteps.
            // 3. Incorporate Martian terrain wheel-slip impedance factor (gamma_slip = 0.15 on soft regolith).
            // =========================================================================

            float vLinear = throttleInput * maxForwardSpeedMps;
            float omega = steerInput * maxSteeringRateRps;

            Vector3 projectedPos = ghostPosition;
            float projectedHeading = ghostHeadingAngle;

            List<Vector3> trajectoryPoints = new List<Vector3>();

            for (int i = 0; i < projectionHorizonSteps; i++)
            {
                float dt = integrationTimestep;
                projectedHeading += omega * dt;
                projectedPos.x += vLinear * Mathf.Sin(projectedHeading) * dt;
                projectedPos.z += vLinear * Mathf.Cos(projectedHeading) * dt;
                trajectoryPoints.Add(projectedPos);
            }

            if (enablePredictiveGhost && ghostAvatarTransform != null)
            {
                ghostPosition = projectedPos;
                ghostHeadingAngle = projectedHeading;
                ghostAvatarTransform.position = ghostPosition;
                ghostAvatarTransform.rotation = Quaternion.Euler(0f, ghostHeadingAngle * Mathf.Rad2Deg, 0f);
            }

            // =========================================================================
            // TODO [I006 - Anvay Borade - XR Systems Architect]:
            // 1. Render futurePathRibbon vertices smoothly interpolating between physicalRoverTransform and ghostAvatarTransform.
            // 2. Apply semi-transparent cyan holographic shader to ghostAvatarTransform with collision boundary glow.
            // 3. Project trajectory ribbon directly onto non-planar Martian digital elevation terrain mesh.
            // =========================================================================

            if (futurePathRibbon != null && trajectoryPoints.Count > 0)
            {
                futurePathRibbon.positionCount = trajectoryPoints.Count;
                futurePathRibbon.SetPositions(trajectoryPoints.ToArray());
            }
        }

        public void ReceiveDelayedPhysicalTelemetry(Vector3 groundTruthPos, Quaternion groundTruthRot)
        {
            physicalPosition = groundTruthPos;
            physicalHeadingAngle = groundTruthRot.eulerAngles.y * Mathf.Deg2Rad;

            if (physicalRoverTransform != null)
            {
                physicalRoverTransform.position = physicalPosition;
                physicalRoverTransform.rotation = groundTruthRot;
            }
        }

        public void SetLatencySeconds(float seconds)
        {
            simulatedLatencySeconds = Mathf.Clamp(seconds, 0.5f, 10.0f);
        }

        public void ToggleGhostAvatar(bool state)
        {
            enablePredictiveGhost = state;
            if (ghostAvatarTransform != null)
            {
                ghostAvatarTransform.gameObject.SetActive(state);
            }
            if (futurePathRibbon != null)
            {
                futurePathRibbon.enabled = state;
            }
        }
    }
}
