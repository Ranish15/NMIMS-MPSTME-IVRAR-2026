using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CrimeSceneEvidenceManager coordinates spatial evidence tagging, photogrammetric
/// anchor alignment, and 3D coordinate serialization for VR crime scene investigation.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 07
/// Governing Standard: ISO/IEC 27037 Forensic Digital Evidence Handling & ASTM E30-19
/// </summary>
public class CrimeSceneEvidenceManager : MonoBehaviour
{
    [System.Serializable]
    public enum EvidenceClassification
    {
        BiologicalTrace,      // Blood spatter, DNA swab site, biological fluid
        BallisticItem,        // Spent shell casing, projectile fragment, impact striation
        Weaponry,             // Firearm, bladed implement, blunt instrument
        LatentFingerprint,    // Friction ridge surface impression, touch contact
        DigitalMedia,         // Smartphone, USB storage, optical medium
        DocumentaryTrace      // Note, invoice, receipt, written correspondence
    }

    [System.Serializable]
    public struct TaggedEvidenceData
    {
        public int evidenceId;
        public EvidenceClassification classification;
        public string labelDescription;
        public Vector3 spatialCoordinates;
        public Quaternion spatialOrientation;
        public float placementTimestampSec;
        public string digitalCustodyHash;
        public bool isGroundTruthValidated;
    }

    [Header("Evidence Configuration")]
    [Tooltip("Prefab marker placed at tagged spatial coordinates.")]
    public GameObject evidenceMarkerPrefab;

    [Tooltip("Maximum raycast distance for spatial tagging in meters.")]
    public float maxTaggingDistanceMeters = 3.5f;

    [Tooltip("Tolerance threshold for Euclidean spatial localization error in meters (e.g. 0.05m = 5cm).")]
    public float spatialErrorToleranceMeters = 0.05f;

    [Header("Active Evidence Registry")]
    public List<TaggedEvidenceData> activeEvidenceList = new List<TaggedEvidenceData>();

    [Header("Session Telemetry")]
    public int totalTagsPlaced = 0;
    public float sessionElapsedDurationSec = 0f;
    public bool isInvestigationLocked = false;

    // Student Technical Boundaries:
    // TODO [N101 - Khushi Srivastava]: Implement XR raycast interactor and direct grab controller binding for marker placement.
    // TODO [N094 - Shirin Sharma]: Implement photogrammetric mesh collider raycast hit and 3D Euclidean error verification against ground truth.

    void Start()
    {
        InitializeEvidenceRegistry();
    }

    void Update()
    {
        if (!isInvestigationLocked)
        {
            sessionElapsedDurationSec += Time.deltaTime;
        }
    }

    /// <summary>
    /// Initializes evidence registry and prepares logging queues.
    /// </summary>
    private void InitializeEvidenceRegistry()
    {
        activeEvidenceList.Clear();
        totalTagsPlaced = 0;
        sessionElapsedDurationSec = 0f;
        Debug.Log("[CrimeSceneEvidenceManager] Evidence management system initialized. Awaiting spatial tag inputs.");
    }

    /// <summary>
    /// Places an evidence marker at the specified spatial coordinate and logs metadata.
    /// </summary>
    public bool RegisterEvidenceTag(
        int evidenceId,
        EvidenceClassification classification,
        string description,
        Vector3 worldPos,
        Quaternion worldRot
    )
    {
        if (isInvestigationLocked)
        {
            Debug.LogWarning("[CrimeSceneEvidenceManager] Investigation session locked. Cannot place additional markers.");
            return false;
        }

        // TODO [N101 - Khushi Srivastava]: Instantiate visual evidenceMarkerPrefab at worldPos with numbered billboard canvas.

        // Compute simulated SHA-256 digital custody fingerprint
        string custodyHash = ComputeSimulatedCustodyHash(evidenceId, worldPos, sessionElapsedDurationSec);

        TaggedEvidenceData tag = new TaggedEvidenceData
        {
            evidenceId = evidenceId,
            classification = classification,
            labelDescription = description,
            spatialCoordinates = worldPos,
            spatialOrientation = worldRot,
            placementTimestampSec = sessionElapsedDurationSec,
            digitalCustodyHash = custodyHash,
            isGroundTruthValidated = false
        };

        // TODO [N094 - Shirin Sharma]: Validate worldPos against ground-truth 3D photogrammetric reference coordinates.

        activeEvidenceList.Add(tag);
        totalTagsPlaced++;
        Debug.Log($"[CrimeSceneEvidenceManager] Tagged #{evidenceId} ({classification}) at {worldPos}. Total tags: {totalTagsPlaced}");
        return true;
    }

    /// <summary>
    /// Evaluates spatial localization accuracy against ground-truth evidence coordinates.
    /// </summary>
    public float ComputeMeanSpatialError(List<Vector3> groundTruthPositions)
    {
        // TODO [N094 - Shirin Sharma]: Calculate Euclidean distance residuals between tagged positions and closest ground truth items.
        if (groundTruthPositions == null || groundTruthPositions.Count == 0 || activeEvidenceList.Count == 0)
        {
            return 0f;
        }

        float totalErrorMeters = 0f;
        int evaluatedCount = 0;

        foreach (var tag in activeEvidenceList)
        {
            float minDistance = float.MaxValue;
            foreach (var gt in groundTruthPositions)
            {
                float dist = Vector3.Distance(tag.spatialCoordinates, gt);
                if (dist < minDistance)
                {
                    minDistance = dist;
                }
            }
            totalErrorMeters += minDistance;
            evaluatedCount++;
        }

        return evaluatedCount > 0 ? (totalErrorMeters / evaluatedCount) : 0f;
    }

    private string ComputeSimulatedCustodyHash(int id, Vector3 pos, float timestamp)
    {
        string raw = $"{id}:{pos.x:F3},{pos.y:F3},{pos.z:F3}:{timestamp:F2}";
        return "SHA256-" + Mathf.Abs(raw.GetHashCode()).ToString("X8");
    }
}
