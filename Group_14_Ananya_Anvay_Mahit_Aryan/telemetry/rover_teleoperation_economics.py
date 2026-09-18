"""
Technoeconomic Operational Parity and Planetary Rover Mission Throughput Model
Group 14: Predictive Ghost-Avatar Digital Twin vs Stop-and-Go Teleoperation
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual mission science traverse kilometers gained,
teleoperation operator idle wait hours reclaimed, rock hazard strikes avoided,
and the capital investment payback horizon (in operating months) for deploying predictive ghost-avatar VR digital twins
versus conventional high-latency move-and-wait teleoperation protocols.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class RoverTeleoperationEconomics:
    def __init__(
        self,
        annual_operational_sols=300,
        sol_active_driving_hours=6.0,
        stop_and_go_velocity_mps=0.038,      # Slow move-and-wait baseline due to 1.5-5.0s delay
        predictive_vr_velocity_mps=0.165,    # Continuous confident traverse via predictive twin
        operators_per_shift=4,
        hazard_collision_reduction_pct=88.5,
        capex_ratio_baseline=1.0             # Normalized VR digital twin engineering, physics simulation, and workstation deployment
    ):
        self.sols = annual_operational_sols
        self.driving_hours = sol_active_driving_hours
        self.v_trad = stop_and_go_velocity_mps
        self.v_vr = predictive_vr_velocity_mps
        self.operators = operators_per_shift
        self.hazard_reduction = hazard_collision_reduction_pct / 100.0
        self.capex = capex_ratio_baseline

    def compute_traditional_stop_and_go_burden(self):
        """
        Calculates operational overhead under traditional stop-and-go teleoperation:
        operators spend up to 75% of operational driving time idle, waiting for
        delayed image feeds and confirmation packets.
        """
        annual_total_driving_seconds = self.sols * self.driving_hours * 3600.0
        
        # Total distance traversed annually (km):
        annual_traverse_distance_km = (self.v_trad * annual_total_driving_seconds) / 1000.0
        
        # Operator hours spent idling/waiting for latency round-trips (75% wait overhead):
        annual_operator_wait_hours = self.sols * self.driving_hours * 0.75 * self.operators
        
        # Hazard proximity strikes and wheel stall incidents annually:
        annual_hazard_incidents = 36.0
        annual_anomaly_recovery_hours = annual_hazard_incidents * 28.0
        
        total_lost_mission_hours = annual_operator_wait_hours + annual_anomaly_recovery_hours
        
        # Normalized operational expenditure factors (dimensionless baseline)
        ground_station_idle_link_factor = 0.45
        anomaly_investigation_team_factor = 0.35
        spacecraft_wear_and_soil_slip_factor = 0.20
        normalized_traditional_opex = (
            ground_station_idle_link_factor +
            anomaly_investigation_team_factor +
            spacecraft_wear_and_soil_slip_factor
        )
        
        return {
            "annual_traverse_distance_km": annual_traverse_distance_km,
            "annual_operator_wait_hours": annual_operator_wait_hours,
            "annual_hazard_incidents": annual_hazard_incidents,
            "annual_anomaly_recovery_hours": annual_anomaly_recovery_hours,
            "total_lost_mission_hours": total_lost_mission_hours,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_predictive_digital_twin_burden(self):
        """
        Calculates operational overhead under predictive ghost avatar digital twin:
        continuous path traversal, near-zero operator wait time,
        and high hazard avoidance through proactive steering ribbon visualization.
        """
        annual_total_driving_seconds = self.sols * self.driving_hours * 3600.0
        
        # Distance traversed with continuous predictive teleoperation:
        annual_traverse_distance_km = (self.v_vr * annual_total_driving_seconds) / 1000.0
        
        # Residual operator wait hours (idle overhead compressed by 85%):
        residual_operator_wait_hours = self.sols * self.driving_hours * 0.75 * self.operators * 0.15
        
        # Residual hazard incidents (88.5% avoided through anticipatory ribbon):
        residual_hazard_incidents = 36.0 * (1.0 - self.hazard_reduction)
        residual_anomaly_recovery_hours = residual_hazard_incidents * 28.0
        
        total_lost_mission_hours = residual_operator_wait_hours + residual_anomaly_recovery_hours
        
        # Predictive VR digital twin operational factors
        digital_twin_physics_calibration = 0.09
        holographic_rendering_compute = 0.07
        residual_anomaly_investigation = 0.05
        normalized_vr_opex = (
            digital_twin_physics_calibration +
            holographic_rendering_compute +
            residual_anomaly_investigation
        )
        
        return {
            "annual_traverse_distance_km": annual_traverse_distance_km,
            "residual_operator_wait_hours": residual_operator_wait_hours,
            "residual_hazard_incidents": residual_hazard_incidents,
            "residual_anomaly_recovery_hours": residual_anomaly_recovery_hours,
            "total_lost_mission_hours": total_lost_mission_hours,
            "normalized_vr_opex": normalized_vr_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual Predictive OpEx / Annual Traditional OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual Predictive OpEx)
        """
        trad = self.compute_traditional_stop_and_go_burden()
        vr = self.compute_predictive_digital_twin_burden()
        
        additional_traverse_km = vr["annual_traverse_distance_km"] - trad["annual_traverse_distance_km"]
        operator_wait_hours_reclaimed = trad["annual_operator_wait_hours"] - vr["residual_operator_wait_hours"]
        hazard_strikes_avoided = trad["annual_hazard_incidents"] - vr["residual_hazard_incidents"]
        total_mission_hours_saved = trad["total_lost_mission_hours"] - vr["total_lost_mission_hours"]
        
        # Dimensionless cost parity ratio kappa
        kappa = vr["normalized_vr_opex"] / trad["normalized_traditional_opex"]
        
        annual_operational_savings = trad["normalized_traditional_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "additional_traverse_km": round(additional_traverse_km, 1),
            "traverse_speed_multiplier": round(self.v_vr / self.v_trad, 2),
            "operator_wait_hours_reclaimed": round(operator_wait_hours_reclaimed, 1),
            "hazard_strikes_avoided": round(hazard_strikes_avoided, 1),
            "total_mission_hours_saved": round(total_mission_hours_saved, 1),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = RoverTeleoperationEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 14: PREDICTIVE ROVER TELEOPERATION TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:42s}: {v}")
    print("=" * 70)
