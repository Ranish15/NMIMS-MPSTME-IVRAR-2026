using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.CybersecurityVR
{
    /// <summary>
    /// Tracks participant eye-gaze fixations, visual attention heatmaps, and Area-of-Interest (AOI) dwell times
    /// across suspicious email components (sender header, domain URL, urgent call-to-action).
    /// Correlates visual attention with threat recognition latency and 14-day recall.
    /// 
    /// Student Roles:
    /// - D065 Diya Shah (Eye-Gaze & Attention Tracking Specialist): AOI raycasting, fixation thresholding, and pupil dynamics
    /// - I080 Anuvrat Tripathi (Human Factors & Retention Analyst): Attention telemetry export, cognitive bias metrics, and retention scoring
    /// </summary>
    public class EyeGazeAttentionTracker : MonoBehaviour
    {
        public enum AOICategory
        {
            SenderDomainHeader,
            SecurityPadlockBadge,
            SubjectLineUrgency,
            BodyTextContent,
            HyperlinkCallToAction,
            IrrelevantBackground
        }

        [System.Serializable]
        public struct AOIFixationRecord
        {
            public AOICategory aoi;
            public float totalDwellTimeSeconds;
            public int fixationCount;
            public float firstFixationLatencySeconds;
        }

        [Header("Gaze Raycast Configuration")]
        [SerializeField] private Transform gazeOriginCamera;
        [SerializeField] private float maxGazeDistance = 5.0f;
        [SerializeField] private LayerMask aoiLayerMask;
        [SerializeField] private float fixationDwellThreshold = 0.15f; // Minimum 150ms for cognitive fixation

        [Header("Runtime Dwell Telemetry")]
        [SerializeField] private AOICategory activeAOI = AOICategory.IrrelevantBackground;
        [SerializeField] private float currentAOIDwellTimer = 0f;

        private Dictionary<AOICategory, AOIFixationRecord> fixationDatabase = new Dictionary<AOICategory, AOIFixationRecord>();
        private float trialElapsedTimer = 0f;

        private void Awake()
        {
            InitializeAOIDatabase();
        }

        private void InitializeAOIDatabase()
        {
            fixationDatabase.Clear();
            foreach (AOICategory cat in Enum.GetValues(typeof(AOICategory)))
            {
                fixationDatabase[cat] = new AOIFixationRecord
                {
                    aoi = cat,
                    totalDwellTimeSeconds = 0f,
                    fixationCount = 0,
                    firstFixationLatencySeconds = -1f
                };
            }
        }

        private void Update()
        {
            trialElapsedTimer += Time.deltaTime;
            ProcessGazeRaycast();
        }

        private void ProcessGazeRaycast()
        {
            if (gazeOriginCamera == null)
            {
                gazeOriginCamera = Camera.main != null ? Camera.main.transform : null;
                if (gazeOriginCamera == null) return;
            }

            Ray gazeRay = new Ray(gazeOriginCamera.position, gazeOriginCamera.forward);
            RaycastHit hit;

            AOICategory detectedAOI = AOICategory.IrrelevantBackground;

            if (Physics.Raycast(gazeRay, out hit, maxGazeDistance, aoiLayerMask))
            {
                // =========================================================================
                // TODO [D065 - Diya Shah - Eye-Gaze & Attention Tracking Specialist]:
                // 1. Query the hit collider's AOI identifier tag (e.g., "AOI_SenderDomain", "AOI_Hyperlink").
                // 2. Filter out high-frequency saccadic eye tremors using a moving-average gaze window.
                // 3. Detect pupil dilation changes under induced cognitive stress when encountering phishing urgency cues.
                // =========================================================================

                if (hit.collider.CompareTag("AOI_SenderDomain"))
                    detectedAOI = AOICategory.SenderDomainHeader;
                else if (hit.collider.CompareTag("AOI_Hyperlink"))
                    detectedAOI = AOICategory.HyperlinkCallToAction;
                else if (hit.collider.CompareTag("AOI_SubjectLine"))
                    detectedAOI = AOICategory.SubjectLineUrgency;
                else if (hit.collider.CompareTag("AOI_BodyText"))
                    detectedAOI = AOICategory.BodyTextContent;
                else if (hit.collider.CompareTag("AOI_SecurityBadge"))
                    detectedAOI = AOICategory.SecurityPadlockBadge;
            }

            UpdateAOIDwellTimes(detectedAOI);
        }

        private void UpdateAOIDwellTimes(AOICategory detectedAOI)
        {
            if (detectedAOI == activeAOI)
            {
                currentAOIDwellTimer += Time.deltaTime;
            }
            else
            {
                // Register completed fixation if dwell exceeded threshold
                if (currentAOIDwellTimer >= fixationDwellThreshold)
                {
                    AOIFixationRecord rec = fixationDatabase[activeAOI];
                    rec.totalDwellTimeSeconds += currentAOIDwellTimer;
                    rec.fixationCount++;
                    if (rec.firstFixationLatencySeconds < 0f)
                    {
                        rec.firstFixationLatencySeconds = trialElapsedTimer - currentAOIDwellTimer;
                    }
                    fixationDatabase[activeAOI] = rec;
                }

                activeAOI = detectedAOI;
                currentAOIDwellTimer = 0f;
            }
        }

        public string ExportTelemetryCsvRow(string participantId, int scenarioId, int dayInterval)
        {
            // =========================================================================
            // TODO [I080 - Anuvrat Tripathi - Human Factors & Retention Analyst]:
            // 1. Compute the Threat Inspection Ratio: (Dwell_Domain + Dwell_Hyperlink) / Total_Email_Dwell.
            // 2. Correlate dwell on critical indicators with participant's susceptibility score across Day 0 vs Day 14.
            // 3. Format telemetry string into structured CSV schema matching experimental benchmark specifications.
            // =========================================================================

            AOIFixationRecord domainRec = fixationDatabase[AOICategory.SenderDomainHeader];
            AOIFixationRecord linkRec = fixationDatabase[AOICategory.HyperlinkCallToAction];
            AOIFixationRecord bodyRec = fixationDatabase[AOICategory.BodyTextContent];

            float criticalCueDwell = domainRec.totalDwellTimeSeconds + linkRec.totalDwellTimeSeconds;
            float totalDwell = criticalCueDwell + bodyRec.totalDwellTimeSeconds;
            float inspectionRatio = totalDwell > 0f ? (criticalCueDwell / totalDwell) : 0f;

            return $"{participantId},{dayInterval},{scenarioId},{domainRec.totalDwellTimeSeconds:F3},{linkRec.totalDwellTimeSeconds:F3},{domainRec.fixationCount},{linkRec.fixationCount},{inspectionRatio:F3}";
        }

        public void ResetGazeTracker()
        {
            trialElapsedTimer = 0f;
            currentAOIDwellTimer = 0f;
            activeAOI = AOICategory.IrrelevantBackground;
            InitializeAOIDatabase();
        }
    }
}
