# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 08 - Gamified Mobile AR Checkpoint Discovery System
## Target Publication: Computers & Education / Computers in Human Behavior / IEEE Transactions on Learning Technologies
## Course Code: 702COI002 (Institute Open Elective)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, IEEE Xplore, ScienceDirect, and Scopus to identify foundational works on mobile augmented reality in educational navigation, gamification mechanics, location-based orientation, and environmental spatial self-efficacy. Candidate works were evaluated against four strict inclusion criteria:
1. Exact **2 Seminal : 4 Recent (2022–2026)** ratio with 100% active HTTP 200 DOIs verified via CrossRef REST APIs.
2. Peer-reviewed indexing in premier human-computer interaction, spatial cognition, or educational computing venues (*Intelligence*, *Applied Sciences*, *IEEE HICSS*, *IEEE CASCON*, *IEEE ICCCT*, *IEEE ICCSAI*).
3. Rigorous theoretical or empirical modeling of gamification elements (points, badges, leaderboards) and spatial ability psychometrics (Santa Barbara Sense of Direction Scale - SBSOD).
4. Direct alignment with B.Tech Information Technology (Integrated) competencies in mobile computing, geospatial algorithms, and empirical human-centered telemetry.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Publication Year | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|---|
| `Hamari2014` | Does Gamification Work? -- A Literature Review of Empirical Studies on Gamification | 2014 (Seminal) | Empirical review of gamification | Motivational affordances $\to$ psychological outcomes $\to$ behavioral change | Framework for structuring quest milestones and XP rewards | [10.1109/HICSS.2014.377](https://doi.org/10.1109/HICSS.2014.377) |
| `Hegarty2002` | Development of a self-report measure of environmental spatial ability | 2002 (Seminal) | Environmental spatial ability | Santa Barbara Sense of Direction Scale: $\bar{S} = \frac{1}{15}\sum_{i=1}^{15} s_i$ | Primary psychometric evaluation instrument in `NavigationalSelfEfficacyLogger.cs` | [10.1016/S0160-2896(02)00116-2](https://doi.org/10.1016/S0160-2896(02)00116-2) |
| `Lee2022` | Benefit Analysis of Gamified Augmented Reality Navigation System | 2022 (Recent) | Gamified AR navigation benefits | Navigational utility index, user retention under gamification | Quantifies wayfinding engagement curves and latency reduction | [10.3390/app12062969](https://doi.org/10.3390/app12062969) |
| `Abdelghany2024` | Augmented Reality Indoor-Outdoor Navigation Through a Campus Digital Twin | 2024 (Recent) | Campus AR digital twin navigation | Seamless indoor/outdoor spatial handoff, geofenced POI anchors | Campus facility checkpoint registry and anchor coordinates | [10.1109/cascon62161.2024.10838167](https://doi.org/10.1109/cascon62161.2024.10838167) |
| `Sakthi2025` | Smart AR Navigation: Enhancing Campus Wayfinding with Augmented Reality | 2025 (Recent) | Smart campus AR wayfinding | Real-time POI visual overlay and direction heading smoothing | Visual beacon billboard rendering in `ARCheckpointDiscoveryManager.cs` | [10.1109/iccct63501.2025.11019523](https://doi.org/10.1109/iccct63501.2025.11019523) |
| `Patel2025` | Campus Navigation and Augmented Reality Guided Mobile Application | 2025 (Recent) | AR-guided mobile navigation | Mobile sensor fusion, proximity triggering latency | Geofencing proximity algorithms and backtracking event detection | [10.1109/iccsai64074.2025.11064653](https://doi.org/10.1109/iccsai64074.2025.11064653) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Hamari, Koivisto, & Sarsa (2014) - Gamification Empirical Review
- **Core Contribution:** Synthesized peer-reviewed empirical studies on gamification, demonstrating that positive motivational and educational outcomes depend on contextual alignment of game mechanics with user tasks rather than superficial point addition.
- **Project Role:** Governs the structural linkage between physical campus checkpoint discovery and experiential learning rewards, preventing gamification fatigue.

### 3.2 Hegarty, Richardson, Montello, Lovelace, & Subbiah (2002) - SBSOD Scale
- **Core Contribution:** Formulated and validated the Santa Barbara Sense of Direction (SBSOD) scale, a 15-item standardized psychometric instrument measuring individuals' self-perceived environmental spatial ability and real-world wayfinding competence.
- **Project Role:** Directly incorporated into `telemetry/generate_paper_figures.py` and `Assets/Scripts/NavigationalSelfEfficacyLogger.cs` as the primary pre- and post-test dependent variable.

### 3.3 Lee (2022) - Benefit Analysis of Gamified AR Navigation
- **Core Contribution:** Evaluated the operational benefits of integrating gamification overlays into mobile augmented reality navigation, showing significant reductions in transit search time and sustained exploratory curiosity.
- **Project Role:** Establishes the empirical baseline comparison against static 2D paper maps in Section III.

### 3.4 Abdelghany & Stroulia (2024) - Campus AR Digital Twin Navigation
- **Core Contribution:** Developed a unified indoor/outdoor campus AR navigation framework using digital twins, proving that continuous visual anchor tracking prevents spatial disorientation during transition zones between academic buildings.
- **Project Role:** Calibrates the campus checkpoint coordinate ingestion and ARFoundation anchor binding in `ARCheckpointDiscoveryManager.cs`.

### 3.5 Sakthi, Dakshatha, & V (2025) - Smart AR Campus Wayfinding
- **Core Contribution:** Implemented mobile AR wayfinding overlays with dynamic heading cues, showing that visual floating arrows reduce cognitive load during corridor decision junctures.
- **Project Role:** Informs the floating 3D beacon marker rendering and compass orientation in Unity.

### 3.6 Patel, Patel, & Dwivedi (2025) - AR-Guided Mobile Navigation
- **Core Contribution:** Validated mobile sensor fusion (GPS, compass, accelerometer) for proximity-based POI triggering, documenting sub-meter trigger accuracy in campus environments.
- **Project Role:** Governs the $5.0$ m proximity thresholding and real-time backtracking detection in `NavigationalSelfEfficacyLogger.cs`.

---

## 4. Synthesis & Research Gap Addressed
While earlier projects examined standalone 2D map apps or non-gamified AR navigation prototypes, **none systematically evaluated whether integrating 3D augmented reality floating beacons into a gamified discovery loop significantly elevates environmental spatial self-efficacy (SBSOD) and suppresses route backtracking compared to traditional static 2D campus maps**. Group 08 bridges this empirical gap.
