# Residential AV Integrator Rework Avoidance & CEDIA Compliance Economics
# Strictly Dimensionless Formulation (Zero Currency Symbols)
# Group: IVRAR Group 01

import math
import numpy as np

def compute_av_integration_economics():
    """
    CSBS Technoeconomic Analysis
    Compares VR pre-construction acoustic raycasting vs traditional post-build testing.
    Evaluates physical rework hours eliminated, customer sign-off acceleration,
    and dimensionless capital amortization payback horizon.
    """
    # 1. Physical AV Integration Rework Metrics
    annual_home_cinema_projects = 24.0      # Average residential AV firm volume
    baseline_rework_rate_pct = 29.2         # Percentage of rooms requiring acoustic re-treatment
    vr_simulated_rework_rate_pct = 4.2      # Residual minor aesthetic adjustments
    
    rework_projects_eliminated = annual_home_cinema_projects * ((baseline_rework_rate_pct - vr_simulated_rework_rate_pct) / 100.0)
    rework_hours_saved_per_case = 32.5       # Hours of on-site wall tear-out, re-damping, re-tuning
    annual_contractor_hours_reclaimed = rework_projects_eliminated * rework_hours_saved_per_case

    # 2. Commissioning & Turnaround Acceleration
    t_conventional_turnaround_days = 18.0    # Wall construction, measurement, re-treatment, final test
    t_vr_optimized_turnaround_days = 11.5    # First-pass build according to VR pre-calculated layout
    turnaround_acceleration_pct = ((t_conventional_turnaround_days - t_vr_optimized_turnaround_days) / t_conventional_turnaround_days) * 100.0

    # 3. Dimensionless Operational Cost Parity (Kappa)
    # Conventional physical trial-and-error rework OpEx normalized to 1.00
    c_rework_labor_materials = 1.00
    c_vr_software_workstation = 0.08
    c_vr_designer_time = 0.10
    c_vr_total_opex = c_vr_software_workstation + c_vr_designer_time
    kappa = c_vr_total_opex / c_rework_labor_materials # 0.18

    # 4. Capital Investment Amortization Horizon
    k_capex_vr_system = 0.35 # VR HMD and workstation capital normalized to annual rework expenditure
    annual_opex_savings = 1.0 - kappa # 0.82
    payback_years = k_capex_vr_system / annual_opex_savings
    payback_months = payback_years * 12.0

    print("=" * 65)
    print("IVRAR GROUP 01 - RESIDENTIAL AV ACOUSTIC INTEGRATION ECONOMICS")
    print("=" * 65)
    print(f"Annual Cinema Projects:             {annual_home_cinema_projects:.0f} installations")
    print(f"Physical Rework Elimination:        {baseline_rework_rate_pct:.1f}% -> {vr_simulated_rework_rate_pct:.1f}% ({rework_projects_eliminated:.1f} site rebuilds prevented)")
    print(f"Annual Contractor Hours Reclaimed:  {annual_contractor_hours_reclaimed:.1f} labor hours/year")
    print(f"Project Schedule Acceleration:      {turnaround_acceleration_pct:.1f}% ({t_conventional_turnaround_days:.1f}d -> {t_vr_optimized_turnaround_days:.1f}d)")
    print(f"Operational Cost Parity (Kappa):    {kappa:.2f} (82.0% operational cost advantage)")
    print(f"Dimensionless Payback Horizon:      {payback_months:.1f} operating months ({payback_years:.2f} years)")
    print("=" * 65)

if __name__ == "__main__":
    compute_av_integration_economics()
