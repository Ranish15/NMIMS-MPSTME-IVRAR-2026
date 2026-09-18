using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.PlanetaryTeleoperation
{
    /// <summary>
    /// Simulates interplanetary telemetry transmission latency (1.5s to 5.0s) and stochastic packet jitter.
    /// Tracks path following deviation (RMSE) and obstacle collision events against Martian terrain hazards.
    /// 
    /// Student Roles:
    /// - I010 Mahit Naresh Daswani Chanchlani (Latency & Network Simulation Specialist): FIFO queue delay line and jitter distribution
    /// - I041 Aryan Oberoi (Human Factors & Teleoperation QA Lead): Path deviation RMSE, hazard collision logging, and workload metrics
    /// </summary>
    public class HighLatencyNetworkSimulator : MonoBehaviour
    {
        [System.Serializable]
        public struct DelayedCommandPacket
        {
            public float dispatchTimestamp;
            public float executionTimestamp;
            public float throttle;
            public float steering;
        }

        [Header("Interplanetary Latency Settings")]
        [SerializeField] private float oneWayLatencySeconds = 2.5f;
        [SerializeField] private float jitterStandardDeviationSeconds = 0.15f;
        [SerializeField] private float packetLossRate = 0.01f;

        [Header("Hazard Detection")]
        [SerializeField] private LayerMask terrainHazardMask;
        [SerializeField] private float roverHazardRadius = 0.75f;

        [Header("Telemetry Counters")]
        public int totalCollisionsDetected = 0;
        public float cumulativePathErrorSquared = 0f;
        public int pathSampleCount = 0;

        private Queue<DelayedCommandPacket> uplinkDelayQueue = new Queue<DelayedCommandPacket>();
        private PredictiveGhostRoverManager roverManager;

        private void Awake()
        {
            roverManager = GetComponent<PredictiveGhostRoverManager>();
        }

        private void Update()
        {
            ProcessDelayedPackets();
            CheckHazardProximity();
        }

        public void EnqueueTeleoperationCommand(float throttle, float steering)
        {
            // =========================================================================
            // TODO [I010 - Mahit Naresh Daswani Chanchlani - Latency & Network Simulation Specialist]:
            // 1. Compute stochastic transmission delay: tau_actual = oneWayLatency + GaussianJitter().
            // 2. Simulate dropped packets if Random.value < packetLossRate.
            // 3. Enqueue DelayedCommandPacket into uplinkDelayQueue sorted by executionTimestamp.
            // =========================================================================

            if (UnityEngine.Random.value < packetLossRate)
            {
                Debug.LogWarning("[NetworkSimulator] Command packet dropped due to deep-space link attenuation.");
                return;
            }

            float actualDelay = oneWayLatencySeconds + UnityEngine.Random.Range(-jitterStandardDeviationSeconds, jitterStandardDeviationSeconds);
            actualDelay = Mathf.Max(0.1f, actualDelay);

            DelayedCommandPacket packet = new DelayedCommandPacket
            {
                dispatchTimestamp = Time.time,
                executionTimestamp = Time.time + actualDelay,
                throttle = throttle,
                steering = steering
            };

            uplinkDelayQueue.Enqueue(packet);
        }

        private void ProcessDelayedPackets()
        {
            while (uplinkDelayQueue.Count > 0 && uplinkDelayQueue.Peek().executionTimestamp <= Time.time)
            {
                DelayedCommandPacket readyPacket = uplinkDelayQueue.Dequeue();
                ExecuteDelayedCommand(readyPacket);
            }
        }

        private void ExecuteDelayedCommand(DelayedCommandPacket packet)
        {
            // Execute command on physical rover avatar
            transform.Translate(Vector3.forward * packet.throttle * 0.5f * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.up * packet.steering * 60f * Time.deltaTime, Space.Self);

            if (roverManager != null)
            {
                roverManager.ReceiveDelayedPhysicalTelemetry(transform.position, transform.rotation);
            }
        }

        private void CheckHazardProximity()
        {
            // =========================================================================
            // TODO [I041 - Aryan Oberoi - Human Factors & Teleoperation QA Lead]:
            // 1. Perform Physics.OverlapSphere checks around rover chassis against terrainHazardMask.
            // 2. Measure instantaneous cross-track error against reference survey path: e_cross = |p_rover - p_target|.
            // 3. Accumulate RMSE metrics and export formatted teleoperation benchmark records.
            // =========================================================================

            Collider[] hitHazards = Physics.OverlapSphere(transform.position, roverHazardRadius, terrainHazardMask);
            if (hitHazards.Length > 0)
            {
                totalCollisionsDetected++;
                Debug.LogWarning($"[TeleoperationQA] Hazard collision registered at {transform.position}! Total: {totalCollisionsDetected}");
            }
        }

        public float ComputePathRMSE()
        {
            if (pathSampleCount == 0) return 0f;
            return Mathf.Sqrt(cumulativePathErrorSquared / pathSampleCount);
        }

        public void SetTargetLatency(float latency)
        {
            oneWayLatencySeconds = latency;
            if (roverManager != null)
            {
                roverManager.SetLatencySeconds(latency);
            }
        }
    }
}
