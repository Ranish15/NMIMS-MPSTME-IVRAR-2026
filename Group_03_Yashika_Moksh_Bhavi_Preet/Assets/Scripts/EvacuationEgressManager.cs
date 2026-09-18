using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// EvacuationEgressManager manages crowd agent navigation, door bottleneck dynamics,
/// and environmental smoke propagation in the university hostel simulation.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 03
/// Reference Standard: National Building Code of India (NBC 2016) / NFPA 101 Life Safety Code
/// </summary>
public class EvacuationEgressManager : MonoBehaviour
{
    [Header("Environment & Geometry Configurations")]
    [Tooltip("Target stairwell discharge door width in meters (NBC standard: >= 1.2m).")]
    public float stairwellDoorWidthMeters = 1.2f;

    [Tooltip("Critical maximum door discharge rate in persons per second per meter width.")]
    public float criticalFlowCapacityPPM = 1.8f;

    [Tooltip("Total number of student occupants residing on the hostel floor.")]
    public int totalFloorOccupants = 85;

    [Header("Smoke & Environmental Visibility")]
    [Range(0f, 1f)]
    public float smokeDensity = 0.0f;
    public float beerLambertExtinctionCoeff = 0.45f;

    [Header("Egress Telemetry Live Outputs")]
    public int evacuatedCount = 0;
    public float currentStairwellDischargeRate = 0f;
    public bool isBottleneckJammed = false;

    // Student Technical Boundaries:
    // TODO [C107 - Moksh Shah]: Implement OpenXR spatial rig binding and stairwell portal trigger volumes.
    // TODO [C068 - Yashika Patil]: Implement Helbing Social Force interpersonal repulsion and smoke speed damping.

    private List<NavMeshAgent> crowdAgents = new List<NavMeshAgent>();
    private float simulationTime = 0f;
    private int agentsThroughDoorWindow = 0;
    private float flowMeasurementTimer = 0f;

    void Start()
    {
        InitializeHostelCorridorGrid();
    }

    void Update()
    {
        simulationTime += Time.deltaTime;
        flowMeasurementTimer += Time.deltaTime;

        // Monitor doorway discharge flux every 1.0 second
        if (flowMeasurementTimer >= 1.0f)
        {
            currentStairwellDischargeRate = agentsThroughDoorWindow / (stairwellDoorWidthMeters * flowMeasurementTimer);
            
            // Check if bottleneck flow exceeds or collapses below critical flow capacity
            if (currentStairwellDischargeRate > criticalFlowCapacityPPM * 1.2f || 
                (agentsThroughDoorWindow == 0 && (totalFloorOccupants - evacuatedCount) > 20))
            {
                isBottleneckJammed = true;
            }
            else
            {
                isBottleneckJammed = false;
            }

            agentsThroughDoorWindow = 0;
            flowMeasurementTimer = 0f;
        }

        UpdateAgentPhysicsForces();
    }

    /// <summary>
    /// Instantiates and positions crowd agents across hostel dorm rooms.
    /// </summary>
    public void InitializeHostelCorridorGrid()
    {
        // TODO [C107 - Moksh Shah]: Configure floor plane colliders, stairwell portal geometry, and VR player boundary checks.
        Debug.Log($"[EvacuationEgressManager] Initializing {totalFloorOccupants} crowd agents across hostel dorm wings.");
    }

    /// <summary>
    /// Updates social force model physics and adjusts speeds based on smoke visibility.
    /// </summary>
    private void UpdateAgentPhysicsForces()
    {
        // TODO [C068 - Yashika Patil]: Implement Helbing Social Force equation:
        // f_ij = A * exp((r_ij - d_ij) / B) * n_ij + k * g(r_ij - d_ij) * n_ij
        // Damped desired speed: v_desired = v_0 * exp(-beerLambertExtinctionCoeff * smokeDensity)
    }

    /// <summary>
    /// Invoked when a crowd agent passes through the stairwell exit portal.
    /// </summary>
    public void RegisterAgentExit(int agentId)
    {
        evacuatedCount++;
        agentsThroughDoorWindow++;
        
        // TODO [C068 - Yashika Patil]: Log agent individual egress latency and record path deviation metric.
    }

    /// <summary>
    /// Invoked by floor marshals to redirect crowd away from blocked stairwells.
    /// </summary>
    public void RedirectCrowdToAlternateStairwell(Vector3 alternateExitPosition)
    {
        // TODO [C107 - Moksh Shah]: Update NavMesh agent destinations dynamically when warden triggers detour command.
        Debug.Log($"[EvacuationEgressManager] Rerouting remaining {totalFloorOccupants - evacuatedCount} agents to alternate portal.");
    }
}
