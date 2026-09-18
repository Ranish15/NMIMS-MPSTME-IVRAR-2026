using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CyberEscapeRoomManager orchestrates the multi-zone physical social engineering scenarios,
/// puzzle validation, and threat vector state machine.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 04
/// Governing Standard: NIST SP 800-53 Controls AT (Awareness & Training) and PE (Physical Protection)
/// </summary>
public class CyberEscapeRoomManager : MonoBehaviour
{
    public enum EscapeZoneState
    {
        Zone1_Reception_Tailgate,
        Zone2_Workstation_USBDrop,
        Zone3_ServerVault_ShoulderSurf,
        RoomCompleted
    }

    [Header("Current Escape Progress")]
    public EscapeZoneState currentZone = EscapeZoneState.Zone1_Reception_Tailgate;
    public float zoneStartTime = 0f;

    [Header("Physical Attack Vector Configurations")]
    [Tooltip("Permissible door holding time in seconds before tailgating alarm triggers.")]
    public float maxDoorOpenLingerSeconds = 3.5f;

    [Tooltip("Distance threshold (meters) where an approaching NPC triggers tailgating suspicion.")]
    public float tailgatingProximityRadius = 1.8f;

    [Header("Telemetry & Score Status")]
    public bool tailgatingVulnerabilityTripped = false;
    public bool rogueUSBPluggedIn = false;
    public bool shoulderSurfedPINExposed = false;

    // Student Technical Boundaries:
    // TODO [K075 - Rahul Behera]: Implement OpenXR hand physics grab interaction for RFID keycard and physical USB drive.
    // TODO [K068 - Rishi Vishwakarma]: Implement NIST SP 800-53 PE/AT attack vector verification rules and state machine triggers.

    void Start()
    {
        zoneStartTime = Time.time;
        InitializeZone1Reception();
    }

    /// <summary>
    /// Zone 1: Reception Area - Tests trainee reaction to an NPC tailgating through secure turnstiles.
    /// </summary>
    public void InitializeZone1Reception()
    {
        // TODO [K075 - Rahul Behera]: Activate turnstile collision triggers and OpenXR RFID badge reader feedback.
        Debug.Log("[CyberEscapeRoomManager] Initialized Zone 1: Reception Turnstile Defense.");
    }

    /// <summary>
    /// Invoked when user holds door open or grants access to an unbadged bystander NPC.
    /// </summary>
    public void OnTailgateIncidentDetected(bool allowedEntry)
    {
        if (allowedEntry)
        {
            tailgatingVulnerabilityTripped = true;
            Debug.LogWarning("[Security Alert] Unauthorized physical access granted (Tailgating violation)!");
        }
        else
        {
            Debug.Log("[Security Success] Tailgater successfully challenged and denied entry.");
        }

        // TODO [K068 - Rishi Vishwakarma]: Log violation timestamp to telemetry and transition to Zone 2.
    }

    /// <summary>
    /// Zone 2: Workstation Floor - Trainee discovers an unlabelled rogue USB drive on a desk.
    /// </summary>
    public void OnUSBInteraction(bool insertedIntoWorkstation)
    {
        if (insertedIntoWorkstation)
        {
            rogueUSBPluggedIn = true;
            Debug.LogError("[Critical Breach] Rogue USB drive inserted! Simulated keystroke injector executed.");
        }
        else
        {
            Debug.Log("[Security Success] Rogue USB isolated and placed in secure evidence container.");
        }

        // TODO [K068 - Rishi Vishwakarma]: Evaluate participant USB handling compliance and trigger Zone 3.
    }

    /// <summary>
    /// Zone 3: Server Room Vault - Trainee enters a PIN on a keypad while an adversary NPC watches.
    /// </summary>
    public void OnPINKeypadEntered(string enteredPIN, bool wasShielded)
    {
        // TODO [K075 - Rahul Behera]: Implement VR head/hand pose occlusion check to determine if keypad was physically shielded.
        if (!wasShielded)
        {
            shoulderSurfedPINExposed = true;
            Debug.LogWarning("[Credential Leak] Authentication PIN compromised via shoulder surfing observation!");
        }
        else
        {
            Debug.Log("[Security Success] Keypad shielded during authentication entry.");
        }
    }

    /// <summary>
    /// Completes the escape room scenario and compiles final audit score.
    /// </summary>
    public void CompleteEscapeRoom()
    {
        currentZone = EscapeZoneState.RoomCompleted;
        float totalDuration = Time.time - zoneStartTime;
        Debug.Log($"[CyberEscapeRoomManager] Escape Room completed in {totalDuration:F1} seconds.");
        // TODO [K068 - Rishi Vishwakarma]: Compute overall OWASP security score and notify telemetry engine.
    }
}
