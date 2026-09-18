using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// NavigationalSelfEfficacyLogger logs student movement trajectories, quest durations,
/// backtracking occurrences, and Santa Barbara Sense of Direction (SBSOD) self-efficacy scores.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 08
/// Governing Metric: Hegarty SBSOD Environmental Spatial Ability & Orientation Latency
/// </summary>
public class NavigationalSelfEfficacyLogger : MonoBehaviour
{
    [System.Serializable]
    public struct TrajectoryWaypoint
    {
        public float timestamp;
        public Vector3 position;
        public float headingDegrees;
    }

    [Header("Subject Profile")]
    [Tooltip("Participant identifier (e.g., P01 to P50).")]
    public string participantId = "P01";

    [Tooltip("Experimental Condition: Gamified_AR or Static_Map_2D.")]
    public string condition = "Gamified_AR";

    [Header("Tracking Parameters")]
    public float sampleIntervalSeconds = 1.0f;
    public float backtrackingHeadingThresholdDegrees = 135.0f;

    [Header("Telemetry State")]
    public List<TrajectoryWaypoint> trajectoryHistory = new List<TrajectoryWaypoint>();
    public int backtrackingIncidentsCount = 0;
    public float totalPathLengthMeters = 0f;

    [Header("Self-Efficacy Metrics")]
    [Range(1.0f, 7.0f)] public float sbsodPreScore = 3.5f;   // 1 to 7 Likert scale baseline
    [Range(1.0f, 7.0f)] public float sbsodPostScore = 5.2f;  // Post-orientation evaluation

    private float _timer = 0f;
    private Vector3 _lastSampledPos;
    private Vector3 _previousMovementDirection;

    // Student Technical Boundaries:
    // TODO [F014 - Om Kadam]: Implement backtracking detection algorithm and SBSOD psychometric scoring aggregation.
    // TODO [F050 - Varun Iyer]: Implement real-time GPS/VIO trajectory logging and distance accumulation.

    void Start()
    {
        _lastSampledPos = transform.position;
        _previousMovementDirection = transform.forward;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= sampleIntervalSeconds)
        {
            _timer = 0f;
            RecordCurrentWaypoint();
        }
    }

    /// <summary>
    /// Samples current user spatial coordinate and detects sudden 180-degree reversals (backtracking).
    /// </summary>
    private void RecordCurrentWaypoint()
    {
        Vector3 currentPos = transform.position;
        float distanceTravelled = Vector3.Distance(currentPos, _lastSampledPos);

        if (distanceTravelled > 0.3f)
        {
            totalPathLengthMeters += distanceTravelled;
            Vector3 movementDir = (currentPos - _lastSampledPos).normalized;

            // TODO [F014 - Om Kadam]: Detect backtracking when angular deviation exceeds backtrackingHeadingThresholdDegrees.
            float angleDelta = Vector3.Angle(_previousMovementDirection, movementDir);
            if (angleDelta >= backtrackingHeadingThresholdDegrees)
            {
                backtrackingIncidentsCount++;
                Debug.Log($"[NavigationalSelfEfficacyLogger] Backtracking incident detected (#{backtrackingIncidentsCount})! Angle: {angleDelta:F1} deg");
            }

            _previousMovementDirection = movementDir;
            _lastSampledPos = currentPos;

            trajectoryHistory.Add(new TrajectoryWaypoint
            {
                timestamp = Time.time,
                position = currentPos,
                headingDegrees = transform.eulerAngles.y
            });
        }
    }

    /// <summary>
    /// Computes self-efficacy delta on Santa Barbara Sense of Direction Scale (Hegarty 2002).
    /// </summary>
    public float CalculateSbsodGain()
    {
        // TODO [F014 - Om Kadam]: Aggregate 15 Likert items, apply reverse-scoring for negative items, and return normalized gain.
        return Mathf.Max(0f, sbsodPostScore - sbsodPreScore);
    }

    /// <summary>
    /// Formats orientation telemetry as a standardized CSV row.
    /// </summary>
    public string ExportCsvRow(int discoveredFacilities, int totalFacilities, float nasaTlx, float sus)
    {
        float discoveryPct = totalFacilities > 0 ? ((float)discoveredFacilities / totalFacilities) * 100f : 0f;
        float sbsodGain = CalculateSbsodGain();

        StringBuilder sb = new StringBuilder();
        sb.Append($"{participantId},{condition},{discoveredFacilities},{totalFacilities},");
        sb.Append($"{discoveryPct:F1},{Time.time:F1},{backtrackingIncidentsCount},{totalPathLengthMeters:F2},");
        sb.Append($"{sbsodPreScore:F2},{sbsodPostScore:F2},{sbsodGain:F2},{nasaTlx:F1},{sus:F1}");
        return sb.ToString();
    }
}
