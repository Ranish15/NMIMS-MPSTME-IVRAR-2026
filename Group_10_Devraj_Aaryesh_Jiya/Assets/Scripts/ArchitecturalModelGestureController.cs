using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// ArchitecturalModelGestureController interprets bare-hand gestures (Pinch, Grab, Rotate, Explode)
/// to manipulate 3D architectural BIM models in VR without dedicated 6-DoF motion controllers.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 10
/// Governing Metric: Gesture Classification Accuracy (%) & Model Review Task Completion Time (s)
/// </summary>
public class ArchitecturalModelGestureController : MonoBehaviour
{
    [System.Serializable]
    public enum HandGesture
    {
        RestOpenHand,       // Neutral hovering hand
        IndexPinch,         // Precise selection or localized scale
        ClosedFistGrab,     // Full model translation / panning
        TwoHandRotate,      // Bilateral rotational alignment
        PalmFacingFreeze    // Temporary lock and contextual BIM menu activation
    }

    [Header("Architectural BIM Model Target")]
    public Transform architecturalModelRoot;
    public ColorMarkerHandTracker handTracker;

    [Header("Gesture Detection Thresholds")]
    public float pinchDistanceThresholdMeters = 0.04f; // 4 cm pinch distance
    public float grabVelocityThreshold = 0.15f;

    [Header("Active Interaction State")]
    public HandGesture currentGesture = HandGesture.RestOpenHand;
    public int successfulGestureCommandsCount = 0;
    public int misrecognizedGestureCount = 0;
    public float sessionInspectionDurationSec = 0f;

    private Vector3 _previousHandPos;
    private bool _isManipulating = false;

    // Student Technical Boundaries:
    // TODO [R054 - Jiya Saxena]: Implement multi-finger gesture classification logic and confusion matrix telemetry.
    // TODO [R045 - Aaryesh Pathare]: Apply affine transformation matrices (translation, rotation, scale) to architecturalModelRoot.

    void Start()
    {
        if (handTracker != null && handTracker.wristTransform != null)
        {
            _previousHandPos = handTracker.wristTransform.position;
        }
    }

    void Update()
    {
        sessionInspectionDurationSec += Time.deltaTime;
        EvaluateHandGesture();
        ApplyModelTransformation();
    }

    /// <summary>
    /// Evaluates current fingertip positions to classify active bare-hand gesture.
    /// </summary>
    private void EvaluateHandGesture()
    {
        if (handTracker == null || !handTracker.isTrackingValid) return;

        // TODO [R054 - Jiya Saxena]: Implement geometric heuristic classifier across 5 fingertip coordinates.
        float pinchDist = handTracker.GetThumbIndexPinchDistance();

        if (pinchDist <= pinchDistanceThresholdMeters)
        {
            currentGesture = HandGesture.IndexPinch;
        }
        else
        {
            currentGesture = HandGesture.RestOpenHand;
        }
    }

    /// <summary>
    /// Applies 3D transformations to the architectural BIM model based on recognized gesture.
    /// </summary>
    private void ApplyModelTransformation()
    {
        if (architecturalModelRoot == null || handTracker == null) return;

        // TODO [R045 - Aaryesh Pathare]: Execute translation, rotation, and explosion kinematics on BIM subcomponents.
        if (currentGesture == HandGesture.IndexPinch && handTracker.wristTransform != null)
        {
            Vector3 currentHandPos = handTracker.wristTransform.position;
            Vector3 delta = currentHandPos - _previousHandPos;
            architecturalModelRoot.position += delta * 1.5f;
            _previousHandPos = currentHandPos;
            _isManipulating = true;
        }
        else if (handTracker.wristTransform != null)
        {
            _previousHandPos = handTracker.wristTransform.position;
            _isManipulating = false;
        }
    }

    /// <summary>
    /// Calculates overall gesture classification accuracy percentage.
    /// </summary>
    public float ComputeGestureAccuracyPct()
    {
        int total = successfulGestureCommandsCount + misrecognizedGestureCount;
        if (total == 0) return 0f;
        return ((float)successfulGestureCommandsCount / total) * 100.0f;
    }

    /// <summary>
    /// Exports session benchmark row to CSV format.
    /// </summary>
    public string ExportSessionBenchmarkRow(string participantId, string condition, float meanLatencyMs, float nasaTlx, float sus)
    {
        float accuracy = ComputeGestureAccuracyPct();
        StringBuilder sb = new StringBuilder();
        sb.Append($"{participantId},{condition},{accuracy:F1},{meanLatencyMs:F2},");
        sb.Append($"{sessionInspectionDurationSec:F1},{successfulGestureCommandsCount},{misrecognizedGestureCount},");
        sb.Append($"{nasaTlx:F1},{sus:F1}");
        return sb.ToString();
    }
}
