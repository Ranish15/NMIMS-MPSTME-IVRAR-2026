using System;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// SocialEngineeringTelemetryLogger records 90 Hz interaction telemetry,
/// computes Hake's normalized learning gain (g), and outputs security audit logs to CSV.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 04
/// Formulation: Hake's Normalized Learning Gain g = (Post - Pre) / (100 - Pre)
/// </summary>
public class SocialEngineeringTelemetryLogger : MonoBehaviour
{
    [Header("Telemetry Configuration")]
    public string outputFilename = "cyber_escape_live_telemetry.csv";
    public float samplingRateHz = 90f;

    [Header("Observed Components")]
    public CyberEscapeRoomManager escapeManager;

    [Header("Psychometric & Knowledge Metrics")]
    public float preTestScore = 42.0f;
    public float postTestScore = 86.0f;

    // Student Technical Boundaries:
    // TODO [K081 - Srinidi Subramaniam]: Implement Hake's normalized learning gain formula and NASA-TLX workload data hooks.
    // TODO [K068 - Rishi Vishwakarma]: Implement continuous 90 Hz CSV event streaming and incident flag synchronization.

    private StreamWriter writer;
    private float sampleInterval;
    private float lastSampleTime;
    private bool isLogging = false;

    void Start()
    {
        sampleInterval = 1f / samplingRateHz;
        InitializeTelemetryLog();
    }

    void Update()
    {
        if (!isLogging || escapeManager == null) return;

        if (Time.time - lastSampleTime >= sampleInterval)
        {
            RecordTelemetryFrame();
            lastSampleTime = Time.time;
        }
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
            writer.WriteLine("timestamp_s,escape_zone,tailgate_violation,usb_breach,shoulder_surf_breach,elapsed_zone_time_s");
            isLogging = true;
            Debug.Log($"[SocialEngineeringTelemetryLogger] Initialized telemetry writer at: {fullPath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[SocialEngineeringTelemetryLogger] Failed to initialize file writer: {ex.Message}");
            isLogging = false;
        }
    }

    private void RecordTelemetryFrame()
    {
        int tg = escapeManager.tailgatingVulnerabilityTripped ? 1 : 0;
        int usb = escapeManager.rogueUSBPluggedIn ? 1 : 0;
        int ss = escapeManager.shoulderSurfedPINExposed ? 1 : 0;
        float zoneElapsed = Time.time - escapeManager.zoneStartTime;

        // TODO [K068 - Rishi Vishwakarma]: Write continuous tracking data to CSV file.
        writer.WriteLine($"{Time.time:F3},{escapeManager.currentZone},{tg},{usb},{ss},{zoneElapsed:F2}");
    }

    /// <summary>
    /// Computes Hake's normalized learning gain index g.
    /// Standard interpretation: Low gain (g < 0.3), Medium gain (0.3 <= g <= 0.7), High gain (g > 0.7).
    /// </summary>
    public float CalculateHakeNormalizedGain()
    {
        // TODO [K081 - Srinidi Subramaniam]: Compute g = (Post - Pre) / (100.0f - Pre)
        if (preTestScore >= 100f) return 0f;
        float g = (postTestScore - preTestScore) / (100.0f - preTestScore);
        Debug.Log($"[SocialEngineeringTelemetryLogger] Hake's Normalized Learning Gain g = {g:F3}");
        return g;
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
