// AcousticRaycaster.cs - Real-Time Geometrical Acoustic Raycasting Engine
// Project: IVRAR Group 01 (Home Cinema Acoustics CEDIA/CTA-RP22)
// Course: Introduction to Virtual Reality and Augmented Reality (702COI002)

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

    // References to coupled student subsystems
    public RT60TelemetryLogger telemetryLogger;

    // =========================================================================
    // STUDENT 1: Meahaan Sharma (I066) - Lead XR Systems Architect
    // Branch: feat/i066-xr-systems-architect
    // =========================================================================

    /// <summary>
    /// Generates uniform ray direction unit vectors across 4pi steradians using the spherical Fibonacci lattice.
    /// </summary>
    /// <param name="count">Total number of rays to emit per loudspeaker source.</param>
    /// <returns>Array of normalized Vector3 direction vectors.</returns>
    public Vector3[] GenerateUniformRayDirections(int count)
    {
        // TODO [I066 - Meahaan Sharma]:
        // 1. Mathematical Formulation:
        //    Golden angle phi = pi * (3.0 - sqrt(5.0)) approx 2.399963 rad.
        //    For index i in [0, count - 1]:
        //      y = 1.0 - (i / (count - 1)) * 2.0
        //      radius = sqrt(max(0, 1.0 - y * y))
        //      theta = phi * i
        //      x = cos(theta) * radius
        //      z = sin(theta) * radius
        // 2. OpenXR Unity API: Return unit vectors normalized with Vector3.Normalize().
        // 3. Boundary Condition: Check for count <= 0 (return Vector3.forward fallback).
        // 4. Oral Viva Defense: Explain why Fibonacci lattice sampling minimizes polar clustering 
        //    compared to naive spherical coordinate random generation (theta, phi ~ Uniform).

        Vector3[] dirs = new Vector3[count];
        float phi = Mathf.PI * (3.0f - Mathf.Sqrt(5.0f));

        for (int i = 0; i < count; i++)
        {
            float y = 1.0f - (i / (float)Mathf.Max(1, count - 1)) * 2.0f;
            float radius = Mathf.Sqrt(Mathf.Max(0.0f, 1.0f - y * y));
            float theta = phi * i;
            dirs[i] = new Vector3(Mathf.Cos(theta) * radius, y, Mathf.Sin(theta) * radius);
        }
        return dirs;
    }

    /// <summary>
    /// Evaluates vertical and horizontal visual sightline clearance from viewer position to the screen center.
    /// </summary>
    public bool EvaluateSightlineClearance(Vector3 viewerEye, Vector3 screenCenter, out float gazeAngle)
    {
        // TODO [I066 - Meahaan Sharma]:
        // 1. Mathematical Formulation:
        //    Vector3 toScreen = screenCenter - viewerEye;
        //    Vector3 horizontalProjection = Vector3.ProjectOnPlane(toScreen, Vector3.up).normalized;
        //    gazeAngle = Vector3.Angle(horizontalProjection, toScreen);
        // 2. Standards Threshold:
        //    SMPTE EG 18-1994 / THX standard requires vertical elevation <= 15 degrees.
        // 3. Oral Viva Defense: How does user head tracking drift in OpenXR affect dynamic sightline certification?

        Vector3 toScreen = screenCenter - viewerEye;
        Vector3 flatForward = new Vector3(toScreen.x, 0, toScreen.z).normalized;
        gazeAngle = Vector3.Angle(flatForward, toScreen);
        return gazeAngle <= 15.0f;
    }

    // =========================================================================
    // STUDENT 2: Amogh Gupta (C034) - Spatial Acoustics & Audio Specialist
    // Branch: feat/c034-spatial-acoustics-au
    // =========================================================================

    /// <summary>
    /// Computes specular reflection direction vector and applies frequency-dependent absorption.
    /// </summary>
    public Vector3 ComputeSpecularReflection(Vector3 incidentDir, Vector3 surfaceNormal, float[] energyBands, float[] materialAlphas)
    {
        // TODO [C034 - Amogh Gupta]:
        // 1. Snell's Law Formulation:
        //    r = d - 2.0 * dot(d, n) * n (where d = incidentDir, n = surfaceNormal).
        // 2. Multi-Band Energy Attenuation:
        //    For each band k in [0, 5]: energyBands[k] *= (1.0f - materialAlphas[k]);
        // 3. Boundary Conditions:
        //    Ensure dot(d, n) < 0; clamp remaining energy >= 0.0f.
        // 4. Oral Viva Defense: Explain how frequency-dependent absorption at 125 Hz differs 
        //    between standard 1/2 inch gypsum drywall (alpha approx 0.29) and acoustic fabric panels (alpha approx 0.08).

        Vector3 refl = incidentDir - 2.0f * Vector3.Dot(incidentDir, surfaceNormal) * surfaceNormal;
        if (energyBands != null && materialAlphas != null)
        {
            for (int k = 0; k < Mathf.Min(energyBands.Length, materialAlphas.Length); k++)
            {
                energyBands[k] *= Mathf.Clamp01(1.0f - materialAlphas[k]);
            }
        }
        return refl.normalized;
    }

    // =========================================================================
    // STUDENT 3: Harshit Rai (N083) - Human Factors & Usability Engineer
    // Branch: feat/n083-human-factors-usabil
    // =========================================================================

    /// <summary>
    /// Tests whether an acoustic ray intersects the primary listening position receiver sphere and triggers spatial haptic cues.
    /// </summary>
    public bool DetectReceiverIntersection(Vector3 rayOrigin, Vector3 rayDir, float rayLength, out float arrivalDelay)
    {
        // TODO [N083 - Harshit Rai]:
        // 1. Geometry Formulation:
        //    Ray-sphere intersection test against primaryListeningPosition.position with receiverRadius.
        // 2. Arrival Time Calculation:
        //    arrivalDelay = totalPathDistance / soundSpeed (soundSpeed = 343 m/s).
        // 3. Haptic Integration:
        //    Trigger OpenXR controller impulse when early reflections (< 20 ms) arrive from front stage.
        // 4. Oral Viva Defense: How does the Direct-to-Reverberant Ratio (DRR) affect speech intelligibility and listener envelopment (LEV)?

        arrivalDelay = 0.0f;
        if (primaryListeningPosition == null) return false;

        Vector3 toReceiver = primaryListeningPosition.position - rayOrigin;
        float projection = Vector3.Dot(toReceiver, rayDir);
        if (projection > 0 && projection < rayLength)
        {
            float perpDistSq = toReceiver.sqrMagnitude - (projection * projection);
            if (perpDistSq <= (receiverRadius * receiverRadius))
            {
                arrivalDelay = projection / soundSpeed;
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        Debug.Log("[IVRAR Group 01] Initialized Acoustic Raycaster. Ready for acoustic simulations.");
    }
}
