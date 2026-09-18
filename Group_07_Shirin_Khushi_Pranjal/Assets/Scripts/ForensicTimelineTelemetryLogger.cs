using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// ForensicTimelineTelemetryLogger logs investigator trajectory, event sequencing,
/// Kendall-Tau timeline concordance, and chain-of-custody audit trails.
/// 
/// Course: IVRAR (Immersive Virtual, Real & Augmented Reality) - Group 07
/// Governing Metric: Timeline Sequencing Kendall-Tau Correlation & Spatial Trajectory Length
/// </summary>
public class ForensicTimelineTelemetryLogger : MonoBehaviour
{
    [System.Serializable]
    public struct TimelineEventRecord
    {
        public int sequenceRank;
        public int evidenceId;
        public string eventHypothesis;
        public float timestampSec;
        public Vector3 observerPosition;
    }

    [Header("Telemetry Configuration")]
    [Tooltip("Subject identifier for empirical benchmarking (e.g. P01 to P50).")]
    public string participantId = "P01";

    [Tooltip("Investigation condition: VR_Spatial or Photo_Log_2D.")]
    public string experimentalCondition = "VR_Spatial";

    [Header("Investigator Tracking")]
    public Transform xrHeadsetTransform;
    public float trajectorySampleIntervalSec = 0.5f;

    [Header("Timeline Sequence State")]
    public List<TimelineEventRecord> recordedTimeline = new List<TimelineEventRecord>();
    public List<int> groundTruthSequence = new List<int> { 101, 104, 102, 105, 103 }; // Ground truth event sequence

    private float _totalPathLengthMeters = 0f;
    private Vector3 _lastSampledHeadsetPos;
    private float _trajectoryTimer = 0f;

    // Student Technical Boundaries:
    // TODO [N106 - Pranjal Thakur]: Implement chain-of-custody audit logging and chronological timeline reconstruction comparator.
    // TODO [N094 - Shirin Sharma]: Implement spatial trajectory path recording and room coverage area estimation.

    void Start()
    {
        if (xrHeadsetTransform != null)
        {
            _lastSampledHeadsetPos = xrHeadsetTransform.position;
        }
    }

    void Update()
    {
        if (xrHeadsetTransform == null) return;

        _trajectoryTimer += Time.deltaTime;
        if (_trajectoryTimer >= trajectorySampleIntervalSec)
        {
            _trajectoryTimer = 0f;
            SampleHeadsetTrajectory();
        }
    }

    /// <summary>
    /// Tracks total path distance walked by investigator inside the spatial crime scene.
    /// </summary>
    private void SampleHeadsetTrajectory()
    {
        // TODO [N094 - Shirin Sharma]: Record HMD spatial coordinates into trajectory buffer and calculate cumulative distance.
        Vector3 currentPos = xrHeadsetTransform.position;
        float deltaDist = Vector3.Distance(currentPos, _lastSampledHeadsetPos);
        if (deltaDist > 0.02f)
        {
            _totalPathLengthMeters += deltaDist;
            _lastSampledHeadsetPos = currentPos;
        }
    }

    /// <summary>
    /// Registers a sequenced event in the reconstructed timeline hypothesis.
    /// </summary>
    public void AppendTimelineEvent(int evidenceId, string hypothesisText)
    {
        TimelineEventRecord record = new TimelineEventRecord
        {
            sequenceRank = recordedTimeline.Count + 1,
            evidenceId = evidenceId,
            eventHypothesis = hypothesisText,
            timestampSec = Time.time,
            observerPosition = xrHeadsetTransform != null ? xrHeadsetTransform.position : Vector3.zero
        };

        recordedTimeline.Add(record);
        Debug.Log($"[ForensicTimelineTelemetryLogger] Event #{record.sequenceRank} recorded: Evidence ID {evidenceId}");
    }

    /// <summary>
    /// Computes Kendall-Tau rank correlation coefficient between participant's timeline and ground-truth.
    /// Range: -1.0 (inverted sequence) to +1.0 (perfect chronological concordance).
    /// </summary>
    public float ComputeTimelineKendallTau()
    {
        // TODO [N106 - Pranjal Thakur]: Evaluate concordant and discordant pairs relative to groundTruthSequence.
        if (recordedTimeline.Count < 2 || groundTruthSequence.Count < 2)
        {
            return 0f;
        }

        List<int> candidateSeq = new List<int>();
        foreach (var r in recordedTimeline)
        {
            if (groundTruthSequence.Contains(r.evidenceId) && !candidateSeq.Contains(r.evidenceId))
            {
                candidateSeq.Add(r.evidenceId);
            }
        }

        int n = candidateSeq.Count;
        if (n < 2) return 0f;

        int concordant = 0;
        int discordant = 0;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                int gtRankI = groundTruthSequence.IndexOf(candidateSeq[i]);
                int gtRankJ = groundTruthSequence.IndexOf(candidateSeq[j]);

                if ((gtRankI < gtRankJ && i < j) || (gtRankI > gtRankJ && i > j))
                {
                    concordant++;
                }
                else if (gtRankI != gtRankJ)
                {
                    discordant++;
                }
            }
        }

        int totalPairs = (n * (n - 1)) / 2;
        return totalPairs > 0 ? (float)(concordant - discordant) / totalPairs : 0f;
    }

    /// <summary>
    /// Exports session telemetry as a CSV row.
    /// </summary>
    public string ExportSessionTelemetryCsvRow(int itemsIdentified, int totalItems, float meanSpatialErrorCm, float nasaTlx)
    {
        float kendallTau = ComputeTimelineKendallTau();
        float identificationRatePct = totalItems > 0 ? (float)itemsIdentified / totalItems * 100f : 0f;

        StringBuilder sb = new StringBuilder();
        sb.Append($"{participantId},{experimentalCondition},{itemsIdentified},{totalItems},");
        sb.Append($"{identificationRatePct:F1},{meanSpatialErrorCm:F2},{kendallTau:F3},");
        sb.Append($"{Time.time:F1},{_totalPathLengthMeters:F2},{nasaTlx:F1}");
        return sb.ToString();
    }
}
