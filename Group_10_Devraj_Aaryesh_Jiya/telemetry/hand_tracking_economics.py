"""
Technoeconomic Operational Parity and VR Hardware Optimization Model
Group 10: OpenCV Color and Fiducial Hand-Tracking Pipeline
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), hardware maintenance overhead avoided,
architectural design review workstation scalability, and the capital investment payback horizon (in operating months)
for deploying a webcam/OpenCV optical hand-tracking pipeline versus dedicated active 6-DoF VR motion controllers.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class HandTrackingEconomics:
    def __init__(
        self,
        design_studio_workstations=12,
        annual_architectural_reviews=48,
        active_controllers_per_station=2,
        controller_battery_recharge_hours_weekly=4.0,
        annual_controller_breakage_rate=0.25,  # 25% annual damage rate from drops/impacts during collaborative reviews
        capex_ratio_baseline=1.0  # Normalized initial enterprise 6-DoF VR controller fleet acquisition
    ):
        self.workstations = design_studio_workstations
        self.reviews = annual_architectural_reviews
        self.controllers_per_station = active_controllers_per_station
        self.battery_hours = controller_battery_recharge_hours_weekly
        self.breakage_rate = annual_controller_breakage_rate
        self.capex = capex_ratio_baseline

    def compute_dedicated_controller_burden(self):
        """
        Calculates annual operational overhead for battery charging/replacement cycles,
        physical controller replacement due to drops, and pairing synchronization labor.
        """
        # Annual maintenance and battery charging labor hours:
        # 12 stations * 4 hours/week * 50 working weeks = 2400 hours
        annual_maintenance_labor_hours = self.workstations * self.battery_hours * 50.0
        
        # Hardware replacement factor (normalized to baseline annual OpEx)
        hardware_drop_damage_factor = 0.52
        battery_wear_and_dock_maintenance_factor = 0.32
        firmware_pairing_troubleshooting_factor = 0.16
        
        normalized_controller_opex = (
            hardware_drop_damage_factor +
            battery_wear_and_dock_maintenance_factor +
            firmware_pairing_troubleshooting_factor
        )
        
        # Controller breakages per year: 12 * 2 * 0.25 = 6 controllers replaced annually
        annual_controller_replacements = self.workstations * self.controllers_per_station * self.breakage_rate
        
        return {
            "annual_maintenance_labor_hours": annual_maintenance_labor_hours,
            "annual_controller_replacements": annual_controller_replacements,
            "normalized_controller_opex": normalized_controller_opex
        }

    def compute_opencv_optical_tracking_burden(self):
        """
        Calculates operational overhead for color-glove replacement,
        camera lens cleaning, and lighting calibration.
        """
        fabric_glove_washing_and_replacement_factor = 0.08
        webcam_mounting_and_calibration_factor = 0.07
        software_pipeline_maintenance_factor = 0.05
        
        normalized_optical_opex = (
            fabric_glove_washing_and_replacement_factor +
            webcam_mounting_and_calibration_factor +
            software_pipeline_maintenance_factor
        )
        
        # Negligible maintenance hours: standard plug-and-play USB cameras require only periodic lens cleaning
        annual_optical_maintenance_hours = self.workstations * 0.5 * 50.0 # 0.5 hr/week
        
        return {
            "annual_optical_maintenance_hours": annual_optical_maintenance_hours,
            "normalized_optical_opex": normalized_optical_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual Optical OpEx / Annual Controller OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Controller OpEx - Annual Optical OpEx)
        """
        ctrl = self.compute_dedicated_controller_burden()
        opt = self.compute_opencv_optical_tracking_burden()
        
        annual_maintenance_hours_reclaimed = (
            ctrl["annual_maintenance_labor_hours"] - opt["annual_optical_maintenance_hours"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = opt["normalized_optical_opex"] / ctrl["normalized_controller_opex"]
        
        annual_operational_savings = ctrl["normalized_controller_opex"] - opt["normalized_optical_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        # Studio workstation deployment capacity multiplier:
        # High controller cost limits stations; low-cost optical tracking enables widespread multi-seat scaling
        deployment_capacity_multiplier = 3.5
        
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_maintenance_hours_reclaimed": round(annual_maintenance_hours_reclaimed, 1),
            "annual_hardware_breakages_avoided": round(ctrl["annual_controller_replacements"], 1),
            "payback_period_months": round(payback_months, 2),
            "deployment_capacity_multiplier": round(deployment_capacity_multiplier, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = HandTrackingEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 10: OPENCV OPTICAL HAND TRACKING TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:45s}: {v}")
    print("=" * 70)
