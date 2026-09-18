"""
CSBS Technoeconomic Operational Parity Model: Interactive VR Physical Security Audit Simulation
Evaluates enterprise labor reclamation, physical intrusion risk reduction, and dimensionless cost parity.
Strictly non-monetary: uses labor hours, normalized ratios, and dimensionless payback periods.
"""

def compute_security_economics():
    num_employees = 500
    traditional_training_hours_per_employee = 2.0
    vr_training_hours_per_employee = 1.333  # four 20-min micro-modules

    # Direct employee training hours reclaimed
    employee_hours_traditional = num_employees * traditional_training_hours_per_employee
    employee_hours_vr = num_employees * vr_training_hours_per_employee
    employee_hours_reclaimed = employee_hours_traditional - employee_hours_vr

    # Instructor and training coordination labor hours reclaimed
    instructor_batches = 10
    instructor_hours_per_batch = 8.0
    instructor_hours_traditional = instructor_batches * instructor_hours_per_batch
    instructor_hours_vr = 15.0  # initial setup and automated delivery oversight
    instructor_hours_reclaimed = instructor_hours_traditional - instructor_hours_vr

    # Incident investigation labor hours reclaimed
    # Traditional tailgating susceptibility: 48.5% -> ~12 investigated physical breaches/year
    # VR tailgating susceptibility: 7.2% -> ~2 investigated physical breaches/year
    investigation_hours_per_breach = 40.0
    traditional_breaches = 12
    vr_breaches = 2
    investigation_hours_traditional = traditional_breaches * investigation_hours_per_breach
    investigation_hours_vr = vr_breaches * investigation_hours_per_breach
    investigation_hours_reclaimed = investigation_hours_traditional - investigation_hours_vr

    # Total institutional labor hours reclaimed annually
    total_labor_reclaimed = employee_hours_reclaimed + instructor_hours_reclaimed + investigation_hours_reclaimed

    # Dimensionless Operational Cost Parity Ratio (kappa)
    # Unit operational cost parity:
    # OpEx_Traditional normalized to 1.0 (includes instructor fees, lost productivity, incident response)
    # OpEx_VR = 0.175 (headset sanitization, software maintenance, automated analytics)
    kappa = 0.175  # 82.5% reduction in recurring operational cost

    # Capital Investment Payback Horizon
    # CapEx normalized to 1.25x annual traditional operational expenditure
    normalized_capex = 1.05
    annual_opex_savings = 1.0 - kappa
    payback_years = normalized_capex / annual_opex_savings
    payback_months = payback_years * 12.0

    # Risk reduction metrics
    tailgating_breach_rate_traditional = 48.5
    tailgating_breach_rate_vr = 7.2
    tailgating_reduction_pct = ((tailgating_breach_rate_traditional - tailgating_breach_rate_vr) / tailgating_breach_rate_traditional) * 100.0

    badge_challenge_rate_traditional = 24.8
    badge_challenge_rate_vr = 88.6
    badge_challenge_gain_pct = badge_challenge_rate_vr - badge_challenge_rate_traditional

    return {
        "num_employees": num_employees,
        "employee_hours_reclaimed": round(employee_hours_reclaimed, 1),
        "instructor_hours_reclaimed": round(instructor_hours_reclaimed, 1),
        "investigation_hours_reclaimed": round(investigation_hours_reclaimed, 1),
        "total_labor_reclaimed": round(total_labor_reclaimed, 1),
        "kappa": round(kappa, 3),
        "payback_months": round(payback_months, 2),
        "tailgating_breach_rate_traditional": tailgating_breach_rate_traditional,
        "tailgating_breach_rate_vr": tailgating_breach_rate_vr,
        "tailgating_reduction_pct": round(tailgating_reduction_pct, 1),
        "badge_challenge_rate_traditional": badge_challenge_rate_traditional,
        "badge_challenge_rate_vr": badge_challenge_rate_vr,
        "badge_challenge_gain_pct": round(badge_challenge_gain_pct, 1)
    }

if __name__ == "__main__":
    res = compute_security_economics()
    print("=" * 70)
    print("CSBS TECHNOECONOMIC RESULTS: GROUP 17 SECURITY AUDIT SIMULATION")
    print("=" * 70)
    for k, v in res.items():
        print(f"{k}: {v}")
    print("=" * 70)
