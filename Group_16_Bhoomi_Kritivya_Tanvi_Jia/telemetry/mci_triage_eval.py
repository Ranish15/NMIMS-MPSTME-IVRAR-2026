"""
Technoeconomic Operational Parity and Disaster Medical Triage Readiness Model
Group 16: Immersive VR START Triage Simulation vs Traditional Live-Actor Drills
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual paramedic triage rehearsal hours gained,
critical under-triage diagnostic errors prevented, and the capital investment payback horizon (in operating months)
for deploying immersive VR mass-casualty triage simulations versus logistically burdensome live-actor field drills.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class MCITriageEval:
    def __init__(
        self,
        annual_ems_trainees=450,
        live_drill_scenarios_per_year=2,        # Typically held semi-annually due to actor/venue constraints
        vr_simulation_scenarios_per_year=24,    # Bi-weekly procedural rehearsal enabled by software
        live_drill_undertriage_rate_pct=24.5,   # Critical errors classifying Immediate as Delayed
        vr_drill_undertriage_rate_pct=5.2,      # Substantial error suppression through repeated immersion
        live_drill_prep_hours_per_event=140.0,  # Moulage makeup, actor coordination, field safety officers
        capex_ratio_baseline=1.0                # Normalized VR headset fleet, multiplayer server, and scenario engine deployment
    ):
        self.trainees = annual_ems_trainees
        self.live_drills = live_drill_scenarios_per_year
        self.vr_drills = vr_simulation_scenarios_per_year
        self.under_live = live_drill_undertriage_rate_pct / 100.0
        self.under_vr = vr_drill_undertriage_rate_pct / 100.0
        self.prep_hours = live_drill_prep_hours_per_event
        self.capex = capex_ratio_baseline

    def compute_traditional_live_drill_burden(self):
        """
        Calculates annual operational overhead under live-actor field exercises:
        high logistics labor, actor compensation, site permits, and limited trainee repetitions.
        """
        # Field coordination and instructor supervision labor hours:
        annual_instructor_and_prep_hours = (self.live_drills * self.prep_hours) + (self.trainees * 6.0)
        
        # High under-triage diagnostic errors during real MCI deployment:
        annual_critical_casualty_assessments = self.trainees * 12
        annual_preventable_undertriage_errors = annual_critical_casualty_assessments * self.under_live
        
        # Normalized operational expenditure factors (dimensionless baseline)
        actor_and_moulage_logistics_factor = 0.48
        facility_and_field_rental_factor = 0.32
        coordinator_overtime_factor = 0.20
        normalized_traditional_opex = (
            actor_and_moulage_logistics_factor +
            facility_and_field_rental_factor +
            coordinator_overtime_factor
        )
        
        return {
            "annual_instructor_and_prep_hours": annual_instructor_and_prep_hours,
            "annual_preventable_undertriage_errors": annual_preventable_undertriage_errors,
            "annual_trainee_reps": self.trainees * 8, # Each trainee assesses ~8 mock casualties per year
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_vr_triage_simulation_burden(self):
        """
        Calculates operational overhead under immersive VR simulation:
        negligible actor costs, rapid automated scenario reset, and massive rehearsal volume.
        """
        # Instructor setup hours reduced by 75% via automated scenario generation:
        annual_instructor_hours = (self.live_drills * self.prep_hours * 0.25) + (self.trainees * 2.0)
        
        # Slashed under-triage errors due to mastery learning:
        annual_critical_casualty_assessments = self.trainees * 12
        residual_undertriage_errors = annual_critical_casualty_assessments * self.under_vr
        
        # VR operational maintenance factors
        vr_software_updates_and_asset_licensing = 0.07
        headset_cleaning_and_network_compute = 0.05
        residual_faculty_supervision = 0.04
        normalized_vr_opex = (
            vr_software_updates_and_asset_licensing +
            headset_cleaning_and_network_compute +
            residual_faculty_supervision
        )
        
        return {
            "annual_instructor_hours": annual_instructor_hours,
            "residual_undertriage_errors": residual_undertriage_errors,
            "annual_trainee_reps": self.trainees * 120, # Each trainee assesses 120+ virtual casualties
            "normalized_vr_opex": normalized_vr_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual VR OpEx / Annual Traditional Live Drill OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual VR OpEx)
        """
        live = self.compute_traditional_live_drill_burden()
        vr = self.compute_vr_triage_simulation_burden()
        
        instructor_hours_reclaimed = live["annual_instructor_and_prep_hours"] - vr["annual_instructor_hours"]
        undertriage_errors_prevented = (
            live["annual_preventable_undertriage_errors"] - vr["residual_undertriage_errors"]
        )
        rehearsal_volume_multiplier = vr["annual_trainee_reps"] / live["annual_trainee_reps"]
        
        # Dimensionless cost parity ratio kappa
        kappa = vr["normalized_vr_opex"] / live["normalized_traditional_opex"]
        
        annual_operational_savings = live["normalized_traditional_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "instructor_hours_reclaimed": round(instructor_hours_reclaimed, 1),
            "undertriage_errors_prevented": int(undertriage_errors_prevented),
            "rehearsal_volume_multiplier": round(rehearsal_volume_multiplier, 1),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

# Alias for backwards compatibility
TriageTrainingEconomics = MCITriageEval

if __name__ == "__main__":
    model = MCITriageEval()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 16: VR MASS CASUALTY TRIAGE TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:42s}: {v}")
    print("=" * 70)
