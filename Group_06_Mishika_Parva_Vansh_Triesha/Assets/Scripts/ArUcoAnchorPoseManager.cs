using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ArUcoAnchorPoseManager handles visual fiducial detection (ArUco / QR),
/// computes Perspective-n-Point (PnP) pose matrices, and resets mobile VIO drift.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 06
/// Governing Standard: VIO Drift Suppression (< 5cm target error)
/// </summary>
public class ArUcoAnchorPoseManager : MonoBehaviour
{
    [System.Serializable]
    public struct VisualAnchorData
    {
        public int markerId;
        public string locationName;
        public int floorLevel;
        public Vector3 globalBIMCoordinates;
        public Quaternion globalBIMRotation;
    }

    [Header("Anchor Configuration")]
    [Tooltip("Physical marker edge size in meters (e.g., 0.18m for standard printable ArUco).")]
    public float markerPhysicalSizeMeters = 0.18f;

    [Tooltip("Maximum allowable PnP reprojection error in pixels before discarding an anchor reading.")]
    public float maxReprojectionErrorPixels = 2.0f;

    [Header("Registered BIM Ground-Truth Anchors")]
    public List<VisualAnchorData> anchorRegistry = new List<VisualAnchorData>();

    [Header("Live Tracking State")]
    public int lastDetectedMarkerId = -1;
    public float cumulativeDriftCorrectionMeters = 0f;
    public bool isAnchorLocked = false;

    // Student Technical Boundaries:
    // TODO [C136 - Mishika Shah]: Implement OpenCV ArUco corner extraction and SolvePnP camera pose calculation.
    // TODO [C172 - Parva Gaglani]: Align ARSessionOrigin transform to BIM coordinate frame upon anchor lock.

    void Start()
    {
        InitializeAnchorRegistry();
    }

    /// <summary>
    /// Populates ground-truth architectural BIM coordinates for visual markers placed in corridors.
    /// </summary>
    private void InitializeAnchorRegistry()
    {
        // TODO [C172 - Parva Gaglani]: Load anchor definitions from building architectural specification JSON.
        Debug.Log($"[ArUcoAnchorPoseManager] Initialized {anchorRegistry.Count} ground-truth visual anchors across building floors.");
    }

    /// <summary>
    /// Invoked when the computer vision pipeline detects an ArUco or QR marker in the camera stream.
    /// </summary>
    public void OnMarkerDetected(int markerId, Vector3 cameraSpacePos, Quaternion cameraSpaceRot, float reprojectionError)
    {
        if (reprojectionError > maxReprojectionErrorPixels)
        {
            Debug.LogWarning($"[ArUcoAnchorPoseManager] Rejected marker {markerId} due to high reprojection error: {reprojectionError:F2}px");
            return;
        }

        lastDetectedMarkerId = markerId;
        isAnchorLocked = true;

        // TODO [C136 - Mishika Shah]: Execute Levenberg-Marquardt PnP refinement and compute camera world pose delta:
        // Delta_Pose = T_BIM_marker * inverse(T_camera_marker)

        ResetVIOTrackingDrift(cameraSpacePos);
    }

    /// <summary>
    /// Resets cumulative dead-reckoning drift in ARCore / ARKit VIO tracking.
    /// </summary>
    private void ResetVIOTrackingDrift(Vector3 measuredLocalOffset)
    {
        // TODO [C172 - Parva Gaglani]: Update ARSessionOrigin position and rotation to eliminate cumulative error (< 0.05m).
        Debug.Log($"[ArUcoAnchorPoseManager] VIO drift reset triggered by visual anchor ID {lastDetectedMarkerId}.");
    }
}
