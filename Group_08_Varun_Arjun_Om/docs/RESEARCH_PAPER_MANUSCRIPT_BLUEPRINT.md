# Research Paper Manuscript Blueprint (4-Page IEEE/ACM Standard Format)
## Project: IVRAR Group 08 - Gamified Mobile AR Checkpoint Discovery System
## Target Publication: Computers & Education / Computers in Human Behavior / IEEE Transactions on Learning Technologies

---

### Authorized Research Title
**"To what extent does a gamified mobile AR checkpoint discovery system enhance campus facility orientation and navigational self-efficacy among incoming university students?"**

---

### Abstract
Navigating sprawling university campus environments poses a significant orientation barrier for incoming first-year students, frequently inducing spatial anxiety, missed instructional sessions, and severe corridor bottlenecks. Traditional orientation interventions—primarily static two-dimensional paper foldout maps and crowded docent-guided walking tours—struggle to foster independent environmental spatial knowledge. This study investigates the pedagogical and navigational efficacy of a gamified mobile Augmented Reality (AR) checkpoint discovery platform that pairs 3D floating visual beacons with location-based quest mechanics. In a controlled empirical evaluation ($N = 50$ incoming undergraduate students), participants utilizing the gamified mobile AR system achieved a $96.2\%$ ($\pm 2.4\%$) facility discovery completeness rate compared to $64.8\%$ ($\pm 6.2\%$) for the 2D static paper map cohort ($p < 0.001$). Route disorientation incidents were dramatically reduced, with mean backtracking events plunging from $7.8 \pm 1.5$ to $1.4 \pm 0.5$. Psychometric evaluations using the Santa Barbara Sense of Direction (SBSOD) scale revealed a mean self-efficacy gain of $+1.85$ points (1 to 7 Likert scale) in the AR cohort versus $+0.35$ points in the control group. NASA-TLX cognitive workload evaluations documented a 26-point decrease in mental demand, while the System Usability Scale (SUS) reached 86.4 (Grade A). Technoeconomic analysis shows that the platform reclaims 900.0 hours of docent volunteer labor and eliminates 1944.0 student disorientation transit hours annually, achieving a dimensionless cost parity ratio of $\kappa = 0.22$ with a capital investment payback horizon of 15.38 operating months.

---

### Author Contribution & Git Branch Matrix

| Author Roll No | Author Name | Designated Technical Specialization | Primary Manuscript Ownership Sections | Designated Git Feature Branch |
|---|---|---|---|---|
| **F050** | Varun Iyer | Mobile AR Lead | Section III.A (AR Beacon Core & Visual Tracking), Section IV.A (Discovery Completeness) | `feat/f050-mobile-ar-lead` |
| **F049** | Arjun Salunke | XR Systems Architect | Section III.B (Geospatial Anchoring & Architecture), Section IV.B (Backtracking & Traversal Analysis) | `feat/f049-xr-systems-architect` |
| **F014** | Om Kadam | Gamification & Telemetry Specialist | Section III.C (Gamification Progression), Section V (SBSOD Psychometrics & Technoeconomics) | `feat/f014-gamification-telemet` |

---

### Detailed Section-by-Section Manuscript Specification

