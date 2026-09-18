using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.CybersecurityVR
{
    /// <summary>
    /// Core state machine and scenario controller for the Immersive VR Phishing Simulation.
    /// Manages randomized email and spear-phishing attack vectors, cognitive bias cues,
    /// and participant interaction telemetry.
    /// 
    /// Student Roles:
    /// - D021 Madhav Gaonkar (Phishing Threat Modeling Lead): Heuristic bias cue formulation and threat matrix
    /// - D030 Arnav Jain (XR Systems Architect): Virtual office interaction and immersive spatial UI rendering
    /// </summary>
    public class PhishingThreatSimulationManager : MonoBehaviour
    {
        public enum CognitiveBiasType
        {
            AuthorityBias,      // Impersonating CEO, IT Director, or Provost
            UrgencyScarcity,    // Immediate account suspension countdown
            FamiliarityTrust,   // Known vendor invoice or campus portal clone
            SocialProofReward   // Department-wide bonus or mandatory compliance award
        }

        public enum ThreatClassification
        {
            Legitimate,
            DeceptivePhish,
            SpearPhishTargeted
        }

        [System.Serializable]
        public struct PhishingScenarioData
        {
            public string scenarioId;
            public ThreatClassification classification;
            public CognitiveBiasType biasType;
            public string senderAddress;
            public string displayDomain;
            public string rawHyperlinkUrl;
            public string subjectLine;
            [TextArea(3, 6)]
            public string emailBody;
            public bool hasGrammaticalAnomalies;
            public bool hasDomainHomographSpoof;
            public float timePressureSeconds;
        }

        [Header("Simulation State Configuration")]
        [SerializeField] private List<PhishingScenarioData> scenarioDeck = new List<PhishingScenarioData>();
        [SerializeField] private int currentScenarioIndex = 0;
        [SerializeField] private float sessionTimer = 0f;
        [SerializeField] private bool isSessionActive = false;

        [Header("Telemetry Counters")]
        public int totalScenariosPresented = 0;
        public int correctClassifications = 0;
        public int maliciousClicksCount = 0;
        public int reportingActionsCount = 0;

        public event Action<string, bool, float> OnDecisionRecorded;

        private void Start()
        {
            InitializeDefaultScenarios();
        }

        private void Update()
        {
            if (isSessionActive)
            {
                sessionTimer += Time.deltaTime;
            }
        }

        private void InitializeDefaultScenarios()
        {
            scenarioDeck.Clear();

            // Scenario 1: High Urgency Password Expiry (Urgency Bias)
            scenarioDeck.Add(new PhishingScenarioData
            {
                scenarioId = "SCEN_001_URGENT_AUTH",
                classification = ThreatClassification.DeceptivePhish,
                biasType = CognitiveBiasType.UrgencyScarcity,
                senderAddress = "security-admin@university-portal-verify.com",
                displayDomain = "university.edu/login",
                rawHyperlinkUrl = "https://university-portal-verify.com/auth?token=9281a",
                subjectLine = "URGENT: Mandatory Account Verification Required within 2 Hours",
                emailBody = "Your campus access credentials will be suspended due to suspicious activity. Verify immediately to retain system access.",
                hasGrammaticalAnomalies = false,
                hasDomainHomographSpoof = true,
                timePressureSeconds = 120f
            });

            // Scenario 2: Executive Impersonation (Authority Bias)
            scenarioDeck.Add(new PhishingScenarioData
            {
                scenarioId = "SCEN_002_EXEC_SPEAR",
                classification = ThreatClassification.SpearPhishTargeted,
                biasType = CognitiveBiasType.AuthorityBias,
                senderAddress = "director.office@executive-univ.org",
                displayDomain = "management-portal.edu",
                rawHyperlinkUrl = "http://executive-univ.org/confidential_review.pdf.exe",
                subjectLine = "Confidential: Q3 Institutional Audit Discrepancy",
                emailBody = "Please review the attached audit discrepancies immediately before our executive council board meeting at 2 PM.",
                hasGrammaticalAnomalies = true,
                hasDomainHomographSpoof = true,
                timePressureSeconds = 90f
            });

            // Scenario 3: Legitimate Campus Notification
            scenarioDeck.Add(new PhishingScenarioData
            {
                scenarioId = "SCEN_003_LEGIT_IT",
                classification = ThreatClassification.Legitimate,
                biasType = CognitiveBiasType.FamiliarityTrust,
                senderAddress = "servicedesk@university.edu",
                displayDomain = "university.edu",
                rawHyperlinkUrl = "https://servicedesk.university.edu/maintenance-schedule",
                subjectLine = "Scheduled Campus Network Maintenance - Sunday 02:00 AM",
                emailBody = "Core network routers will undergo scheduled firmware patches this Sunday. No password re-entry or credential submission is required.",
                hasGrammaticalAnomalies = false,
                hasDomainHomographSpoof = false,
                timePressureSeconds = 300f
            });
        }

        public void BeginSimulation()
        {
            currentScenarioIndex = 0;
            totalScenariosPresented = 0;
            correctClassifications = 0;
            maliciousClicksCount = 0;
            reportingActionsCount = 0;
            sessionTimer = 0f;
            isSessionActive = true;
            PresentScenario(currentScenarioIndex);
        }

        private void PresentScenario(int index)
        {
            if (index < 0 || index >= scenarioDeck.Count)
            {
                EndSimulation();
                return;
            }

            PhishingScenarioData current = scenarioDeck[index];
            totalScenariosPresented++;

            // =========================================================================
            // TODO [D030 - Arnav Jain - XR Systems Architect]:
            // 1. Render current.subjectLine and current.emailBody onto the floating 3D virtual office workstation canvas.
            // 2. Bind current.rawHyperlinkUrl to the interactive clickable element with hover raycast detection.
            // 3. Initiate spatial countdown visual timer if current.timePressureSeconds is constrained.
            // =========================================================================

            Debug.Log($"[PhishingSimulation] Scenario {current.scenarioId} presented. Bias: {current.biasType}");
        }

        public void SubmitUserAction(bool didClickLink, bool didReportPhish, bool didIgnore)
        {
            if (!isSessionActive || currentScenarioIndex >= scenarioDeck.Count) return;

            PhishingScenarioData current = scenarioDeck[currentScenarioIndex];
            bool isCorrect = false;

            if (current.classification == ThreatClassification.Legitimate)
            {
                if (!didReportPhish && (didClickLink || didIgnore))
                {
                    isCorrect = true;
                    correctClassifications++;
                }
            }
            else
            {
                if (didClickLink)
                {
                    maliciousClicksCount++;
                    isCorrect = false;
                }
                else if (didReportPhish)
                {
                    correctClassifications++;
                    reportingActionsCount++;
                    isCorrect = true;
                }
            }

            // =========================================================================
            // TODO [D021 - Madhav Gaonkar - Phishing Threat Modeling Lead]:
            // 1. Quantify cognitive susceptibility coefficient based on current.biasType and participant reaction latency.
            // 2. Score domain anomaly detection: Did participant inspect the URL homograph spoof before deciding?
            // 3. Trigger immersive corrective feedback animation illustrating the adversary attack chain if compromised.
            // =========================================================================

            OnDecisionRecorded?.Invoke(current.scenarioId, isCorrect, sessionTimer);

            currentScenarioIndex++;
            PresentScenario(currentScenarioIndex);
        }

        private void EndSimulation()
        {
            isSessionActive = false;
            float accuracy = totalScenariosPresented > 0 ? (float)correctClassifications / totalScenariosPresented * 100f : 0f;
            Debug.Log($"[PhishingSimulation] Completed. Accuracy: {accuracy:F1}% | Malicious Clicks: {maliciousClicksCount} | Reports: {reportingActionsCount}");
        }
    }
}
