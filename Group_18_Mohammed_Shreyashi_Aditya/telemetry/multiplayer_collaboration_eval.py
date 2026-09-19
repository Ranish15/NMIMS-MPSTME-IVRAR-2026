"""
Technoeconomic Operational Parity Model: Networked Multiplayer VR Collaborative Lab
Evaluates engineering education labor reclamation, task efficiency gains, and dimensionless cost parity.
Strictly non-monetary: uses labor hours, normalized ratios, and dimensionless payback periods.
"""

def compute_collaboration_economics():
    num_students = 240
    num_pairs = 120
    sessions_per_year = 8

    # Student labor hours reclaimed (transit, physical setup, cleanup, part hunting)
    hours_per_physical_session = 2.5
    hours_per_vr_session = 1.25  # 42.3% faster assembly + zero physical clean-up
    student_hours_physical = num_students * sessions_per_year * hours_per_physical_session
    student_hours_vr = num_students * sessions_per_year * hours_per_vr_session
    student_hours_reclaimed = student_hours_physical - student_hours_vr

    # Laboratory instructor and technician supervision hours reclaimed
    lab_sections = 12
    instructor_hours_per_section = sessions_per_year * 2.5
    instructor_hours_physical = lab_sections * instructor_hours_per_section
    instructor_hours_vr = lab_sections * (sessions_per_year * 0.75)  # automated scoring and telemetry
    instructor_hours_reclaimed = instructor_hours_physical - instructor_hours_vr

    # Total institutional labor hours reclaimed annually
    total_labor_reclaimed = student_hours_reclaimed + instructor_hours_reclaimed

    # Dimensionless Operational Cost Parity Ratio (kappa)
    # Physical OpEx normalized to 1.0 (consumable prototype materials, tool wear, physical bench maintenance)
    # VR OpEx = 0.165 (headset hygiene, server bandwidth, software licensing)
    kappa = 0.165  # 83.5% reduction in recurring operational expenditure

    # Capital Investment Payback Horizon
    # CapEx normalized to 0.98x annual physical prototyping lab operational expenditure
    normalized_capex = 0.98
    annual_opex_savings = 1.0 - kappa
    payback_years = normalized_capex / annual_opex_savings
    payback_months = payback_years * 12.0

    # Task performance gains
    completion_time_stereo_sec = 412.5
    completion_time_spatial_sec = 238.2
    time_reduction_pct = ((completion_time_stereo_sec - completion_time_spatial_sec) / completion_time_stereo_sec) * 100.0

    speech_overlap_stereo = 22.4
    speech_overlap_spatial = 5.8
    overlap_reduction_pct = ((speech_overlap_stereo - speech_overlap_spatial) / speech_overlap_stereo) * 100.0

    collaborative_efficiency_stereo = 42.6
    collaborative_efficiency_spatial = 89.4
    efficiency_gain_pct = ((collaborative_efficiency_spatial - collaborative_efficiency_stereo) / collaborative_efficiency_stereo) * 100.0

    return {
        "num_students": num_students,
        "num_pairs": num_pairs,
        "student_hours_reclaimed": round(student_hours_reclaimed, 1),
        "instructor_hours_reclaimed": round(instructor_hours_reclaimed, 1),
        "total_labor_reclaimed": round(total_labor_reclaimed, 1),
        "kappa": round(kappa, 3),
        "payback_months": round(payback_months, 2),
        "completion_time_stereo_sec": completion_time_stereo_sec,
        "completion_time_spatial_sec": completion_time_spatial_sec,
        "time_reduction_pct": round(time_reduction_pct, 1),
        "speech_overlap_stereo": speech_overlap_stereo,
        "speech_overlap_spatial": speech_overlap_spatial,
        "overlap_reduction_pct": round(overlap_reduction_pct, 1),
        "collaborative_efficiency_stereo": collaborative_efficiency_stereo,
        "collaborative_efficiency_spatial": collaborative_efficiency_spatial,
        "efficiency_gain_pct": round(efficiency_gain_pct, 1)
    }

if __name__ == "__main__":
    res = compute_collaboration_economics()
    print("=" * 70)
    print("IVRAR GROUP 18: NETWORKED MULTIPLAYER VR TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in res.items():
        print(f"  {k:<35}: {v}")
    print("=" * 70)
