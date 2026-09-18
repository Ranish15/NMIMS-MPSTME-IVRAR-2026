using System;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.MassCasualtyTriage
{
    /// <summary>
    /// Simple Triage and Rapid Treatment (START) Protocol State Machine and Scenario Engine.
    /// Simulates industrial disaster casualties with randomized physiological vitals,
    /// dynamic toxic gas/fire hazards, and trainee triage tagging interactions.
    /// 
    /// Student Roles:
    /// - I004 Bhoomi Bhandari (Triage Clinical Protocol Lead): START clinical algorithm, physiological vitals, and diagnostic branching
    /// - I037 Kritivya Mishra (XR Systems Architect): Industrial plant environment, smoke/chemical particle hazards, and spatial tagging UI
    /// </summary>
    public class STARTTriageSimulationManager : MonoBehaviour
    {
        public enum TriageTagCategory
        {
            Unassigned,
            Green_Minor,          // Walking wounded
            Yellow_Delayed,       // Serious, non-life-threatening within 1-2 hours
            Red_Immediate,        // Life-threatening, immediate salvageable intervention
            Black_Expectant       // Deceased or catastrophic non-survivable injuries
        }

        [System.Serializable]
        public struct CasualtyProfile
        {
            public string casualtyId;
            public bool canAmbulate;
            public bool spontaneousBreathing;
            public bool airwayPositionedBreathing;
            public float respiratoryRateBpm;
            public bool hasRadialPulse;
            public float capillaryRefillSeconds;
            public bool obeysSimpleCommands;
            public TriageTagCategory groundTruthTag;
            public Vector3 casualtyPosition;
            public bool isExposedToHazardZone;
        }

        [Header("Casualty Scenario Configuration")]
        [SerializeField] private List<CasualtyProfile> activeCasualties = new List<CasualtyProfile>();
        [SerializeField] private float scenarioTimer = 0f;
        [SerializeField] private bool isSimulationRunning = false;

        [Header("Dynamic Industrial Hazards")]
        [SerializeField] private Transform chemicalPlumeHazardCenter;
        [SerializeField] private float chemicalHazardRadius = 12.0f;
        [SerializeField] private ParticleSystem toxicSmokeEmitter;

        public event Action<string, TriageTagCategory, bool, float> OnTriageTagSubmitted;

        private void Start()
        {
            GenerateDefaultIndustrialCasualties();
        }

        private void Update()
        {
            if (isSimulationRunning)
            {
                scenarioTimer += Time.deltaTime;
                UpdateDynamicHazardPropagation();
            }
        }

        private void GenerateDefaultIndustrialCasualties()
        {
            activeCasualties.Clear();

            // Casualty 1: Severe blast lung, rapid breathing (Red - Immediate)
            activeCasualties.Add(new CasualtyProfile
            {
                casualtyId = "CAS_001_BLAST",
                canAmbulate = false,
                spontaneousBreathing = true,
                airwayPositionedBreathing = true,
                respiratoryRateBpm = 36.0f, // > 30 bpm -> Red
                hasRadialPulse = true,
                capillaryRefillSeconds = 1.8f,
                obeysSimpleCommands = true,
                groundTruthTag = TriageTagCategory.Red_Immediate,
                casualtyPosition = new Vector3(4f, 0f, 10f),
                isExposedToHazardZone = false
            });

            // Casualty 2: Tension pneumothorax, absent pulse (Red - Immediate)
            activeCasualties.Add(new CasualtyProfile
            {
                casualtyId = "CAS_002_HEMORRHAGE",
                canAmbulate = false,
                spontaneousBreathing = true,
                airwayPositionedBreathing = true,
                respiratoryRateBpm = 22.0f,
                hasRadialPulse = false, // Absent radial pulse -> Red
                capillaryRefillSeconds = 3.8f, // > 2s
                obeysSimpleCommands = false,
                groundTruthTag = TriageTagCategory.Red_Immediate,
                casualtyPosition = new Vector3(-6f, 0f, 15f),
                isExposedToHazardZone = true
            });

            // Casualty 3: Closed leg fracture, stable vitals (Yellow - Delayed)
            activeCasualties.Add(new CasualtyProfile
            {
                casualtyId = "CAS_003_FRACTURE",
                canAmbulate = false,
                spontaneousBreathing = true,
                airwayPositionedBreathing = true,
                respiratoryRateBpm = 18.0f,
                hasRadialPulse = true,
                capillaryRefillSeconds = 1.4f,
                obeysSimpleCommands = true,
                groundTruthTag = TriageTagCategory.Yellow_Delayed,
                casualtyPosition = new Vector3(8f, 0f, 22f),
                isExposedToHazardZone = false
            });

            // Casualty 4: Walking superficial chemical burns (Green - Minor)
            activeCasualties.Add(new CasualtyProfile
            {
                casualtyId = "CAS_004_BURNS",
                canAmbulate = true, // Walking wounded -> Green
                spontaneousBreathing = true,
                airwayPositionedBreathing = true,
                respiratoryRateBpm = 20.0f,
                hasRadialPulse = true,
                capillaryRefillSeconds = 1.2f,
                obeysSimpleCommands = true,
                groundTruthTag = TriageTagCategory.Green_Minor,
                casualtyPosition = new Vector3(2f, 0f, 5f),
                isExposedToHazardZone = false
            });

            // Casualty 5: Apneic despite airway repositioning (Black - Expectant)
            activeCasualties.Add(new CasualtyProfile
            {
                casualtyId = "CAS_005_APNEIC",
                canAmbulate = false,
                spontaneousBreathing = false,
                airwayPositionedBreathing = false, // Apneic -> Black
                respiratoryRateBpm = 0.0f,
                hasRadialPulse = false,
                capillaryRefillSeconds = 6.0f,
                obeysSimpleCommands = false,
                groundTruthTag = TriageTagCategory.Black_Expectant,
                casualtyPosition = new Vector3(-12f, 0f, 18f),
                isExposedToHazardZone = false
            });
        }

        public void SubmitTriageDecision(string casualtyId, TriageTagCategory assignedTag, float assessmentDurationSeconds)
        {
            CasualtyProfile casualty = activeCasualties.Find(c => c.casualtyId == casualtyId);
            if (string.IsNullOrEmpty(casualty.casualtyId)) return;

            // =========================================================================
            // TODO [I004 - Bhoomi Bhandari - Triage Clinical Protocol Lead]:
            // 1. Evaluate START decision tree: Ambulation -> Respiration -> Perfusion -> Mental Status.
            // 2. Identify Under-Triage errors (critical risk: marking Red as Yellow/Green/Black).
            // 3. Identify Over-Triage errors (marking Green/Yellow as Red, overwhelming emergency surgical teams).
            // =========================================================================

            bool isCorrect = (assignedTag == casualty.groundTruthTag);
            OnTriageTagSubmitted?.Invoke(casualtyId, assignedTag, isCorrect, assessmentDurationSeconds);
            Debug.Log($"[STARTTriage] Casualty {casualtyId} assigned {assignedTag}. Ground Truth: {casualty.groundTruthTag}. Correct: {isCorrect}");
        }

        private void UpdateDynamicHazardPropagation()
        {
            // =========================================================================
            // TODO [I037 - Kritivya Mishra - XR Systems Architect]:
            // 1. Expand chemicalPlumeHazardCenter radius over time (toxic gas plume dispersion).
            // 2. Trigger audio alarm sirens, smoke opacity shaders, and flashing industrial emergency beacons.
            // 3. If trainee or casualty remains inside hazard zone without PPE, trigger hazardous exposure warning.
            // =========================================================================

            chemicalHazardRadius += 0.05f * Time.deltaTime;
        }

        public void StartSimulation()
        {
            scenarioTimer = 0f;
            isSimulationRunning = true;
            Debug.Log("[STARTTriage] Mass casualty triage simulation started.");
        }

        public void StopSimulation()
        {
            isSimulationRunning = false;
            Debug.Log($"[STARTTriage] Simulation completed. Total elapsed time: {scenarioTimer:F1}s.");
        }

        public List<CasualtyProfile> GetCasualties() => activeCasualties;
    }
}
