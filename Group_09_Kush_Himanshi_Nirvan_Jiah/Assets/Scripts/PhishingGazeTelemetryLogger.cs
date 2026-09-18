using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// PhishingGazeTelemetryLogger tracks investigator visual fixation dwell times,
/// suspicious phishing lure detection latencies, and behavioral susceptibility metrics.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 09
/// Governing Metric: Fixation Dwell Time (ms), Lure Acquisition Latency (s), & SCAM Model
/// </summary>
public class PhishingGazeTelemetryLogger : MonoBehaviour
{
    [System.Serializable]
    public struct GazeTargetLure
    {
        public string lureName;
        public Collider lureCollider;
        public float totalFixationTimeMs;
        public bool isSuspiciousArtifact;
        public bool wasRecognizedBySubject;
    }

    [Header("Subject Profile")]
    [Tooltip("Participant identifier (e.g. P01 to P50).")]
    public string participantId = "P01";

    [Tooltip("Training Condition: VR_Interactive_Simulation or Traditional_Video_Training.")]
    public string condition = "VR_Interactive_Simulation";

    [Header("Gaze Tracking Sensors")]
    public Transform eyeGazeRaycastOrigin;
    public float maxGazeDistanceMeters = 5.0f;
    public float minimumFixationThresholdMs = 150.0f;

    [Header("Monitored Phishing Lures")]
    public List<GazeTargetLure> registeredLures = new List<GazeTargetLure>();

    [Header("Telemetry State")]
    public float sessionStartTimeSec = 0f;
    public int detectedLuresCount = 0;
    public bool isLoggingActive = false;

    private Collider _currentlyGazedCollider = null;
    private float _currentFixationTimer = 0f;

    // Student Technical Boundaries:
    // TODO [R008 - Nirvan Chhajed]: Implement eye-tracking raycast collision detection and gaze fixation duration accumulator.
    // TODO [R033 - Jiah Kothari]: Implement lure verification validation, challenge latency recording, and CSV telemetry serialization.

    void Start()
    {
        sessionStartTimeSec = Time.time;
        isLoggingActive = true;
    }

    void Update()
    {
        if (!isLoggingActive || eyeGazeRaycastOrigin == null) return;

        SampleGazeRaycast();
    }

    /// <summary>
    /// Executes raycast along the user's eye-gaze / head-gaze forward vector to track attention.
    /// </summary>
    private void SampleGazeRaycast()
    {
        Ray ray = new Ray(eyeGazeRaycastOrigin.position, eyeGazeRaycastOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxGazeDistanceMeters))
        {
            // TODO [R008 - Nirvan Chhajed]: Check if hit.collider belongs to registered suspicious phishing lures and accumulate dwell time.
            if (hit.collider == _currentlyGazedCollider)
            {
                _currentFixationTimer += Time.deltaTime * 1000.0f; // ms
            }
            else
            {
                CommitPreviousFixation();
                _currentlyGazedCollider = hit.collider;
                _currentFixationTimer = 0f;
            }
        }
        else
        {
            CommitPreviousFixation();
            _currentlyGazedCollider = null;
            _currentFixationTimer = 0f;
        }
    }

    private void CommitPreviousFixation()
    {
        if (_currentlyGazedCollider == null || _currentFixationTimer < minimumFixationThresholdMs) return;

        for (int i = 0; i < registeredLures.Count; i++)
        {
            var lure = registeredLures[i];
            if (lure.lureCollider == _currentlyGazedCollider)
            {
                lure.totalFixationTimeMs += _currentFixationTimer;
                registeredLures[i] = lure;
                break;
            }
        }
    }

    /// <summary>
    /// Flags that a participant challenged or recognized a specific lure.
    /// </summary>
    public void RegisterLureDetection(string lureName)
    {
        // TODO [R033 - Jiah Kothari]: Record detection latency from session start and update recognized state.
        for (int i = 0; i < registeredLures.Count; i++)
        {
            var lure = registeredLures[i];
            if (lure.lureName == lureName && !lure.wasRecognizedBySubject)
            {
                lure.wasRecognizedBySubject = true;
                registeredLures[i] = lure;
                detectedLuresCount++;
                Debug.Log($"[PhishingGazeTelemetryLogger] Lure '{lureName}' detected! Total detected: {detectedLuresCount}");
                break;
            }
        }
    }

    /// <summary>
    /// Exports session telemetry as a standardized CSV row.
    /// </summary>
    public string ExportCsvRow(int totalLures, float authoritySusceptibility, float urgencySusceptibility, float nasaTlx, float sus)
    {
        float detectionRatePct = totalLures > 0 ? ((float)detectedLuresCount / totalLures) * 100.0f : 0f;
        float sessionDurationSec = Time.time - sessionStartTimeSec;

        StringBuilder sb = new StringBuilder();
        sb.Append($"{participantId},{condition},{detectedLuresCount},{totalLures},");
        sb.Append($"{detectionRatePct:F1},{sessionDurationSec:F1},");
        sb.Append($"{authoritySusceptibility:F2},{urgencySusceptibility:F2},{nasaTlx:F1},{sus:F1}");
        return sb.ToString();
    }
}
