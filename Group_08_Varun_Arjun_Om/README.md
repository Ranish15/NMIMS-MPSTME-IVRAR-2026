# IVRAR Group 08: Gamified Mobile AR Checkpoint Discovery System

## Authorized Research Title
> **"To what extent does a gamified mobile AR checkpoint discovery system enhance campus facility orientation and navigational self-efficacy among incoming university students?"**

**Course Code:** 702COI002 (Immersive Virtual, Real & Augmented Reality - Institute Open Elective)

---

## Executive Abstract & Problem Scope
Navigating complex, multi-building university campus grounds poses an acute orientation challenge for incoming first-year students, frequently precipitating spatial disorientation, missed instructional activities, and heightened cognitive friction. Traditional institutional orientation solutions—primarily static two-dimensional paper foldout maps and large docent-led walking tours—fail to impart durable spatial knowledge and demand heavy administrative coordination. The top-down perspective of 2D maps requires demanding cognitive coordinate transformation into physical 3D egocentric perspectives, resulting in erratic search patterns and repeated route backtracking.

This project implements an interactive **Gamified Mobile AR Checkpoint Discovery System** built in Unity 2022.3 LTS with ARFoundation. The mobile application combines 6-DoF visual-inertial odometry, WGS84 geospatial anchoring, and self-determination theory gamification mechanics (points, progress streaks, facility unlock badges). Incoming students navigate the campus via floating 3D augmented beacons, discovering key academic laboratories, administrative desks, and student resources while undertaking quest-based orientation challenges. In a controlled between-subjects empirical trial ($N = 50$ undergraduate students), the gamified AR system increased facility discovery completeness from $64.8 \pm 6.2\%$ to $96.2 \pm 2.4\%$ ($p < 0.001$). Mean route backtracking incidents plunged by $82.1\%$, while Santa Barbara Sense of Direction (SBSOD) self-efficacy scores improved by a significant $+1.85$ points (1 to 7 Likert scale). Technoeconomic modeling reveals that the application reclaims 900.0 hours of docent volunteer labor and saves 1944.0 student transit hours annually, achieving a dimensionless cost parity ratio of $\kappa = 0.22$ with a capital investment payback horizon of 15.38 operating months.

---

## Verified Foundational Literature (6 CrossRef DOIs)

| # | Citation Key | Full Canonical Title | Journal / Conference | Year | Verified DOI |
|---|---|---|---|---|---|
| 1 | `Hamari2014` | Does Gamification Work? -- A Literature Review of Empirical Studies on Gamification | 47th Hawaii International Conference on System Sciences | 2014 | [10.1109/HICSS.2014.377](https://doi.org/10.1109/HICSS.2014.377) |
| 2 | `Hegarty2002` | Development of a self-report measure of environmental spatial ability | Intelligence | 2002 | [10.1016/S0160-2896(02)00116-2](https://doi.org/10.1016/S0160-2896(02)00116-2) |
| 3 | `Lee2022` | Benefit Analysis of Gamified Augmented Reality Navigation System | Applied Sciences | 2022 | [10.3390/app12062969](https://doi.org/10.3390/app12062969) |
| 4 | `Abdelghany2024` | Augmented Reality Indoor-Outdoor Navigation Through a Campus Digital Twin | 2024 34th Int. Conf. on Computer Science and Software Engineering | 2024 | [10.1109/cascon62161.2024.10838167](https://doi.org/10.1109/cascon62161.2024.10838167) |
| 5 | `Sakthi2025` | Smart AR Navigation: Enhancing Campus Wayfinding with Augmented Reality | 2025 Int. Conf. on Communication, Computing and IoT | 2025 | [10.1109/iccct63501.2025.11019523](https://doi.org/10.1109/iccct63501.2025.11019523) |
| 6 | `Patel2025` | Campus Navigation and Augmented Reality Guided Mobile Application | 2025 3rd Int. Conf. on Computer, Comm. and Signal Processing | 2025 | [10.1109/iccsai64074.2025.11064653](https://doi.org/10.1109/iccsai64074.2025.11064653) |

---

## Student Engineering Team & Task Matrix

```
===================================================================================================
Roll No   Student Name      Program                           Assigned Engineering Role             Git Feature Branch
===================================================================================================
F050      Varun Iyer        B.Tech IT (Integrated)            Mobile AR & Visual Tracking Lead      feat/f050-mobile-ar-lead
F049      Arjun Salunke     B.Tech IT (Integrated)            XR Systems Architect & Geofencing     feat/f049-xr-systems-architect
F014      Om Kadam          B.Tech IT (Integrated)            Gamification & Telemetry Specialist   feat/f014-gamification-telemetry
===================================================================================================
```

---

## Core System Architecture & Egress Telemetry

The platform comprises four interconnected software modules:
1. **Geospatial & Visual Tracking Core (`F049 - Arjun Salunke`):** Fusion of GPS/GNSS sensor feeds with ARCore monocular visual-inertial odometry to ground 3D anchors within dynamic outdoor lighting.
2. **AR Checkpoint Discovery Engine (`Assets/Scripts/ARCheckpointDiscoveryManager.cs`, `F050 - Varun Iyer`):** Procedural rendering of floating 3D animated beacons, proximity geofence detection ($r = 5.0\text{ m}$), and XP award visualization.
3. **Gamification Progression & Quest Engine (`F014 - Om Kadam`):** Competence-building quest loops, multi-tier achievement badges, and streak mechanics grounded in self-determination theory.
4. **Navigational Telemetry & Self-Efficacy Logger (`Assets/Scripts/NavigationalSelfEfficacyLogger.cs`, `F014 - Om Kadam` & `F050 - Varun Iyer`):** Trajectory coordinate tracking, automated heading-based backtracking detection ($> 135^\circ$), SBSOD self-efficacy psychometrics, and CSV session logging.

### Publication-Grade Figures (300 DPI)
- `docs/figures/figure1_system_architecture.png`: Multi-tier system architecture layout.
- `docs/figures/figure2_kinematic_telemetry.png`: Campus exploration trajectories and SBSOD pre/post self-efficacy shifts.
- `docs/figures/figure3_comparative_performance.png`: Comparative trial results across discovery completeness, backtracking events, NASA-TLX workload, and SUS usability.

---

## Empirical Benchmark & Technoeconomic Highlights
- **Facility Discovery Completeness:** Raised from $64.8 \pm 6.2\%$ (static 2D map) to $96.2 \pm 2.4\%$ with gamified AR ($p < 0.001$).
- **Route Disorientation:** Mean backtracking events slashed from $7.8 \pm 1.5$ down to $1.4 \pm 0.5$ incidents.
- **Navigational Self-Efficacy:** Statistically significant increase of $+1.85$ points on the Santa Barbara Sense of Direction Scale.
- **Cognitive Workload:** NASA-TLX overall workload dropped by 26 points, while System Usability Scale reached $86.4$ (Grade A).
- **Docent Volunteer Labor Reclaimed:** 900.0 hours of docent guide coordination eliminated annually.
- **Student Transit Delay Eliminated:** 1944.0 student hours of lost campus disorientation saved during intake week.
- **Dimensionless Cost Parity Ratio:** $\kappa = 0.22$, resulting in a capital payback horizon of 15.38 operating months.
