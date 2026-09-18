"""
Technoeconomic Operational Parity and Enterprise Cybersecurity Incident Mitigation Model
Group 12: Immersive VR Phishing Simulation vs 2D Web-Based Training
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual security analyst remediation hours reclaimed,
phishing compromise incidents avoided, employee productive hours preserved, and the capital investment payback horizon (in operating months)
for deploying immersive VR experiential phishing simulations versus conventional 2D web-based video/quiz modules.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class CybersecurityTrainingEconomics:
    def __init__(
        self,
        enterprise_workforce_size=2500,
        baseline_phishing_click_rate_pct=28.5,
        web_day14_click_rate_pct=22.4,        # Standard web training decays rapidly
        vr_day14_click_rate_pct=6.8,          # VR experiential learning yields high retention
        simulated_campaigns_per_year=12,
        soc_analyst_hours_per_incident=45.0,  # Triage, forensics, credential rotation, lateral containment
        it_helpdesk_hours_per_compromise=4.5, # Password resets, token reissue, device scans
        capex_ratio_baseline=1.0              # Normalized initial VR headset fleet and software deployment
    ):
        self.workforce = enterprise_workforce_size
        self.baseline_click = baseline_phishing_click_rate_pct / 100.0
        self.web_click = web_day14_click_rate_pct / 100.0
        self.vr_click = vr_day14_click_rate_pct / 100.0
        self.campaigns = simulated_campaigns_per_year
        self.soc_hours_per_inc = soc_analyst_hours_per_incident
        self.helpdesk_hours_per_comp = it_helpdesk_hours_per_compromise
        self.capex = capex_ratio_baseline

    def compute_standard_web_training_burden(self):
        """
        Calculates annual operational overhead under standard 2D web-based annual training:
        high residual phishing click-through rate, elevated SOC forensic investigations,
        and continuous IT support ticket churn.
        """
        # Annual malicious clicks under web training:
        # 2500 employees * 12 campaigns * 0.224 click rate = 6720 click events
        annual_click_events = self.workforce * self.campaigns * self.web_click
        
        # Escalated security compromise investigations (~1.2% of clicks escalate to critical containment)
        annual_escalated_incidents = annual_click_events * 0.012
        
        # SOC analyst labor hours consumed:
        annual_soc_investigation_hours = annual_escalated_incidents * self.soc_hours_per_inc
        
        # IT helpdesk remediation hours consumed:
        annual_helpdesk_remediation_hours = annual_click_events * 0.15 * self.helpdesk_hours_per_comp
        
        # Total security operations remediation hours:
        total_security_remediation_hours = (
            annual_soc_investigation_hours + annual_helpdesk_remediation_hours
        )
        
        # Normalized operational expenditure factors (dimensionless baseline)
        web_lms_license_and_content_factor = 0.25
        soc_investigation_overhead_factor = 0.52
        employee_downtime_ticket_factor = 0.23
        normalized_traditional_opex = (
            web_lms_license_and_content_factor +
            soc_investigation_overhead_factor +
            employee_downtime_ticket_factor
        )
        
        return {
            "annual_click_events": annual_click_events,
            "annual_escalated_incidents": annual_escalated_incidents,
            "total_security_remediation_hours": total_security_remediation_hours,
            "annual_soc_investigation_hours": annual_soc_investigation_hours,
            "annual_helpdesk_remediation_hours": annual_helpdesk_remediation_hours,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_immersive_vr_training_burden(self):
        """
        Calculates operational overhead under immersive VR experiential simulation:
        drastic reduction in click-through rate and credential harvesting compromises.
        """
        # Annual malicious clicks under VR training:
        # 2500 employees * 12 campaigns * 0.068 click rate = 2040 click events
        annual_click_events = self.workforce * self.campaigns * self.vr_click
        annual_escalated_incidents = annual_click_events * 0.012
        
        annual_soc_investigation_hours = annual_escalated_incidents * self.soc_hours_per_inc
        annual_helpdesk_remediation_hours = annual_click_events * 0.15 * self.helpdesk_hours_per_comp
        total_security_remediation_hours = (
            annual_soc_investigation_hours + annual_helpdesk_remediation_hours
        )
        
        # VR operational maintenance factors
        vr_headset_hygiene_and_device_mdm = 0.10
        threat_scenario_content_updates = 0.09
        residual_incident_containment = 0.05
        normalized_vr_opex = (
            vr_headset_hygiene_and_device_mdm +
            threat_scenario_content_updates +
            residual_incident_containment
        )
        
        return {
            "annual_click_events": annual_click_events,
            "annual_escalated_incidents": annual_escalated_incidents,
            "total_security_remediation_hours": total_security_remediation_hours,
            "annual_soc_investigation_hours": annual_soc_investigation_hours,
            "annual_helpdesk_remediation_hours": annual_helpdesk_remediation_hours,
            "normalized_vr_opex": normalized_vr_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual VR OpEx / Annual Traditional Web OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Web OpEx - Annual VR OpEx)
        """
        web = self.compute_standard_web_training_burden()
        vr = self.compute_immersive_vr_training_burden()
        
        annual_compromises_avoided = web["annual_click_events"] - vr["annual_click_events"]
        annual_escalated_breaches_prevented = (
            web["annual_escalated_incidents"] - vr["annual_escalated_incidents"]
        )
        annual_remediation_hours_reclaimed = (
            web["total_security_remediation_hours"] - vr["total_security_remediation_hours"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = vr["normalized_vr_opex"] / web["normalized_traditional_opex"]
        
        annual_operational_savings = web["normalized_traditional_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_compromises_avoided": int(annual_compromises_avoided),
            "annual_escalated_breaches_prevented": round(annual_escalated_breaches_prevented, 1),
            "annual_remediation_hours_reclaimed": round(annual_remediation_hours_reclaimed, 1),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1),
            "click_rate_relative_reduction_pct": round((1.0 - (self.vr_click / self.web_click)) * 100.0, 1)
        }

if __name__ == "__main__":
    model = CybersecurityTrainingEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 12: VR PHISHING SIMULATION TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:42s}: {v}")
    print("=" * 70)
