"""
Technoeconomic Operational Parity and Continuous Behavioral Biometric Security Model
Group 13: Continuous Head and Hand Kinematic Biometrics vs Static Enterprise 2FA
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual productive design review hours preserved,
avatar impersonation security compromises avoided, IT helpdesk lockout tickets eliminated,
and the capital investment payback horizon (in operating months) for deploying continuous behavioral biometrics
versus conventional periodic 2FA re-authentication prompts in collaborative enterprise VR.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class BiometricSecurityEconomics:
    def __init__(
        self,
        enterprise_vr_seats=1200,
        annual_collaborative_sessions_per_seat=220,
        periodic_reauth_interruptions_per_session=3,
        minutes_lost_per_reauth_event=1.5,
        impersonation_risk_reduction_pct=92.5,
        capex_ratio_baseline=1.0  # Normalized edge biometric inference pipeline and client calibration deployment
    ):
        self.seats = enterprise_vr_seats
        self.sessions_per_seat = annual_collaborative_sessions_per_seat
        self.interruptions = periodic_reauth_interruptions_per_session
        self.lost_minutes_per_prompt = minutes_lost_per_reauth_event
        self.risk_reduction = impersonation_risk_reduction_pct / 100.0
        self.capex = capex_ratio_baseline

    def compute_periodic_2fa_burden(self):
        """
        Calculates annual operational friction under periodic 2FA:
        frequent user workflow disruption, context switching latency,
        and helpdesk password/token reset tickets.
        """
        total_sessions = self.seats * self.sessions_per_seat
        annual_interruption_events = total_sessions * self.interruptions
        
        # Productive engineer labor hours lost re-authenticating inside headset:
        # (1200 seats * 220 sessions * 3 prompts * 1.5 min) / 60 = 19,800 hours
        annual_productive_hours_lost = (annual_interruption_events * self.lost_minutes_per_prompt) / 60.0
        
        # IT helpdesk re-auth lockout tickets (~0.8% of prompts require token resets):
        annual_helpdesk_tickets = annual_interruption_events * 0.008
        annual_helpdesk_labor_hours = annual_helpdesk_tickets * 0.5  # 30 minutes per ticket
        
        # Avatar identity spoofing incidents under static login (unattended headsets picked up by unauthorized peers):
        annual_spoofing_incidents = 24.0
        annual_forensic_hours = annual_spoofing_incidents * 40.0
        
        total_lost_and_remediation_hours = (
            annual_productive_hours_lost + annual_helpdesk_labor_hours + annual_forensic_hours
        )
        
        # Normalized operational expenditure factors (dimensionless baseline)
        auth_server_licensing_factor = 0.22
        helpdesk_support_ticket_factor = 0.38
        incident_forensics_overhead_factor = 0.40
        normalized_traditional_opex = (
            auth_server_licensing_factor +
            helpdesk_support_ticket_factor +
            incident_forensics_overhead_factor
        )
        
        return {
            "annual_interruption_events": annual_interruption_events,
            "annual_productive_hours_lost": annual_productive_hours_lost,
            "annual_helpdesk_labor_hours": annual_helpdesk_labor_hours,
            "annual_spoofing_incidents": annual_spoofing_incidents,
            "annual_forensic_hours": annual_forensic_hours,
            "total_lost_and_remediation_hours": total_lost_and_remediation_hours,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_continuous_biometric_burden(self):
        """
        Calculates operational overhead under continuous behavioral biometrics:
        zero workflow interruptions, passive background verification,
        and automated avatar lockout upon unauthorized handover.
        """
        # Productive hours lost: 0 (continuous passive background monitoring)
        annual_productive_hours_lost = 0.0
        
        # Residual false-rejection lockout tickets (EER < 4.5%):
        residual_helpdesk_tickets = self.seats * self.sessions_per_seat * 0.001
        annual_helpdesk_labor_hours = residual_helpdesk_tickets * 0.5
        
        # Residual spoofing incidents (92.5% prevented by continuous kinematic scoring):
        residual_spoofing_incidents = 24.0 * (1.0 - self.risk_reduction)
        annual_forensic_hours = residual_spoofing_incidents * 40.0
        
        total_lost_and_remediation_hours = (
            annual_productive_hours_lost + annual_helpdesk_labor_hours + annual_forensic_hours
        )
        
        # Continuous biometric operational factors
        edge_biometric_model_maintenance = 0.08
        kinematic_telemetry_cloud_logging = 0.06
        residual_investigation_overhead = 0.04
        normalized_biometric_opex = (
            edge_biometric_model_maintenance +
            kinematic_telemetry_cloud_logging +
            residual_investigation_overhead
        )
        
        return {
            "annual_productive_hours_lost": annual_productive_hours_lost,
            "annual_helpdesk_labor_hours": annual_helpdesk_labor_hours,
            "annual_spoofing_incidents": residual_spoofing_incidents,
            "annual_forensic_hours": annual_forensic_hours,
            "total_lost_and_remediation_hours": total_lost_and_remediation_hours,
            "normalized_biometric_opex": normalized_biometric_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual Biometric OpEx / Annual Traditional 2FA OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual Biometric OpEx)
        """
        trad = self.compute_periodic_2fa_burden()
        bio = self.compute_continuous_biometric_burden()
        
        annual_productive_hours_reclaimed = (
            trad["annual_productive_hours_lost"] - bio["annual_productive_hours_lost"]
        )
        annual_spoofing_breaches_avoided = (
            trad["annual_spoofing_incidents"] - bio["annual_spoofing_incidents"]
        )
        total_labor_hours_reclaimed = (
            trad["total_lost_and_remediation_hours"] - bio["total_lost_and_remediation_hours"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = bio["normalized_biometric_opex"] / trad["normalized_traditional_opex"]
        
        annual_operational_savings = trad["normalized_traditional_opex"] - bio["normalized_biometric_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_productive_hours_reclaimed": round(annual_productive_hours_reclaimed, 1),
            "annual_spoofing_breaches_avoided": round(annual_spoofing_breaches_avoided, 1),
            "total_labor_hours_reclaimed": round(total_labor_hours_reclaimed, 1),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1),
            "workflow_disruptions_eliminated": int(trad["annual_interruption_events"])
        }

if __name__ == "__main__":
    model = BiometricSecurityEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 13: VR BEHAVIORAL BIOMETRICS TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:42s}: {v}")
    print("=" * 70)
