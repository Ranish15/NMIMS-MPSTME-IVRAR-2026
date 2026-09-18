"""
Technoeconomic Operational Parity and Campus Navigation Waste Reduction Model
Group 11: Hybrid Smart AR Kiosk and Mobile Handoff System
Course: IVRAR (Immersive Virtual, Real & Augmented Reality)

This module computes the dimensionless cost parity ratio (kappa), annual printed paper sheets eliminated,
reception staff inquiry hours reclaimed, visitor transit hours saved, and the capital investment payback horizon (in operating months)
for deploying hybrid smart AR kiosks with mobile WebXR handoff versus traditional printed paper maps and reception desks.

CONSTRAINTS:
- ZERO currency symbols (dimensionless ratios, labor hours, and payback months only).
- Express all financial metrics as dimensionless ratios, labor hours, and payback months.
"""

import math

class KioskHandoffEconomics:
    def __init__(
        self,
        annual_campus_visitors=8500,
        main_facility_entrances=4,
        reception_staff_count=6,
        reception_daily_inquiry_hours=5.0,
        paper_maps_printed_per_visitor=1.2,  # Includes discarded, updated, and reprinted maps
        capex_ratio_baseline=1.0  # Normalized initial smart kiosk hardware, display, and WebXR software deployment
    ):
        self.visitors = annual_campus_visitors
        self.entrances = main_facility_entrances
        self.reception_staff = reception_staff_count
        self.inquiry_hours = reception_daily_inquiry_hours
        self.paper_per_visitor = paper_maps_printed_per_visitor
        self.capex = capex_ratio_baseline

    def compute_traditional_paper_navigation_burden(self):
        """
        Calculates annual operational overhead for printing thousands of multi-color paper maps,
        reception desk wayfinding assistance labor, and waste disposal management.
        """
        # Annual printed sheets consumed: 8500 * 1.2 = 10,200 sheets annually
        annual_paper_sheets_consumed = self.visitors * self.paper_per_visitor
        
        # Receptionist labor hours dedicated to direction giving and visitor guidance:
        # 6 staff * 5.0 hours/day * 250 operational days = 7500 hours
        annual_reception_guidance_hours = self.reception_staff * self.inquiry_hours * 250.0
        
        # Lost visitor transit time under paper foldout maps:
        # 8500 visitors * 0.22 hours (13.2 minutes) lost wandering per visit = 1870 hours
        annual_visitor_transit_lost_hours = self.visitors * 0.22
        
        # Normalized operational expenditure factors (dimensionless baseline)
        paper_printing_and_inventory_factor = 0.42
        receptionist_direction_labor_factor = 0.46
        waste_recycling_and_cleanup_factor = 0.12
        normalized_traditional_opex = (
            paper_printing_and_inventory_factor +
            receptionist_direction_labor_factor +
            waste_recycling_and_cleanup_factor
        )
        
        return {
            "annual_paper_sheets_consumed": annual_paper_sheets_consumed,
            "annual_reception_guidance_hours": annual_reception_guidance_hours,
            "annual_visitor_transit_lost_hours": annual_visitor_transit_lost_hours,
            "normalized_traditional_opex": normalized_traditional_opex
        }

    def compute_smart_kiosk_webxr_burden(self):
        """
        Calculates operational overhead for maintaining kiosk touchscreens,
        cloud WebXR map hosting, and power consumption.
        """
        kiosk_screen_cleaning_and_power_factor = 0.08
        cloud_server_hosting_and_api_factor = 0.09
        bim_map_maintenance_factor = 0.05
        
        normalized_kiosk_opex = (
            kiosk_screen_cleaning_and_power_factor +
            cloud_server_hosting_and_api_factor +
            bim_map_maintenance_factor
        )
        
        # Paper sheets consumed under digital kiosk: 0 (100% paperless)
        annual_paper_sheets_consumed = 0
        
        # Receptionist hours reduced by 80% (kiosk resolves 80% of routine direction inquiries)
        residual_reception_hours = self.reception_staff * self.inquiry_hours * 250.0 * 0.20
        
        # Visitor transit loss reduced by 65% via floating AR directional guidance
        residual_visitor_transit_lost = self.visitors * 0.22 * 0.35
        
        return {
            "annual_paper_sheets_consumed": annual_paper_sheets_consumed,
            "residual_reception_hours": residual_reception_hours,
            "residual_visitor_transit_lost": residual_visitor_transit_lost,
            "normalized_kiosk_opex": normalized_kiosk_opex
        }

    def compute_operational_parity_and_payback(self):
        """
        Computes the dimensionless operational cost parity ratio (kappa) and payback horizon.
        kappa = Annual Kiosk OpEx / Annual Traditional OpEx
        Payback Period (Months) = (12 * CapEx) / (Annual Traditional OpEx - Annual Kiosk OpEx)
        """
        trad = self.compute_traditional_paper_navigation_burden()
        kiosk = self.compute_smart_kiosk_webxr_burden()
        
        annual_paper_sheets_saved = trad["annual_paper_sheets_consumed"]
        annual_reception_hours_reclaimed = (
            trad["annual_reception_guidance_hours"] - kiosk["residual_reception_hours"]
        )
        annual_visitor_hours_saved = (
            trad["annual_visitor_transit_lost_hours"] - kiosk["residual_visitor_transit_lost"]
        )
        
        # Dimensionless cost parity ratio kappa
        kappa = kiosk["normalized_kiosk_opex"] / trad["normalized_traditional_opex"]
        
        annual_operational_savings = trad["normalized_traditional_opex"] - kiosk["normalized_kiosk_opex"]
        
        if annual_operational_savings > 0:
            payback_months = (12.0 * self.capex) / annual_operational_savings
        else:
            payback_months = float("inf")
            
        return {
            "kappa_ratio": round(kappa, 4),
            "annual_paper_sheets_saved": int(annual_paper_sheets_saved),
            "annual_reception_hours_reclaimed": round(annual_reception_hours_reclaimed, 1),
            "annual_visitor_hours_saved": round(annual_visitor_hours_saved, 1),
            "payback_period_months": round(payback_months, 2),
            "normalized_opex_reduction_pct": round((1.0 - kappa) * 100.0, 1),
            "paper_waste_reduction_pct": 100.0
        }

if __name__ == "__main__":
    model = KioskHandoffEconomics()
    results = model.compute_operational_parity_and_payback()
    print("=" * 70)
    print("IVRAR GROUP 11: SMART AR KIOSK HANDOFF TECHNOECONOMIC MODEL")
    print("=" * 70)
    for k, v in results.items():
        print(f"  {k:40s}: {v}")
    print("=" * 70)
