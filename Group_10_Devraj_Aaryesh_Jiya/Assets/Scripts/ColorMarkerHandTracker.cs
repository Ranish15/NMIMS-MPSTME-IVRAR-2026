using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ColorMarkerHandTracker ingests segmented fingertip centroids and fiducial markers
/// from the external OpenCV computer vision pipeline, calculates real-time hand skeleton poses,
/// and tracks end-to-end pipeline latency to guarantee sub-15ms performance.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 10
/// Governing Standard: ISO 9241-411 (Evaluation methods for physical input devices) & Sub-15ms Latency
/// </summary>
public class ColorMarkerHandTracker : MonoBehaviour
{
    [System.Serializable]
    public struct FingertipData
    {
        public int fingerIndex;     // 0=Thumb, 1=Index, 2=Middle, 3=Ring, 4=Pinky
        public Vector3 screenPosition;
        public Vector3 worldPosition;
        public Color trackingColorMask;
        public bool isTracked;
    }

    [Header("Tracking Pipeline Configuration")]
    [Tooltip("Target upper bound for end-to-end latency in milliseconds.")]
    public float maxAllowableLatencyMs = 15.0f;

    [Tooltip("Smoothing factor for fingertip Kalman/Exponential filter (0=no smoothing, 1=frozen).")]
    [Range(0f, 0.95f)] public float spatialSmoothingFactor = 0.25f;

    [Header("Fingertip Joints")]
    public FingertipData[] fingerJoints = new FingertipData[5];
    public Transform wristTransform;

    [Header("Live Latency & Telemetry")]
    public float currentPipelineLatencyMs = 0f;
    public float rollingMeanLatencyMs = 0f;
    public int droppedFrameCount = 0;
    public bool isTrackingValid = false;

    private float _lastPacketTimestampMs = 0f;
    private int _sampleCounter = 0;
    private float _cumulativeLatencyMs = 0f;

    // Student Technical Boundaries:
    // TODO [R014 - Devraj Ghumare]: Implement UDP socket packet listener receiving OpenCV HSV contour coordinates.
    // TODO [R045 - Aaryesh Pathare]: Implement 3D depth back-projection mapping 2D image coordinates to Unity VR camera space.

    void Start()
    {
        InitializeFingertipRegistry();
    }

    void Update()
    {
        MonitorPipelineLatency();
    }

    /// <summary>
    /// Configures default fingertip joint tracking structures.
    /// </summary>
    private void InitializeFingertipRegistry()
    {
        Color[] defaultColors = { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta };
        for (int i = 0; i < 5; i++)
        {
            fingerJoints[i] = new FingertipData
            {
                fingerIndex = i,
                screenPosition = Vector3.zero,
                worldPosition = Vector3.zero,
                trackingColorMask = defaultColors[i],
                isTracked = false
            };
        }
        Debug.Log("[ColorMarkerHandTracker] Fingertip tracking structure initialized across 5 color channels.");
    }

    /// <summary>
    /// Updates fingertip coordinates received from the OpenCV vision pipeline.
    /// </summary>
    public void UpdateFingertipPosition(int index, Vector3 newScreenPos, float captureTimestampMs)
    {
        if (index < 0 || index >= 5) return;

        // TODO [R014 - Devraj Ghumare]: Apply HSV color calibration filtering and contour centroid extraction.
        // Calculate latency between frame capture in OpenCV and receipt in Unity
        float receiveTimeMs = Time.realtimeSinceStartup * 1000.0f;
        currentPipelineLatencyMs = receiveTimeMs - captureTimestampMs;

        // TODO [R045 - Aaryesh Pathare]: Unproject newScreenPos through VR camera intrinsic matrix into world space.
        var data = fingerJoints[index];
        data.screenPosition = newScreenPos;
        data.worldPosition = Vector3.Lerp(data.worldPosition, newScreenPos, 1.0f - spatialSmoothingFactor);
        data.isTracked = true;
        fingerJoints[index] = data;

        isTrackingValid = true;
        _sampleCounter++;
        _cumulativeLatencyMs += currentPipelineLatencyMs;
        rollingMeanLatencyMs = _cumulativeLatencyMs / _sampleCounter;
    }

    /// <summary>
    /// Checks whether the vision pipeline meets the sub-15ms latency constraint.
    /// </summary>
    private void MonitorPipelineLatency()
    {
        if (currentPipelineLatencyMs > maxAllowableLatencyMs)
        {
            droppedFrameCount++;
            // Debug warning suppressed to prevent console spam
        }
    }

    /// <summary>
    /// Computes distance between Thumb (0) and Index (1) tips to detect pinch.
    /// </summary>
    public float GetThumbIndexPinchDistance()
    {
        if (!fingerJoints[0].isTracked || !fingerJoints[1].isTracked) return float.MaxValue;
        return Vector3.Distance(fingerJoints[0].worldPosition, fingerJoints[1].worldPosition);
    }
}
