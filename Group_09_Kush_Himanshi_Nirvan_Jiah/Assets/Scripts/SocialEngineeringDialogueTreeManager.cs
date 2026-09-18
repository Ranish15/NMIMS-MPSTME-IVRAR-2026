using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SocialEngineeringDialogueTreeManager models dynamic conversational branch trees,
/// persuasion tactic delivery (Cialdini principles), and employee response state transitions
/// within an immersive corporate social engineering VR simulation.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 09
/// Governing Standard: NIST SP 800-50 (Building an Information Technology Security Awareness Program)
/// </summary>
public class SocialEngineeringDialogueTreeManager : MonoBehaviour
{
    [System.Serializable]
    public enum PersuasionTactic
    {
        None,
        PerceivedAuthority,      // Impersonating IT Director or external compliance auditor
        ArtificialUrgency,       // Impending system wipeout or urgent executive deadline
        ScarcityOpportunity,     // Limited access slot or urgent patch deadline
        SocialProofAlignment,    // Claiming peer departments have already complied
        ReciprocalLeniency       // Offering informal favor in exchange for credential confirmation
    }

    [System.Serializable]
    public enum EmployeeResponseState
    {
        UncheckedCompliance,     // Employee surrenders confidential credentials/access without verification
        HesitantInquiry,         // Employee asks for clarification but accepts superficial explanation
        FormalChallenge,         // Employee requests verifiable employee ID, callback number, or ticket
        SecurityEscalation       // Employee locks terminal and triggers SOC security incident report
    }

    [System.Serializable]
    public struct DialogueNode
    {
        public int nodeId;
        public string avatarUtterance;
        public PersuasionTactic tactic;
        public List<int> childBranchNodeIds;
        public bool isTerminalNode;
        public bool representsSecurityCompromise;
    }

    [Header("Simulation Configuration")]
    [Tooltip("Target scenario: IT_Support_Impersonation, Vendor_Physical_Breach, or Executive_Whaling.")]
    public string scenarioProfile = "IT_Support_Impersonation";

    [Header("Conversational Graph")]
    public List<DialogueNode> dialogueGraph = new List<DialogueNode>();
    public int currentNodeIndex = 0;

    [Header("Session Telemetry")]
    public PersuasionTactic activeTactic = PersuasionTactic.None;
    public EmployeeResponseState finalResponseState = EmployeeResponseState.HesitantInquiry;
    public int totalDialogueExchanges = 0;
    public bool isSimulationComplete = false;

    // Student Technical Boundaries:
    // TODO [I074 - Kush Keswani]: Implement dynamic speech intent classifier and non-linear branch selector based on user speech input.
    // TODO [R002 - Himanshi Agarwal]: Bind avatar facial morph targets, lip-sync audio, and XR environment state to conversational transitions.

    void Start()
    {
        InitializeDialogueGraph();
    }

    /// <summary>
    /// Constructs default conversational attack tree with Cialdini persuasion branch pathways.
    /// </summary>
    private void InitializeDialogueGraph()
    {
        dialogueGraph.Clear();
        currentNodeIndex = 0;
        totalDialogueExchanges = 0;
        isSimulationComplete = false;

        // TODO [I074 - Kush Keswani]: Load multi-scenario branch graph from structured JSON dialogue asset.
        Debug.Log($"[SocialEngineeringDialogueTreeManager] Initialized scenario '{scenarioProfile}' with {dialogueGraph.Count} dialogue nodes.");
    }

    /// <summary>
    /// Processes participant response choice or transcribed verbal input to advance dialogue state.
    /// </summary>
    public void ProcessEmployeeInput(string spokenInput, int selectedBranchIndex)
    {
        if (isSimulationComplete) return;

        totalDialogueExchanges++;

        // TODO [I074 - Kush Keswani]: Map employee NLP response to conversational node transition.
        // TODO [R002 - Himanshi Agarwal]: Trigger avatar gesturing animation reflecting selected persuasion tactic.

        Debug.Log($"[SocialEngineeringDialogueTreeManager] Exchange #{totalDialogueExchanges}: Processed '{spokenInput}'. Current tactic: {activeTactic}");
    }

    /// <summary>
    /// Evaluates final outcome of the conversational social engineering attack.
    /// </summary>
    public bool EvaluateCompromiseStatus()
    {
        return finalResponseState == EmployeeResponseState.UncheckedCompliance;
    }
}
