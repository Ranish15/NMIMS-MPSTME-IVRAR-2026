"""
Technoeconomic Operational Parity and Cybersecurity Training Optimization Model
Group 09: AI-Adaptive VR Social-Engineering Simulation
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual employee training hours
reclaimed, cyber breach risk exposure reduction, and the capital investment payback horizon (in operating months)
for deploying an adaptive VR social engineering simulation versus traditional passive video/slide compliance training.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class PhishingThreatRoiEval:
    def __init__(
        self,
        corporate_workforce_size=1200,
        annual_training_frequency_per_year=4,
        traditional_training_duration_hours=1.5,
        vr_simulation_duration_hours=0.5,
        baseline_phishing_click_rate=0.28,  # 28% employee susceptibility in baseline simulated phishing campaigns
        vr_trained_phishing_click_rate=0.06,  # Slashed to 6% following immersive conversational training
        capex_ratio_baseline=1.0  # Normalized initial VR hardware and conversational AI platform deployment
    ):
        self.workforce = corporate_workforce_size
        self.frequency = annual_training_frequency_per_year
        self.trad_hours = traditional_training_duration_hours
        self.vr_hours = vr_simulation_duration_hours
        self.baseline_risk = baseline_phishing_click_rate
        self.vr_risk = vr_trained_phishing_click_rate
        self.capex = capex_ratio_baseline

    def compute_traditional_security_training_burden(self):
        """
        Calculates annual labor hours and organizational overhead for traditional
        passive video lectures, LMS licensing, and residual security incident response.
        """
        annual_employee_training_hours = self.workforce * self.frequency * self.trad_hours
        
        # Security incident triage labor factor (IT team investigating frequent simulated/real phishing clicks)
        incident_response_overhead_factor = 0.55
        lms_seat_licensing_factor = 0.30
        compliance_administration_factor = 0.15
        
        normalized_traditional_opex = (
            incident_response_overhead_factor +
            lms_seat_licensing_factor +
            compliance_administration_factor
        )
        
        return {
            "annual_employee_training_hours": annual_employee_training_hours,
            "normalized_traditional_opex": normalized_traditional_opex,
            "baseline_compromise_probability": self.baseline_risk
        }

    def compute_vr_social_engineering_burden(self):
        """
        Calculates operational overhead for maintaining adaptive dialogue models,
        VR workstation maintenance, and specialized simulation scenario updates.
        """
        cloud_nlp_inference_factor = 0.12
        vr_workstation_sanitization_and_it_factor = 0.08
        scenario_asset_refresh_factor = 0.06
        
        normalized_vr_opex = (
            cloud_nlp_inference_factor +
            vr_workstation_sanitization_and_it_factor +
            scenario_asset_refresh_factor
        )
        
        annual_vr_training_hours = self.workforce * self.frequency * self.vr_hours
        
        return {
            "annual_vr_training_hours": annual_vr_training_hours,
            "normalized_vr_opex": normalized_vr_opex,
            "residual_compromise_probability": self.vr_risk
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual VR OpEx / Annual Traditional OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual VR OpEx)
        """
        trad = self.compute_traditional_security_training_burden()
        vr = self.compute_vr_social_engineering_burden()
        
        annual_employee_productive_hours_reclaimed = (
            trad["annual_employee_training_hours"] - vr["annual_vr_training_hours"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = vr["normalized_vr_opex"] / trad["normalized_traditional_opex"]
        annual_operational_savings = trad["normalized_traditional_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        # Relative reduction in breach vulnerability exposure
        vulnerability_reduction_pct = (
            (trad["baseline_compromise_probability"] - vr["residual_compromise_probability"]) /
            trad["baseline_compromise_probability"]
        ) * 100.0
        
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_employee_hours_reclaimed": round(annual_employee_productive_hours_reclaimed, 1),
            "vulnerability_reduction_pct": round(vulnerability_reduction_pct, 1),
            "payback_period_months": round(payback_months, 2),
            "training_throughput_efficiency_multiplier": round(self.trad_hours / self.vr_hours, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = PhishingThreatRoiEval()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 09: VR SOCIAL ENGINEERING ROI & EVALUATION MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:45s}: {v}")
    print("=" * 70)
