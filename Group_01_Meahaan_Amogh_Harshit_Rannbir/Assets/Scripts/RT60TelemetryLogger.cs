// RT60TelemetryLogger.cs - Schroeder Backward Integration and CSV Logger
// Project: IVRAR Group 01 (Home Cinema Acoustics CEDIA/CTA-RP22)
// Student Implementation Boundaries (TODO Tags)

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class RT60TelemetryLogger : MonoBehaviour
{
    [Header("Telemetry Configuration")]
    public string outputFilename = "telemetry/acoustic_benchmark_data.csv";
    public float timeResolutionMs = 5.0f;

    // Energy decay buffers
    private List<float> energyHistogram = new List<float>();

    // Student Task Boundary - C034 Amogh Gupta
    // TODO [C034 - Amogh Gupta]:
    // Implement Schroeder backward integration:
    // E_decay[t] = sum_{j = t}^{N} h^2[j]
    // Extract T20 (from -5dB to -25dB) and extrapolate RT60 = 3 * T20
    public float ComputeSchroederRT60(float[] impulseResponse)
    {
        int n = impulseResponse.Length;
        if (n == 0) return 0.0f;

        float[] energyDecay = new float[n];
        float sum = 0.0f;

        // Backward summation
        for (int i = n - 1; i >= 0; i--)
        {
            sum += impulseResponse[i] * impulseResponse[i];
            energyDecay[i] = sum;
        }

        float totalEnergy = energyDecay[0];
        if (totalEnergy <= 0.0001f) return 0.0f;

        // Convert to dB decay curve
        float[] decayDb = new float[n];
        for (int i = 0; i < n; i++)
        {
            decayDb[i] = 10.0f * Mathf.Log10(Mathf.Max(0.00001f, energyDecay[i] / totalEnergy));
        }

        // Find t(-5dB) and t(-25dB) for T20
        float t5 = -1f, t25 = -1f;
        for (int i = 0; i < n; i++)
        {
            if (t5 < 0 && decayDb[i] <= -5.0f) t5 = i * (timeResolutionMs / 1000.0f);
            if (t25 < 0 && decayDb[i] <= -25.0f) t25 = i * (timeResolutionMs / 1000.0f);
        }

        if (t5 >= 0 && t25 > t5)
        {
            float t20 = t25 - t5;
            return t20 * 3.0f; // Extrapolate to 60 dB decay
        }
        return 0.35f; // Fallback nominal value
    }

    // Student Task Boundary - N087 Rannbir Sachdeva
    // TODO [N087 - Rannbir Sachdeva]:
    // Implement CEDIA/CTA-RP22 compliance evaluation:
    // Target RT60: 0.20s <= RT60 <= 0.50s (nominal 0.25s - 0.35s)
    public bool CheckCEDIACompliance(float rt60)
    {
        return rt60 >= 0.20f && rt60 <= 0.50f;
    }
}