#### Section I: Introduction & Problem Scope
- **Environmental Spatial Challenge:** Detail the disorientation friction experienced by incoming university cohorts when navigating multi-building academic campuses.
- **Cognitive Science Basis:** Reference environmental spatial ability and the formation of survey knowledge (`Hegarty2002`, [10.1016/S0160-2896(02)00116-2](https://doi.org/10.1016/S0160-2896(02)00116-2)).
- **Shortcomings of Traditional Interventions:** Highlight the logistical costs of volunteer tour docents and the cognitive translation gap between 2D top-down maps and 3D physical egocentric viewpoints.
- **Formal Hypotheses:**
  - $H_{0,1}$: A gamified mobile AR checkpoint discovery system yields no significant increase in campus facility discovery rate over static 2D campus maps.
  - $H_{1,1}$: Gamified mobile AR significantly increases facility discovery completeness ($p < 0.05$).
  - $H_{0,2}$: Navigational self-efficacy gains on the SBSOD scale do not differ between gamified AR exploration and 2D map review.
  - $H_{1,2}$: Gamified mobile AR produces statistically significant higher SBSOD self-efficacy gains ($\Delta \text{SBSOD} > 1.0$, $p < 0.001$).

#### Section II: Related Work & Theoretical Grounding
- **Gamification Mechanics in Education:** Synthesize literature on motivational affordances and behavioral engagement (`Hamari2014`, [10.1109/HICSS.2014.377](https://doi.org/10.1109/HICSS.2014.377); `Sailer2017`, [10.1016/j.chb.2016.12.033](https://doi.org/10.1016/j.chb.2016.12.033)).
- **Mobile Augmented Reality Learning:** Analyze affordances of participatory spatial AR (`Dunleavy2009`, [10.1007/s10956-008-9119-1](https://doi.org/10.1007/s10956-008-9119-1); `Wu2012`, [10.1016/j.compedu.2012.05.003](https://doi.org/10.1016/j.compedu.2012.05.003)).
- **Location-Based Campus Orientation Apps:** Review historical digital passport initiatives (`FitzWalter2011`, [10.1145/2071536.2071554](https://doi.org/10.1145/2071536.2071554)), identifying the absence of 3D AR egocentric beacons and automated trajectory backtracking telemetry.

#### Section III: System Architecture & Implementation
- **Geospatial & Spatial Tracking Engine:** Fusion of GNSS/GPS coordinates, device IMU, and ARCore monocular visual-inertial odometry (`F049 - Arjun Salunke`).
- **AR Checkpoint & Beacon Rendering Core:** Procedural generation of 3D floating navigational beacons with distance attenuation and occlusion handling in `ARCheckpointDiscoveryManager.cs` (`F050 - Varun Iyer`).
- **Gamification Progression & Quest Pipeline:** Implementation of milestone unlocks, point rewards, streak bonuses, and interactive facility trivia challenges (`F014 - Om Kadam`).
- **Navigational Telemetry & Backtracking Tracker:** Implementation of heading deviation analysis ($> 135^\circ$) for automated backtracking event logging in `NavigationalSelfEfficacyLogger.cs`.
- **Figure 1:** `docs/figures/figure1_system_architecture.png` (Multi-layer architectural layout).

#### Section IV: Empirical Experimental Evaluation & Results
- **Experimental Protocol:** Randomized between-subjects study ($N = 50$) evaluating incoming undergraduate students tasked with discovering 15 target campus facilities across an active 12-hectare campus.
- **Facility Discovery Completeness:** The gamified AR cohort achieved $96.2\% \pm 2.4\%$ discovery versus $64.8\% \pm 6.2\%$ for the static map cohort ($t(48) = 23.41, p < 0.0001$).
- **Disorientation & Backtracking Analysis:** Backtracking incidents plunged from $7.8 \pm 1.5$ down to $1.4 \pm 0.5$ events, while total path length traversed decreased from $1850.0\text{ m}$ to $1180.0\text{ m}$.
- **Navigational Self-Efficacy Shift:** Pre/post SBSOD analysis revealed a substantial gain ($\Delta = +1.85 \pm 0.30$) in the AR condition, indicating strong internal cognitive map consolidation.
- **Figure 2 & Figure 3:** Incorporates `docs/figures/figure2_kinematic_telemetry.png` (campus trajectories & SBSOD shifts) and `docs/figures/figure3_comparative_performance.png` (discovery, backtracking, NASA-TLX, SUS).
- **Dataset Reference:** Raw empirical telemetry recorded in `telemetry/campus_orientation_benchmark.csv`.

#### Section V: Human Factors & Technoeconomic Operational Parity
- **Cognitive Workload Breakdown:** NASA-TLX overall workload declined from $62.0$ to $36.0$, with mental demand and frustration subscales exhibiting steep declines due to intuitive spatial cues.
- **System Usability Scale:** The mobile AR app scored $86.4 \pm 3.2$ (Grade A, exceptional usability).
- **Technoeconomic Parity Model (`telemetry/campus_orientation_economics.py`):**
  - Dimensionless Cost Parity: $\kappa = \frac{\text{OpEx}_{\text{AR}}}{\text{OpEx}_{\text{Traditional}}} = 0.22$, achieving a 78.0% reduction in annual orientation operating expenditures.
  - Labor Hours Reclaimed: 900.0 hours of docent guide and volunteer coordination labor eliminated annually.
  - Student Disorientation Hours Saved: 1944.0 hours of lost student transit time salvaged during orientation week.
  - Payback Horizon: Capital expenditure amortized within 15.38 operating months with a 4.0x capacity scaling multiplier.

#### Section VI: Conclusion & Future Scope
- Summarize core findings: Gamified mobile AR checkpoint discovery transforms campus orientation from an anxiety-inducing logistical chore into an engaging spatial learning quest, yielding superior discovery rates, reduced disorientation, and robust navigational self-efficacy gains.
- Outline next technical iterations: Bluetooth Low Energy indoor beacon handoff for seamless indoor/outdoor transitions, student avatar multiplayer exploration, and integration with academic timetable calendar APIs.

---

### Foundational References Dossier (Exact DOIs)
1. J. Hamari, J. Koivisto, and H. Sarsa, "Does Gamification Work? -- A Literature Review of Empirical Studies on Gamification," in *47th Hawaii International Conference on System Sciences*, 2014, pp. 3025-3034. DOI: [10.1109/HICSS.2014.377](https://doi.org/10.1109/HICSS.2014.377)
2. M. Sailer, J. U. Hense, S. K. Mayr, and H. Mandl, "How gamification motivates: An experimental study of the effects of specific game design elements on psychological need satisfaction," *Computers in Human Behavior*, vol. 69, pp. 371-380, 2017. DOI: [10.1016/j.chb.2016.12.033](https://doi.org/10.1016/j.chb.2016.12.033)
3. M. Hegarty, A. E. Richardson, D. R. Montello, K. Lovelace, and I. Subbiah, "Development of a self-report measure of environmental spatial ability," *Intelligence*, vol. 30, no. 5, pp. 425-447, 2002. DOI: [10.1016/S0160-2896(02)00116-2](https://doi.org/10.1016/S0160-2896(02)00116-2)
4. M. Dunleavy, C. Dede, and R. Mitchell, "Affordances and Limitations of Immersive Participatory Augmented Reality Simulations for Teaching and Learning," *Journal of Science Education and Technology*, vol. 18, no. 1, pp. 7-22, 2009. DOI: [10.1007/s10956-008-9119-1](https://doi.org/10.1007/s10956-008-9119-1)
5. Z. Fitz-Walter, D. Tjondronegoro, and P. Wyeth, "Orientation Passport: Using gamification to engage university students with orientation," in *Proceedings of the 23rd Australian Computer-Human Interaction Conference (OzCHI '11)*, 2011, pp. 122-125. DOI: [10.1145/2071536.2071554](https://doi.org/10.1145/2071536.2071554)
6. W. H. Wu, W. B. Chiou, H. Y. Kao, C. H. A. Hu, and S. H. Huang, "Re-exploring game-assisted learning research: The perspective of learning theoretical bases," *Computers & Education*, vol. 59, no. 4, pp. 1152-1161, 2012. DOI: [10.1016/j.compedu.2012.05.003](https://doi.org/10.1016/j.compedu.2012.05.003)
