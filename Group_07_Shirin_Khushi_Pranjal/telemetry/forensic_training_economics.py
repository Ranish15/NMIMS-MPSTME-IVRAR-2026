"""
Technoeconomic Operational Parity and Forensic Training Optimization Model
Group 07: Interactive VR Spatial Crime Scene Reconstruction
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual instructional staging
hours reclaimed, and the capital investment payback horizon (in operating months)
for deploying reusable VR spatial crime scene simulations versus recurring physical mock crime scene staging.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class ForensicTrainingEconomics:
    def __init__(
        self,
        annual_student_investigators=180,
        cohorts_per_academic_year=6,
        crime_scene_scenarios=4,
        physical_staging_hours_per_scenario=16.0,
        physical_teardown_hours_per_scenario=8.0,
        vr_headset_workstations=10,
        headset_amortization_years=3.0,
        capex_ratio_baseline=1.0  # Normalized initial hardware and capture expenditure
    ):
        self.students = annual_student_investigators
        self.cohorts = cohorts_per_academic_year
        self.scenarios = crime_scene_scenarios
        self.staging_hours = physical_staging_hours_per_scenario
        self.teardown_hours = physical_teardown_hours_per_scenario
        self.workstations = vr_headset_workstations
        self.amortization_years = headset_amortization_years
        self.capex = capex_ratio_baseline

    def compute_physical_mock_staging_burden(self):
        """
        Calculates annual labor hours and consumable overhead for physical mock crime scene staging:
        staging mannequins, mock ballistic casings, synthetic bio-fluids, and physical room reservation locks.
        """
        # Staging and reset hours per cohort across all scenarios
        annual_staging_labor_hours = (
            self.cohorts * self.scenarios * (self.staging_hours + self.teardown_hours)
        )
        
        # Physical facility lockout hours (room locked from other academic uses during investigation exercises)
        annual_facility_lockout_hours = self.cohorts * self.scenarios * 40.0 # 40 hours per scenario exercise
        
        # Consumable materials and prop replacement factor (normalized to baseline annual OpEx)
        consumable_prop_factor = 0.42
        instructional_technician_labor_factor = 0.58
        normalized_physical_opex = consumable_prop_factor + instructional_technician_labor_factor
        
        return {
            "annual_staging_labor_hours": annual_staging_labor_hours,
            "annual_facility_lockout_hours": annual_facility_lockout_hours,
            "normalized_physical_opex": normalized_physical_opex
        }

    def compute_vr_spatial_reconstruction_burden(self):
        """
        Calculates operational overhead for digital photogrammetry/LiDAR assets,
        HMD sanitization, and software maintenance.
        """
        hmd_maintenance_and_sanitization_factor = 0.12
        scenario_3d_asset_refresh_factor = 0.08
        cloud_storage_and_licensing_factor = 0.05
        
        normalized_vr_opex = (
            hmd_maintenance_and_sanitization_factor +
            scenario_3d_asset_refresh_factor +
            cloud_storage_and_licensing_factor
        )
        
        # Zero staging labor hours: virtual crime scenes reset instantaneously with software reset
        instantaneous_reset_hours_per_exercise = 0.05
        annual_vr_operational_hours = self.cohorts * self.scenarios * instantaneous_reset_hours_per_exercise
        
        return {
            "annual_vr_operational_hours": annual_vr_operational_hours,
            "normalized_vr_opex": normalized_vr_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual VR OpEx / Annual Physical OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Physical OpEx - Annual VR OpEx)
        """
        phys = self.compute_physical_mock_staging_burden()
        vr = self.compute_vr_spatial_reconstruction_burden()
        
        annual_net_labor_hours_reclaimed = phys["annual_staging_labor_hours"] - vr["annual_vr_operational_hours"]
        facility_hours_unlocked = phys["annual_facility_lockout_hours"]
        
        # Dimensionless cost parity ratio kappa
        kappa = vr["normalized_vr_opex"] / phys["normalized_physical_opex"]
        
        annual_operational_savings = phys["normalized_physical_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        # Training throughput multiplier: number of investigation attempts possible per week
        # Physical crime scene allows 1 group at a time; VR allows concurrent multi-station runs
        physical_cohort_capacity_per_day = 2
        vr_cohort_capacity_per_day = 10
        throughput_multiplier = vr_cohort_capacity_per_day / physical_cohort_capacity_per_day
        
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_labor_hours_reclaimed": round(annual_net_labor_hours_reclaimed, 1),
            "annual_facility_hours_unlocked": round(facility_hours_unlocked, 1),
            "payback_period_months": round(payback_months, 2),
            "throughput_multiplier": round(throughput_multiplier, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = ForensicTrainingEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 07: FORENSIC SPATIAL VR TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:35s}: {v}")
    print("=" * 70)
