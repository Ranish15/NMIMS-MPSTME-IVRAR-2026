"""
Technoeconomic Operational Parity and Labor Reallocation Model
Group 03: University Hostel VR Emergency Evacuation Simulator
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual labor hours
reclaimed, and the capital investment payback horizon (in operating months)
for deploying multi-user VR fire evacuation drills versus full-scale physical hostel evacuations.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class HostelEvacuationEconomics:
    def __init__(
        self,
        hostel_resident_count=450,
        warden_and_marshal_count=18,
        drills_per_year=4,
        hmd_unit_count=6,
        hmd_useful_life_years=3.0,
        capex_ratio_baseline=1.0  # Normalized initial capital hardware expenditure
    ):
        self.residents = hostel_resident_count
        self.wardens = warden_and_marshal_count
        self.drills = drills_per_year
        self.hmd_count = hmd_unit_count
        self.hmd_life = hmd_useful_life_years
        self.capex = capex_ratio_baseline

    def compute_physical_drill_burden(self):
        """
        Calculates annual operational labor hours and disruption index for traditional physical drills.
        Full building evacuations require staging, false alarms, assembly count, and reset.
        """
        hours_per_resident_per_drill = 1.5  # Evacuation, assembly, head count, re-entry
        hours_per_warden_per_drill = 4.0    # Briefing, floor sweep, debriefing, compliance report
        
        annual_resident_disruption_hours = self.residents * hours_per_resident_per_drill * self.drills
        annual_warden_operational_hours = self.wardens * hours_per_warden_per_drill * self.drills
        total_physical_hours = annual_resident_disruption_hours + annual_warden_operational_hours
        
        return {
            "annual_resident_hours": annual_resident_disruption_hours,
            "annual_warden_hours": annual_warden_operational_hours,
            "total_physical_labor_hours": total_physical_hours,
            "normalized_physical_opex": 1.0  # Base reference OpEx
        }

    def compute_vr_simulation_burden(self):
        """
        Calculates annual operational requirements for VR simulation drills.
        Wardens and marshals undergo repeated immersive drills without hostel-wide disruption.
        """
        hmd_recharge_and_maintenance_factor = 0.024  # Fraction of physical drill operational cost
        scenario_software_refresh_factor = 0.028      # Scenario updates and floor plan adjustments
        
        vr_opex_ratio = hmd_recharge_and_maintenance_factor + scenario_software_refresh_factor
        
        # In VR, marshals train individually or in cohorts without halting resident studies
        annual_warden_vr_training_hours = self.wardens * 1.0 * self.drills * 2  # Double frequency, 1 hr each
        
        return {
            "vr_opex_ratio": vr_opex_ratio,
            "annual_warden_vr_hours": annual_warden_vr_training_hours,
            "resident_disruption_eliminated_hours": self.residents * 1.5 * self.drills
        }

    def evaluate_cost_parity_and_payback(self):
        """
        Evaluates dimensionless cost parity (kappa) and payback horizon in months.
        """
        phys = self.compute_physical_drill_burden()
        vr = self.compute_vr_simulation_burden()
        
        kappa = vr["vr_opex_ratio"] / phys["normalized_physical_opex"]
        annual_operational_savings_ratio = 1.0 - kappa
        
        # Payback period in operating months
        payback_months = (self.capex / annual_operational_savings_ratio) * 12.0
        
        # Total academic labor hours saved per academic year
        net_labor_hours_reclaimed = (
            phys["total_physical_labor_hours"] - vr["annual_warden_vr_hours"]
        )

        return {
            "dimensionless_cost_parity_kappa": round(kappa, 4),
            "annual_operational_savings_pct": round((1.0 - kappa) * 100.0, 2),
            "capital_payback_months": round(payback_months, 1),
            "net_academic_labor_hours_reclaimed": round(net_labor_hours_reclaimed, 1),
            "training_frequency_multiplier": 2.0  # Marshals train twice as often with zero resident friction
        }

def run_technoeconomic_analysis():
    model = HostelEvacuationEconomics()
    results = model.evaluate_cost_parity_and_payback()
    
    print("=" * 80)
    print("IVRAR GROUP 03: TECHNOECONOMIC PARITY & EVACUATION DRILL MODEL")
    print("=" * 80)
    print(f"Dimensionless Cost Parity Ratio (kappa):      {results['dimensionless_cost_parity_kappa']}")
    print(f"Annual Operational Expenditure Reduction:   {results['annual_operational_savings_pct']}%")
    print(f"Capital Payback Horizon:                   {results['capital_payback_months']} operating months")
    print(f"Net Academic Labor Hours Reclaimed/Year:    {results['net_academic_labor_hours_reclaimed']} hours")
    print(f"Drill Frequency Amplification:              {results['training_frequency_multiplier']}x operational readiness")
    print("=" * 80)

if __name__ == "__main__":
    run_technoeconomic_analysis()
