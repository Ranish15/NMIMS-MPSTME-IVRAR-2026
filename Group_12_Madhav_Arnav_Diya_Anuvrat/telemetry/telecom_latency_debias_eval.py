"""
Technoeconomic Operational Parity, Telecom Latency, and Enterprise Cybersecurity Incident Mitigation Model
Group 12: Immersive VR Phishing Simulation vs 2D Web-Based Training
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual security analyst remediation hours reclaimed,
phishing compromise incidents avoided, employee productive hours preserved, telecom streaming latency limits, and the capital investment payback horizon (in operating months)
for deploying immersive VR experiential phishing simulations versus conventional 2D web-based video/quiz modules.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class TelecomLatencyDebiasEval:
    def __init__(
        self,
        enterprise_workforce_size=2500,
        baseline_phishing_click_rate_pct=28.5,
        web_day14_click_rate_pct=22.4,        # Standard web training decays rapidly
        vr_day14_click_rate_pct=6.8,          # VR experiential learning yields high retention
        simulated_campaigns_per_year=12,
        soc_analyst_hours_per_incident=45.0,  # Triage, forensics, credential rotation, lateral containment
        it_helpdesk_hours_per_compromise=4.5, # Password resets, token reissue, device scans
        capex_ratio_baseline=1.0,             # Normalized initial VR headset fleet and software deployment
        network_rtt_latency_budget_ms=20.0     # Target telecom round-trip streaming latency for cloud XR
    ):
        self.workforce = enterprise_workforce_size
        self.baseline_click = baseline_phishing_click_rate_pct / 100.0
        self.web_click = web_day14_click_rate_pct / 100.0
        self.vr_click = vr_day14_click_rate_pct / 100.0
        self.campaigns = simulated_campaigns_per_year
        self.soc_hours_per_inc = soc_analyst_hours_per_incident
        self.helpdesk_hours_per_comp = it_helpdesk_hours_per_compromise
        self.capex = capex_ratio_baseline
        self.latency_budget_ms = network_rtt_latency_budget_ms

    def compute_standard_web_training_burden(self):
        """
        Calculates annual operational overhead under standard 2D web-based annual training:
        high residual phishing click-through rate, elevated SOC forensic investigations,
        and continuous IT support ticket churn.
        """
        annual_click_events = self.workforce * self.campaigns * self.web_click
        annual_escalated_incidents = annual_click_events * 0.012
        annual_soc_investigation_hours = annual_escalated_incidents * self.soc_hours_per_inc
        annual_helpdesk_remediation_hours = annual_click_events * 0.15 * self.helpdesk_hours_per_comp
        
        total_security_remediation_hours = (
            annual_soc_investigation_hours + annual_helpdesk_remediation_hours
        )
        
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
        annual_click_events = self.workforce * self.campaigns * self.vr_click
        annual_escalated_incidents = annual_click_events * 0.012
        annual_soc_investigation_hours = annual_escalated_incidents * self.soc_hours_per_inc
        annual_helpdesk_remediation_hours = annual_click_events * 0.15 * self.helpdesk_hours_per_comp
        total_security_remediation_hours = (
            annual_soc_investigation_hours + annual_helpdesk_remediation_hours
        )
        
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

    def compute_telecom_and_operational_parity(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa), payback horizon,
        and telecom latency transmission limits.
        """
        web = self.compute_standard_web_training_burden()
        vr = self.compute_immersive_vr_training_burden()
        
        clicks_avoided = web["annual_click_events"] - vr["annual_click_events"]
        escalated_incidents_prevented = web["annual_escalated_incidents"] - vr["annual_escalated_incidents"]
        soc_analyst_hours_reclaimed = web["total_security_remediation_hours"] - vr["total_security_remediation_hours"]
        
        kappa = vr["normalized_vr_opex"] / web["normalized_traditional_opex"]
        annual_operational_savings = web["normalized_traditional_opex"] - vr["normalized_vr_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        retention_resilience_ratio = (1.0 - (self.vr_click / self.web_click)) * 100.0
        
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_compromise_clicks_avoided": round(clicks_avoided, 1),
            "annual_escalated_breaches_prevented": round(escalated_incidents_prevented, 1),
            "annual_security_remediation_hours_reclaimed": round(soc_analyst_hours_reclaimed, 1),
            "payback_period_months": round(payback_months, 2),
            "retention_resilience_advantage_pct": round(retention_resilience_ratio, 1),
            "network_latency_budget_ms": self.latency_budget_ms,
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = TelecomLatencyDebiasEval()
    results = model.compute_telecom_and_operational_parity()
    print("=" * 75)
    print("IVRAR GROUP 12: TELECOM LATENCY & CYBERSECURITY DEBIAS EVALUATION MODEL")
    print("=" * 75)
    for k, v in results.items():
        print(f"  {k:45s}: {v}")
    print("=" * 75)
