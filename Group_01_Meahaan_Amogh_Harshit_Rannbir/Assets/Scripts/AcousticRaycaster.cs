// AcousticRaycaster.cs - Real-Time Geometrical Acoustic Raycasting Engine
// Project: IVRAR Group 01 (Home Cinema Acoustics CEDIA/CTA-RP22)
// Student Implementation Boundaries (TODO Tags)

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcousticRaycaster : MonoBehaviour
{
    [Header("Raycast Engine Configuration")]
    public int raysPerSource = 1000;
    public int maxBounces = 6;
    public float soundSpeed = 343.0f; // m/s
    public float receiverRadius = 0.50f;

    [Header("Octave Bands (Hz)")]
    public float[] octaveBands = new float[] { 125f, 250f, 500f, 1000f, 2000f, 4000f };

    [Header("Scene References")]
    public Transform primaryListeningPosition;
    public Transform[] loudspeakerSources;
    public LayerMask acousticSurfacesLayer;

    // Student Task Boundary - C034 Amogh Gupta
    // TODO [C034 - Amogh Gupta]:
    // Implement Snell's law specular reflection vector calculation:
    // Vector3 r = d - 2.0f * Vector3.Dot(d, n) * n;
    // Compute energy absorption per octave band: E_k(n+1) = E_k(n) * (1.0f - alpha_i)
    public Vector3 ComputeSpecularReflection(Vector3 incidentDir, Vector3 surfaceNormal)
    {
        return incidentDir - 2.0f * Vector3.Dot(incidentDir, surfaceNormal) * surfaceNormal;
    }

    // Student Task Boundary - I066 Meahaan Sharma
    // TODO [I066 - Meahaan Sharma]:
    // Implement spherical Fibonacci ray distribution for uniform speaker emission:
    // Generate uniform unit vectors across 4pi steradians for each loudspeaker.
    public Vector3[] GenerateUniformRayDirections(int count)
    {
        Vector3[] dirs = new Vector3[count];
        float phi = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f)); // Golden angle

        for (int i = 0; i < count; i++)
        {
            float y = 1.0f - (i / (float)(count - 1)) * 2.0f;
            float radius = Mathf.Sqrt(1.0f - y * y);
            float theta = phi * i;

            float x = Mathf.Cos(theta) * radius;
            float z = Mathf.Sin(theta) * radius;
            dirs[i] = new Vector3(x, y, z);
        }
        return dirs;
    }

    // Student Task Boundary - I066 Meahaan Sharma
    // TODO [I066 - Meahaan Sharma]:
    // Implement SMPTE/THX sightline verification ray from viewer eye position to screen edges.
    public bool EvaluateSightlineClearance(Vector3 viewerEye, Vector3 screenCenter, out float gazeAngle)
    {
        Vector3 toScreen = screenCenter - viewerEye;
        Vector3 flatForward = new Vector3(toScreen.x, 0, toScreen.z).normalized;
        gazeAngle = Vector3.Angle(flatForward, toScreen);
        return gazeAngle <= 15.0f; // SMPTE standard: <= 15 deg vertical gaze
    }

    void Start()
    {
        Debug.Log("[IVRAR Group 01] Initialized Acoustic Raycaster. Ready for acoustic simulations.");
    }
}
