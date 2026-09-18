"""
Technoeconomic Operational Parity and Clinical Exposure Therapy Scalability Model
Group 15: Bio-Adaptive VR Exposure Therapy vs Manual In-Clinic VRET
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual clinical psychologist labor hours reclaimed,
patient premature dropout events prevented, clinic throughput scaling capacity,
and the capital investment payback horizon (in operating months) for deploying automated bio-adaptive VR exposure therapy
versus traditional manual in vivo or therapist-steered exposure protocols.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class VRETAcrophobiaEconomics:
    def __init__(
        self,
        annual_acrophobia_patient_intake=180,
        manual_clinician_hours_per_patient=10.5, # 7 sessions * 1.5 hours 1-on-1 clinician attendance
        bioadaptive_clinician_hours_per_patient=1.8, # Initial intake + exit assessment; exposure automated
        manual_dropout_rate_pct=34.5,            # High attrition due to sudden overwhelming height exposure
        bioadaptive_dropout_rate_pct=6.2,        # Low attrition due to closed-loop habituation plateaus
        capex_ratio_baseline=1.0                 # Normalized VR clinical workstation, biometric sensors, and software suite
    ):
        self.patients = annual_acrophobia_patient_intake
        self.hours_manual = manual_clinician_hours_per_patient
        self.hours_bio = bioadaptive_clinician_hours_per_patient
        self.drop_manual = manual_dropout_rate_pct / 100.0
        self.drop_bio = bioadaptive_dropout_rate_pct / 100.0
        self.capex = capex_ratio_baseline

    def compute_manual_exposure_therapy_burden(self):
        """
        Calculates annual clinical operational overhead under conventional therapist-steered exposure:
        intensive 1-on-1 clinician hours, high patient dropout, and facility room utilization.
        """
        annual_clinician_hours_consumed = self.patients * self.hours_manual
        annual_patient_dropouts = self.patients * self.drop_manual
        annual_completed_treatments = self.patients * (1.0 - self.drop_manual)
        
        # Normalized operational expenditure factors (dimensionless baseline)
        clinical_psychologist_labor_factor = 0.68
        consultation_room_overhead_factor = 0.22
        administrative_rescheduling_factor = 0.10
        normalized_traditional_opex = (
            clinical_psychologist_labor_factor +
            consultation_room_overhead_factor +
            administrative_rescheduling_factor
        )
        
        return {
            "annual_clinician_hours_consumed": annual_clinician_hours_consumed,
            "annual_patient_dropouts": annual_patient_dropouts,
            "annual_completed_treatments": annual_completed_treatments,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_bioadaptive_vret_burden(self):
        """
        Calculates clinical overhead under automated bio-adaptive VRET:
        minimal clinician supervision required, automated habituation plateauing,
        and high patient treatment completion.
        """
        annual_clinician_hours_consumed = self.patients * self.hours_bio
        annual_patient_dropouts = self.patients * self.drop_bio
        annual_completed_treatments = self.patients * (1.0 - self.drop_bio)
        
        # Bio-adaptive operational maintenance factors
        vr_headset_hygiene_and_calibration = 0.08
        biometric_telemetry_software_support = 0.06
        residual_clinician_supervision = 0.05
        normalized_bioadaptive_opex = (
            vr_headset_hygiene_and_calibration +
            biometric_telemetry_software_support +
            residual_clinician_supervision
        )
        
        return {
            "annual_clinician_hours_consumed": annual_clinician_hours_consumed,
            "annual_patient_dropouts": annual_patient_dropouts,
            "annual_completed_treatments": annual_completed_treatments,
            "normalized_bioadaptive_opex": normalized_bioadaptive_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual Bio-Adaptive OpEx / Annual Traditional OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual Bio-Adaptive OpEx)
        """
        trad = self.compute_manual_exposure_therapy_burden()
        bio = self.compute_bioadaptive_vret_burden()
        
        clinician_hours_reclaimed = (
            trad["annual_clinician_hours_consumed"] - bio["annual_clinician_hours_consumed"]
        )
        patient_dropouts_prevented = (
            trad["annual_patient_dropouts"] - bio["annual_patient_dropouts"]
        )
        additional_completed_treatments = (
            bio["annual_completed_treatments"] - trad["annual_completed_treatments"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = bio["normalized_bioadaptive_opex"] / trad["normalized_traditional_opex"]
        
        annual_operational_savings = trad["normalized_traditional_opex"] - bio["normalized_bioadaptive_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "clinician_hours_reclaimed": round(clinician_hours_reclaimed, 1),
            "patient_dropouts_prevented": round(patient_dropouts_prevented, 1),
            "additional_completed_treatments": round(additional_completed_treatments, 1),
            "clinical_capacity_multiplier": round(self.hours_manual / self.hours_bio, 2),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = VRETAcrophobiaEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 15: BIO-ADAPTIVE VRET ACROPHOBIA TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:42s}: {v}")
    print("=" * 70)
