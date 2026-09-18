"""
Defensive Security Training Technoeconomic Analysis Model
Group: IVRAR Group 02
Curriculum: Computer Science and Business Systems (CSBS)

Evaluates the operational cost parity and capital payback horizon of substituting
physical shoot-house live-fire training with immersive VR sensory-stress simulation.
All economic metrics are strictly dimensionless ratios, labor hours, and payback months.
"""

def calculate_defensive_training_economics(
    annual_trainees=120,
    live_fire_rounds_per_trainee=350,
    simulated_rounds_equivalent=800,
    shoot_house_rental_hours=16.0,
    vr_session_hours=6.0,
    trainer_hourly_parity=1.0,
    capex_vr_units=4.0
):
    """
    Computes dimensionless cost parity ratio (kappa) and payback horizon.
    """
    # 1. Physical Live-Fire Ammunition & Facility Burden (Normalized to trainer labor hours)
    # 350 rounds + range safety officer + lead decontamination equivalent:
    # 1 live round cost equivalent = 0.015 trainer labor hours
    live_ammo_hours_per_trainee = live_fire_rounds_per_trainee * 0.015
    live_facility_hours_per_trainee = shoot_house_rental_hours * 0.75 # Range operating overhead
    total_live_fire_equivalent_hours = annual_trainees * (live_ammo_hours_per_trainee + live_facility_hours_per_trainee)

    # 2. Virtual Reality Immersive Simulation Operational Burden
    # Zero consumable ammunition, electricity and headset maintenance = 0.05 trainer hours/session
    vr_operational_hours_per_trainee = vr_session_hours * 0.12
    total_vr_equivalent_hours = annual_trainees * vr_operational_hours_per_trainee

    # 3. Capital Expenditure Parity
    # VR HMDs + Tracking Rig + Haptic controllers = 450.0 equivalent trainer labor hours per station
    total_capex_equivalent_hours = capex_vr_units * 450.0

    # 4. Operational Cost Parity Ratio (Kappa)
    kappa = total_vr_equivalent_hours / total_live_fire_equivalent_hours

    # 5. Net Annual Operating Savings (Normalized Labor Hours)
    annual_savings_hours = total_live_fire_equivalent_hours - total_vr_equivalent_hours

    # 6. Dimensionless Payback Period (Operating Months)
    payback_years = total_capex_equivalent_hours / annual_savings_hours
    payback_months = payback_years * 12.0

    # 7. Training Throughput Expansion
    throughput_gain_ratio = (live_facility_hours_per_trainee / vr_session_hours)

    return {
        "annual_trainees": annual_trainees,
        "live_fire_annual_hours": round(total_live_fire_equivalent_hours, 1),
        "vr_sim_annual_hours": round(total_vr_equivalent_hours, 1),
        "annual_hours_saved": round(annual_savings_hours, 1),
        "cost_parity_ratio_kappa": round(kappa, 3),
        "payback_operating_months": round(payback_months, 1),
        "throughput_gain_multiplier": round(throughput_gain_ratio, 2)
    }

if __name__ == "__main__":
    results = calculate_defensive_training_economics()
    print("=" * 65)
    print("IVRAR GROUP 02 - DEFENSIVE SECURITY VR TRAINING ECONOMICS")
    print("=" * 65)
    print(f"Annual Security Trainees:         {results['annual_trainees']} personnel")
    print(f"Live-Fire Baseline Burden:        {results['live_fire_annual_hours']} equiv. labor hours/yr")
    print(f"VR Simulation Operating Burden:   {results['vr_sim_annual_hours']} equiv. labor hours/yr")
    print(f"Annual Labor & Resource Reclaimed:{results['annual_hours_saved']} equiv. labor hours/yr")
    print(f"Operational Cost Parity (Kappa):  {results['cost_parity_ratio_kappa']} ({(1-results['cost_parity_ratio_kappa'])*100:.1f}% operational advantage)")
    print(f"Dimensionless Payback Horizon:    {results['payback_operating_months']} operating months")
    print(f"Training Throughput Multiplier:   {results['throughput_gain_multiplier']}x acceleration")
    print("=" * 65)
