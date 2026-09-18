using System;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// BottleneckTelemetryLogger captures 90 Hz real-time egress dynamics,
/// doorway discharge flux, warden-to-marshal dispatch latency, and logs data to CSV.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 03
/// </summary>
public class BottleneckTelemetryLogger : MonoBehaviour
{
    [Header("Telemetry Configuration")]
    public string outputFilename = "hostel_evacuation_live_telemetry.csv";
    public float samplingRateHz = 90f;

    [Header("Observed Components")]
    public EvacuationEgressManager egressManager;

    // Student Technical Boundaries:
    // TODO [C078 - Bhavi Doshi]: Implement NASA-TLX cognitive workload sampling and usability metric recording.
    // TODO [C067 - Preet Shah]: Implement Netcode RPC timestamping for warden-to-marshal dispatch communication latency.

    private StreamWriter writer;
    private float sampleInterval;
    private float lastSampleTime;
    private float commandIssuedTime = 0f;
    private float commandAcknowledgedTime = 0f;
    private bool isLogging = false;

    void Start()
    {
        sampleInterval = 1f / samplingRateHz;
        InitializeTelemetryLog();
    }

    void Update()
    {
        if (!isLogging || egressManager == null) return;

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
            writer.WriteLine("timestamp_s,remaining_occupants,evacuated_count,doorway_discharge_rate_ppm,is_bottleneck_jammed,warden_dispatch_latency_s,route_deviation_pct");
            isLogging = true;
            Debug.Log($"[BottleneckTelemetryLogger] Initialized CSV telemetry logger at: {fullPath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[BottleneckTelemetryLogger] Failed to initialize file writer: {ex.Message}");
            isLogging = false;
        }
    }

    private void RecordTelemetryFrame()
    {
        float currentLatency = (commandAcknowledgedTime > commandIssuedTime) 
            ? (commandAcknowledgedTime - commandIssuedTime) 
            : (Time.time - commandIssuedTime);

        int remaining = egressManager.totalFloorOccupants - egressManager.evacuatedCount;
        float discharge = egressManager.currentStairwellDischargeRate;
        int jammed = egressManager.isBottleneckJammed ? 1 : 0;

        // TODO [C078 - Bhavi Doshi]: Extract route deviation and panic clustering metrics for real-time logging.
        float routeDeviation = jammed == 1 ? 18.5f : 4.2f;

        writer.WriteLine($"{Time.time:F3},{remaining},{egressManager.evacuatedCount},{discharge:F2},{jammed},{currentLatency:F3},{routeDeviation:F1}");
    }

    /// <summary>
    /// Invoked when head warden broadcasts an evacuation command over virtual radio.
    /// </summary>
    public void OnWardenCommandIssued(string commandId)
    {
        commandIssuedTime = Time.time;
        // TODO [C067 - Preet Shah]: Broadcast network RPC packet and record send timestamp with microsecond accuracy.
    }

    /// <summary>
    /// Invoked when floor marshal acknowledges the order on their wrist terminal.
    /// </summary>
    public void OnMarshalCommandAcknowledged(string commandId)
    {
        commandAcknowledgedTime = Time.time;
        float latency = commandAcknowledgedTime - commandIssuedTime;
        Debug.Log($"[BottleneckTelemetryLogger] Inter-warden dispatch latency for {commandId}: {latency * 1000f:F1} ms");
        // TODO [C067 - Preet Shah]: Synchronize acknowledgment across all client network states.
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
