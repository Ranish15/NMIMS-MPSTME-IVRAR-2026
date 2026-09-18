using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ARCheckpointDiscoveryManager coordinates geospatial and visual AR checkpoint tracking,
/// proximity boundary triggering, badge unlock animations, and player progression state.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 08
/// Governing Standard: ISO/IEC 23005-2 (Spatial Information Representation) & SBSOD Scale
/// </summary>
public class ARCheckpointDiscoveryManager : MonoBehaviour
{
    [System.Serializable]
    public enum FacilityType
    {
        AcademicLaboratory,   // Robotics, Physics, Computing labs
        AdministrativeOffice, // Student Affairs, Exam Cell, Registrar
        LibraryResource,      // Central Library, Quiet Study Zone, Media Lab
        StudentAmenity,       // Cafeteria, Sports Complex, Health Center
        AuditoriumSeminar     // Main Auditorium, Seminar Halls
    }

    [System.Serializable]
    public struct CampusCheckpoint
    {
        public int checkpointId;
        public string facilityName;
        public FacilityType facilityCategory;
        public Vector3 localSpatialPosition;
        public double latitudeWgs84;
        public double longitudeWgs84;
        public float triggerRadiusMeters;
        public int xpRewardPoints;
        public bool isDiscovered;
        public float discoveryTimestampSec;
    }

    [Header("Checkpoint Configuration")]
    [Tooltip("Prefab instantiated for floating 3D AR beacon markers.")]
    public GameObject checkpointBeaconPrefab;

    [Tooltip("Proximity radius in meters required to unlock a facility checkpoint.")]
    public float defaultProximityThresholdMeters = 5.0f;

    [Header("Campus Facility Registry")]
    public List<CampusCheckpoint> campusCheckpoints = new List<CampusCheckpoint>();

    [Header("Player Progress Telemetry")]
    public int totalPointsAccumulated = 0;
    public int checkpointsDiscoveredCount = 0;
    public float explorationSessionTimeSec = 0f;
    public bool isOrientationSessionActive = false;

    // Student Technical Boundaries:
    // TODO [F050 - Varun Iyer]: Implement AR camera pose projection, visual beacon billboard rendering, and ARFoundation anchor binding.
    // TODO [F049 - Arjun Salunke]: Implement geospatial coordinate conversion (WGS84 to Unity world space) and proximity trigger logic.

    void Start()
    {
        InitializeCampusCheckpoints();
    }

    void Update()
    {
        if (isOrientationSessionActive)
        {
            explorationSessionTimeSec += Time.deltaTime;
        }
    }

    /// <summary>
    /// Populates ground-truth facility locations across the university campus grounds.
    /// </summary>
    private void InitializeCampusCheckpoints()
    {
        // TODO [F049 - Arjun Salunke]: Ingest checkpoint manifest from local JSON configuration asset.
        checkpointsDiscoveredCount = 0;
        totalPointsAccumulated = 0;
        Debug.Log($"[ARCheckpointDiscoveryManager] Initialized {campusCheckpoints.Count} campus checkpoints.");
    }

    /// <summary>
    /// Evaluates user distance to registered checkpoints based on user spatial transform.
    /// </summary>
    public void EvaluateProximityToCheckpoints(Vector3 userWorldPosition)
    {
        if (!isOrientationSessionActive) return;

        // TODO [F050 - Varun Iyer]: Compare userWorldPosition against checkpoint anchors and trigger unlock sequence.
        for (int i = 0; i < campusCheckpoints.Count; i++)
        {
            var cp = campusCheckpoints[i];
            if (!cp.isDiscovered)
            {
                float dist = Vector3.Distance(userWorldPosition, cp.localSpatialPosition);
                if (dist <= cp.triggerRadiusMeters)
                {
                    UnlockCheckpoint(i);
                }
            }
        }
    }

    /// <summary>
    /// Unlocks a checkpoint, awards XP points, and triggers AR reward visualization.
    /// </summary>
    private void UnlockCheckpoint(int index)
    {
        var cp = campusCheckpoints[index];
        cp.isDiscovered = true;
        cp.discoveryTimestampSec = explorationSessionTimeSec;
        campusCheckpoints[index] = cp;

        checkpointsDiscoveredCount++;
        totalPointsAccumulated += cp.xpRewardPoints;

        // TODO [F050 - Varun Iyer]: Spawn 3D particle burst and play auditory reward cue at checkpoint anchor.
        Debug.Log($"[ARCheckpointDiscoveryManager] Unlocked: {cp.facilityName}! XP Earned: {cp.xpRewardPoints}. Total: {totalPointsAccumulated}");
    }

    /// <summary>
    /// Computes percentage of campus facilities discovered by the student.
    /// </summary>
    public float GetDiscoveryCompletionRatePct()
    {
        if (campusCheckpoints.Count == 0) return 0f;
        return ((float)checkpointsDiscoveredCount / campusCheckpoints.Count) * 100f;
    }
}
