using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.MassCasualtyTriage
{
    /// <summary>
    /// Telemetry Logger and Triage Confusion Matrix Specialist Module.
    /// Tracks trainee spatial transit duration, per-casualty assessment latency,
    /// under-triage and over-triage rates, and overall START protocol categorization accuracy.
    /// 
    /// Student Roles:
    /// - I044 Tanvi Paithankar (Spatial Telemetry & Confusion Matrix Specialist): Multi-class confusion matrix, under-triage/over-triage computation
    /// - I069 Jia Jadhav (Human Factors & Usability Engineer): Assessment latency profiling, NASA-TLX workload logging, and clinical CSV export
    /// </summary>
    public class TriageTelemetryLogger : MonoBehaviour
    {
        [Header("Triage Performance Metrics")]
        public int totalCasualtiesAssessed = 0;
        public int correctCategorizations = 0;
        public int underTriageCount = 0;
        public int overTriageCount = 0;
        public float totalAssessmentDurationSeconds = 0f;

        // 4x4 Confusion Matrix: [Actual, Predicted]
        // 0: Green_Minor, 1: Yellow_Delayed, 2: Red_Immediate, 3: Black_Expectant
        private int[,] confusionMatrix = new int[4, 4];

        private STARTTriageSimulationManager triageManager;

        private void Awake()
        {
            triageManager = GetComponent<STARTTriageSimulationManager>();
            if (triageManager != null)
            {
                triageManager.OnTriageTagSubmitted += HandleTriageTagSubmitted;
            }
        }

        private void OnDestroy()
        {
            if (triageManager != null)
            {
                triageManager.OnTriageTagSubmitted -= HandleTriageTagSubmitted;
            }
        }

        private void HandleTriageTagSubmitted(string casualtyId, STARTTriageSimulationManager.TriageTagCategory assigned, bool isCorrect, float latencySec)
        {
            totalCasualtiesAssessed++;
            totalAssessmentDurationSeconds += latencySec;

            if (isCorrect)
            {
                correctCategorizations++;
            }

            // Find ground truth tag
            var casualty = triageManager.GetCasualties().Find(c => c.casualtyId == casualtyId);
            int actualIdx = CategoryToIndex(casualty.groundTruthTag);
            int predictedIdx = CategoryToIndex(assigned);

            // =========================================================================
            // TODO [I044 - Tanvi Paithankar - Spatial Telemetry & Confusion Matrix Specialist]:
            // 1. Populate confusionMatrix[actualIdx, predictedIdx]++.
            // 2. Classify critical Under-Triage: Actual == Red_Immediate (2) but Predicted < 2.
            // 3. Classify Over-Triage: Actual < 2 (Green/Yellow) but Predicted == Red_Immediate (2).
            // =========================================================================

            if (actualIdx >= 0 && predictedIdx >= 0)
            {
                confusionMatrix[actualIdx, predictedIdx]++;
            }

            if (casualty.groundTruthTag == STARTTriageSimulationManager.TriageTagCategory.Red_Immediate &&
                assigned != STARTTriageSimulationManager.TriageTagCategory.Red_Immediate)
            {
                underTriageCount++;
            }
            else if (casualty.groundTruthTag != STARTTriageSimulationManager.TriageTagCategory.Red_Immediate &&
                     assigned == STARTTriageSimulationManager.TriageTagCategory.Red_Immediate)
            {
                overTriageCount++;
            }
        }

        private int CategoryToIndex(STARTTriageSimulationManager.TriageTagCategory cat)
        {
            switch (cat)
            {
                case STARTTriageSimulationManager.TriageTagCategory.Green_Minor: return 0;
                case STARTTriageSimulationManager.TriageTagCategory.Yellow_Delayed: return 1;
                case STARTTriageSimulationManager.TriageTagCategory.Red_Immediate: return 2;
                case STARTTriageSimulationManager.TriageTagCategory.Black_Expectant: return 3;
                default: return -1;
            }
        }

        public string ExportTraineeSummaryCsvRow(string traineeId, float nasaTlxWorkload)
        {
            // =========================================================================
            // TODO [I069 - Jia Jadhav - Human Factors & Usability Engineer]:
            // 1. Compute mean triage latency per patient: T_mean = totalAssessmentDuration / totalCasualties.
            // 2. Compute overall accuracy percentage: Acc = (correct / total) * 100.
            // 3. Format clinical CSV row: TraineeID, Acc%, MeanLatency, UnderTriageRate, OverTriageRate, NASA-TLX.
            // =========================================================================

            float accPct = totalCasualtiesAssessed > 0 ? ((float)correctCategorizations / totalCasualtiesAssessed) * 100f : 0f;
            float meanLatency = totalCasualtiesAssessed > 0 ? (totalAssessmentDurationSeconds / totalCasualtiesAssessed) : 0f;
            float underTriageRate = totalCasualtiesAssessed > 0 ? ((float)underTriageCount / totalCasualtiesAssessed) * 100f : 0f;
            float overTriageRate = totalCasualtiesAssessed > 0 ? ((float)overTriageCount / totalCasualtiesAssessed) * 100f : 0f;

            return $"{traineeId},{accPct:F1},{meanLatency:F2},{underTriageRate:F1},{overTriageRate:F1},{nasaTlxWorkload:F1}";
        }

        public int[,] GetConfusionMatrix() => confusionMatrix;
    }
}
