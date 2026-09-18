using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// MultiFloorRoutePathfinder manages the 3D multi-level topological building graph,
/// executes A* shortest path search across floors, renders AR guidance arrows, and logs 90 Hz telemetry.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 06
/// </summary>
public class MultiFloorRoutePathfinder : MonoBehaviour
{
    [System.Serializable]
    public class NavigationNode
    {
        public string nodeId;
        public int floorLevel;
        public Vector3 position;
        public List<string> neighborNodeIds = new List<string>();
        public bool isStairwellPortal;
        public bool isElevatorPortal;
    }

    [Header("Telemetry & Pathfinding Settings")]
    public string outputFilename = "ar_indoor_nav_telemetry.csv";
    public float samplingRateHz = 90f;
    public string destinationRoomId = "Lab_402";

    [Header("Observed Components")]
    public ArUcoAnchorPoseManager anchorManager;

    [Header("Navigation Real-Time State")]
    public float transitStartTime = 0f;
    public int wrongTurnsDetected = 0;
    public float currentPathDeviationMeters = 0f;

    // Student Technical Boundaries:
    // TODO [C139 - Vansh Panchal]: Implement 3D Multi-Floor A* graph search with vertical stairwell/elevator transition weights.
    // TODO [C174 - Triesha Shah]: Render floating AR directional waypoint arrows and log 90 Hz trajectory telemetry.

    private StreamWriter writer;
    private float sampleInterval;
    private float lastSampleTime;
    private bool isNavigating = false;
    private List<Vector3> activePathWaypoints = new List<Vector3>();

    void Start()
    {
        sampleInterval = 1f / samplingRateHz;
        InitializeTelemetryLog();
    }

    void Update()
    {
        if (!isNavigating) return;

        if (Time.time - lastSampleTime >= sampleInterval)
        {
            RecordTelemetryFrame();
            lastSampleTime = Time.time;
        }

        MonitorRouteDeviations();
    }

    private void InitializeTelemetryLog()
    {
        try
        {
            string folderPath = Path.Combine(Application.dataPath, "..", "telemetry");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fullPath = Path.Combine(folderPath, outputFilename);

            writer = new StreamWriter(fullPath, false, Encoding.UTF8);
            writer.WriteLine("timestamp_s,user_x,user_y,user_z,current_floor,path_deviation_m,wrong_turns_count,active_anchor_id");
            isNavigating = true;
            transitStartTime = Time.time;
            Debug.Log($"[MultiFloorRoutePathfinder] Initialized navigation telemetry writer at: {fullPath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[MultiFloorRoutePathfinder] Failed to initialize file writer: {ex.Message}");
            isNavigating = false;
        }
    }

    /// <summary>
    /// Computes multi-floor shortest path from user's current anchor to target destination.
    /// </summary>
    public void ComputePathToDestination(string startNodeId, string targetRoomNodeId)
    {
        // TODO [C139 - Vansh Panchal]: Execute A* pathfinding on 3D building graph:
        // f(n) = g(n) + h(n), where h(n) includes Manhattan horizontal distance + vertical floor penalty.
        Debug.Log($"[MultiFloorRoutePathfinder] Calculating shortest route: {startNodeId} -> {targetRoomNodeId}");
    }

    /// <summary>
    /// Checks if student is walking away from planned AR waypoints.
    /// </summary>
    private void MonitorRouteDeviations()
    {
        // TODO [C174 - Triesha Shah]: If distance from closest path segment > 4.0m, flag wrong-turn and trigger recalculation.
        currentPathDeviationMeters = (anchorManager != null && anchorManager.isAnchorLocked) ? 0.04f : 0.85f;
    }

    private void RecordTelemetryFrame()
    {
        Vector3 userPos = Camera.main != null ? Camera.main.transform.position : Vector3.zero;
        int currentFloor = (anchorManager != null) ? anchorManager.lastDetectedMarkerId / 100 : 1;
        int anchorId = (anchorManager != null) ? anchorManager.lastDetectedMarkerId : -1;

        writer.WriteLine($"{Time.time - transitStartTime:F3},{userPos.x:F2},{userPos.y:F2},{userPos.z:F2},{currentFloor},{currentPathDeviationMeters:F3},{wrongTurnsDetected},{anchorId}");
    }

    void OnDestroy()
    {
        if (writer != null)
        {
            writer.Flush();
            writer.Close();
            writer = null;
        }
    }
}
