"""
Technoeconomic Operational Parity and Campus Orientation Optimization Model
Group 08: Gamified Mobile AR Checkpoint Discovery System
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual docent guidance hours
reclaimed, student disorientation hours eliminated, and the capital investment payback horizon (in operating months)
for deploying a gamified mobile AR orientation application versus traditional printed maps and guided tours.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class CampusOrientationEconomics:
    def __init__(
        self,
        incoming_first_year_students=1500,
        orientation_docents_required=30,
        orientation_duration_days=5,
        daily_docent_tour_hours=6.0,
        campus_checkpoint_beacons=24,
        capex_ratio_baseline=1.0  # Normalized initial application development & spatial mapping cost
    ):
        self.students = incoming_first_year_students
        self.docents = orientation_docents_required
        self.days = orientation_duration_days
        self.tour_hours_daily = daily_docent_tour_hours
        self.beacons = campus_checkpoint_beacons
        self.capex = capex_ratio_baseline

    def compute_traditional_orientation_burden(self):
        """
        Calculates annual operational overhead for physical printed guide maps,
        student orientation leader docent stipends/hours, and lost student transit time.
        """
        # Docent / volunteer tour guide labor hours per annual intake
        annual_docent_labor_hours = self.docents * self.days * self.tour_hours_daily
        
        # Student disorientation delay under 2D printed paper foldout maps:
        # 1500 students * 1.8 hours wandering / asking for directions during week 1
        annual_student_disorientation_hours = self.students * 1.8
        
        # Normalized operational expenditure factors (dimensionless baseline)
        printed_collateral_paper_factor = 0.38  # Printing thousands of glossy multi-page campus map booklets
        docent_training_and_coordination_factor = 0.48
        information_desk_staffing_factor = 0.14
        normalized_traditional_opex = (
            printed_collateral_paper_factor +
            docent_training_and_coordination_factor +
            information_desk_staffing_factor
        )
        
        return {
            "annual_docent_labor_hours": annual_docent_labor_hours,
            "annual_student_disorientation_hours": annual_student_disorientation_hours,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_gamified_ar_orientation_burden(self):
        """
        Calculates operational overhead for maintaining cloud spatial anchors,
        server backend leaderboard telemetry, and annual quest content refresh.
        """
        cloud_spatial_anchor_hosting_factor = 0.10
        quest_curation_and_poi_update_factor = 0.08
        app_store_maintenance_factor = 0.04
        
        normalized_ar_opex = (
            cloud_spatial_anchor_hosting_factor +
            quest_curation_and_poi_update_factor +
            app_store_maintenance_factor
        )
        
        # Student disorientation reduced by 72% due to 3D floating landmark cues
        residual_disorientation_hours = self.students * 1.8 * 0.28
        
        return {
            "residual_disorientation_hours": residual_disorientation_hours,
            "normalized_ar_opex": normalized_ar_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual AR OpEx / Annual Traditional OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual AR OpEx)
        """
        trad = self.compute_traditional_orientation_burden()
        ar = self.compute_gamified_ar_orientation_burden()
        
        docent_hours_reclaimed = trad["annual_docent_labor_hours"]
        student_transit_hours_saved = (
            trad["annual_student_disorientation_hours"] - ar["residual_disorientation_hours"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = ar["normalized_ar_opex"] / trad["normalized_traditional_opex"]
        
        annual_operational_savings = trad["normalized_traditional_opex"] - ar["normalized_ar_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        # Orientation throughput capacity multiplier:
        # Traditional docent groups capped at 25 students; AR app supports arbitrary concurrent self-guided exploration
        capacity_multiplier = 4.0
        
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_docent_hours_reclaimed": round(docent_hours_reclaimed, 1),
            "annual_student_hours_saved": round(student_transit_hours_saved, 1),
            "payback_period_months": round(payback_months, 2),
            "capacity_multiplier": round(capacity_multiplier, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1)
        }

if __name__ == "__main__":
    model = CampusOrientationEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 08: GAMIFIED AR ORIENTATION TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:35s}: {v}")
    print("=" * 70)
