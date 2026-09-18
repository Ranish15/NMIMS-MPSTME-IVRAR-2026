using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IVRAR.SecurityAudit
{
    public enum SocialEngineeringPretext
    {
        DeliveryCourierHeavyBox,
        HurriedExecutiveNoBadge,
        TelecomContractorClipboard,
        DisgruntledFormerEmployee
    }

    public enum TraineeSecurityAction
    {
        Pending,
        HeldDoorOpen_Breach,
        ChallengedBadge_Compliant,
        DirectedToReception_Compliant,
        TriggeredDuressAlarm_Compliant,
        IgnoredVisitor_Breach
    }

    /// <summary>
    /// Manages corporate entrance physical security scenarios, avatar social engineering approaches,
    /// and trainee security compliance decisions during simulated penetration testing.
    /// </summary>
    public class TailgatingBreachManager : MonoBehaviour
    {
        [Header("Scenario Configuration")]
        [SerializeField] private SocialEngineeringPretext currentPretext = SocialEngineeringPretext.DeliveryCourierHeavyBox;
        [SerializeField] private float approachDistanceThreshold = 1.8f;
        [SerializeField] private float decisionTimeoutSeconds = 15.0f;
        [SerializeField] private bool turnstileDoorLocked = true;

        [Header("Scene References")]
        [SerializeField] private Transform traineeTransform;
        [SerializeField] private Transform socialEngineerNpc;
        [SerializeField] private AudioSource socialEngineerDialogueAudio;
        [SerializeField] private PhysicalSecurityTelemetryLogger telemetryLogger;

        // Runtime state variables
        private TraineeSecurityAction resolvedAction = TraineeSecurityAction.Pending;
        private float scenarioTimer = 0.0f;
        private bool isScenarioActive = false;
        private bool isSocialEngineerChallenged = false;

        public SocialEngineeringPretext CurrentPretext => currentPretext;
        public TraineeSecurityAction ResolvedAction => resolvedAction;
        public float ScenarioElapsedTime => scenarioTimer;
        public bool IsScenarioActive => isScenarioActive;

        private void Start()
        {
            InitializeScenario(currentPretext);
        }

        public void InitializeScenario(SocialEngineeringPretext pretext)
        {
            currentPretext = pretext;
            resolvedAction = TraineeSecurityAction.Pending;
            scenarioTimer = 0.0f;
            isScenarioActive = true;
            isSocialEngineerChallenged = false;
            turnstileDoorLocked = true;

            Debug.Log($"[SecurityAudit] Scenario initialized with pretext: {currentPretext}");

            // TODO [B069 - Samarth Pande]: Implement social engineering pretext state transitions, door interlock latching, and challenge dialogue response validation.
            /*
             * Samarth Pande (B069) - Physical Security Controls Lead:
             * 1. Configure Cialdini influence triggers (e.g. reciprocity for courier, authority for executive).
             * 2. Program electronic turnstile lock/unlock relays based on NFC/RFID reader events.
             * 3. Validate trainee dialogue selection against ISO/IEC 27001 Control A.7 physical entry requirements.
             */
        }

        private void Update()
        {
            if (!isScenarioActive) return;

            scenarioTimer += Time.deltaTime;

            // Check distance between trainee and approaching NPC
            if (traineeTransform != null && socialEngineerNpc != null)
            {
                float distance = Vector3.Distance(traineeTransform.position, socialEngineerNpc.position);
                if (distance <= approachDistanceThreshold && !isSocialEngineerChallenged)
                {
                    TriggerSocialEngineerDialoguePrompt();
                }
            }

            // Enforce decision timeout
            if (scenarioTimer >= decisionTimeoutSeconds && resolvedAction == TraineeSecurityAction.Pending)
            {
                OnTimeoutBreach();
            }

            // TODO [B148 - Ishan Choudhary]: Integrate dynamic NPC animation state tree, audio spatialization for polite pressure prompts, and physical turnstile collision barriers.
            /*
             * Ishan Choudhary (B148) - XR Systems Architect:
             * 1. Drive NPC walking/idle/rejection animations via Mecanim blend tree.
             * 2. Configure 3D spatialized HRTF audio for voice requests ("Could you hold the door? Hands are full!").
             * 3. Enforce physical NavMesh Obstacles and BoxColliders preventing NPC transit unless door is held.
             */
        }

        private void TriggerSocialEngineerDialoguePrompt()
        {
            if (socialEngineerDialogueAudio != null && !socialEngineerDialogueAudio.isPlaying)
            {
                socialEngineerDialogueAudio.Play();
            }
            Debug.Log($"[SecurityAudit] Social engineer playing dialogue prompt for {currentPretext}");
        }

        public void RegisterTraineeAction(TraineeSecurityAction action)
        {
            if (!isScenarioActive || resolvedAction != TraineeSecurityAction.Pending) return;

            resolvedAction = action;
            isScenarioActive = false;

            bool isBreach = (action == TraineeSecurityAction.HeldDoorOpen_Breach || action == TraineeSecurityAction.IgnoredVisitor_Breach);

            if (isBreach)
            {
                turnstileDoorLocked = false;
                Debug.LogWarning($"[SecurityAudit] PHYSICAL BREACH OCCURRED! Action: {action}, Latency: {scenarioTimer:F2}s");
            }
            else
            {
                turnstileDoorLocked = true;
                Debug.Log($"[SecurityAudit] COMPLIANT SECURITY PROTOCOL EXECUTED: {action}, Latency: {scenarioTimer:F2}s");
            }

            if (telemetryLogger != null)
            {
                telemetryLogger.LogScenarioCompletion(currentPretext, resolvedAction, scenarioTimer, !isBreach);
            }
        }

        public void ChallengeBadge()
        {
            isSocialEngineerChallenged = true;
            RegisterTraineeAction(TraineeSecurityAction.ChallengedBadge_Compliant);
        }

        public void DirectToReception()
        {
            RegisterTraineeAction(TraineeSecurityAction.DirectedToReception_Compliant);
        }

        public void HoldDoorForVisitor()
        {
            RegisterTraineeAction(TraineeSecurityAction.HeldDoorOpen_Breach);
        }

        private void OnTimeoutBreach()
        {
            RegisterTraineeAction(TraineeSecurityAction.IgnoredVisitor_Breach);
        }
    }
}
