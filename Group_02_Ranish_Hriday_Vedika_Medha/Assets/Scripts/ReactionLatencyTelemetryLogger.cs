using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace IVRAR.Group02.SensoryStress
{
    /// <summary>
    /// Logs trainee reaction times, engagement decisions, and computes
    /// Signal Detection Theory metrics (d-prime sensitivity and beta criterion).
    /// 
    /// Course Scaffolding provided for IVRAR Group 02.
    /// Contains explicit Student Implementation Boundaries (TODO Tags).
    /// </summary>
    public class ReactionLatencyTelemetryLogger : MonoBehaviour
    {
        [Header("Telemetry Configuration")]
        public string exportFileName = "reaction_latency_trial_log.csv";
        private StringBuilder csvBuffer = new StringBuilder();

        private int totalHits = 0;
        private int totalMisses = 0;
        private int totalFalseAlarms = 0;
        private int totalCorrectRejections = 0;

        void Awake()
        {
            csvBuffer.AppendLine("Timestamp,TrialID,TargetType,TraineeAction,LatencyMs,Hit,FalseAlarm,DPrime,BetaCriterion");
        }

        // =========================================================================
        // Student Implementation Boundaries (TODO Tags)
        // =========================================================================

        // TODO [N047 - Vedika Kaki]:
        // 1. Calculate the millisecond reaction latency between target presentation timestamp
        //    and defensive trigger pull or decision dismissal.
        // 2. Compute Signal Detection Theory metrics using the Stanislaw & Todorov (1999) formulations:
        //    d' = z(HitRate) - z(FalseAlarmRate)
        //    beta = exp(-d' * c), where c = -0.5 * (z(HitRate) + z(FalseAlarmRate)).
        public (double dPrime, double betaCriterion) ComputeSignalDetectionMetrics()
        {
            // Student implementation required
            // Default placeholder return:
            double placeholderDPrime = 2.82;
            double placeholderBeta = 1.05;
            return (placeholderDPrime, placeholderBeta);
        }

        // TODO [N042 - Hriday Jain]:
        // 1. Log cognitive workload proxy indices (gaze fixations, head tremor variance under startle).
        // 2. Correlate trial latency data with NASA-TLX subscales (Mental Demand, Temporal Demand)
        //    and Situational Awareness Global Assessment Technique (SAGAT) target recall.
        public void RecordEngagementEvent(string trialId, bool isHostile, bool traineeEngaged, float latencyMs)
        {
            // Update decision counts
            if (isHostile && traineeEngaged) totalHits++;
            else if (isHostile && !traineeEngaged) totalMisses++;
            else if (!isHostile && traineeEngaged) totalFalseAlarms++;
            else if (!isHostile && !traineeEngaged) totalCorrectRejections++;

            var (dPrime, beta) = ComputeSignalDetectionMetrics();

            csvBuffer.AppendLine($"{DateTime.UtcNow:O},{trialId},{(isHostile ? "Hostile" : "Civilian")}," +
                                 $"{(traineeEngaged ? "Engaged" : "Ignored")},{latencyMs:F1}," +
                                 $"{(isHostile && traineeEngaged ? 1 : 0)},{(!isHostile && traineeEngaged ? 1 : 0)}," +
                                 $"{dPrime:F2},{beta:F2}");

            Debug.Log($"[Telemetry Logged] Trial: {trialId} | Latency: {latencyMs:F1} ms | d': {dPrime:F2}");
        }

        public void ExportTelemetryToDisk()
        {
            string outPath = Path.Combine(Application.dataPath, "..", "telemetry", exportFileName);
            File.WriteAllText(outPath, csvBuffer.ToString());
            Debug.Log($"[Scaffolding] Exported telemetry log to: {outPath}");
        }
    }
}
