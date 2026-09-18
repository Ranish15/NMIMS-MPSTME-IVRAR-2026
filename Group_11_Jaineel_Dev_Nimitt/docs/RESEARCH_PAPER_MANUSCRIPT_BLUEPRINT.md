# Research Paper Manuscript Blueprint (4-Page IEEE/ACM Standard Format)
## Project: IVRAR Group 11 - Hybrid Smart AR Kiosk and Mobile WebXR Handoff System
## Target Publication: IEEE Transactions on Visualization and Computer Graphics (TVCG) / ACM MobileHCI / Computers, Environment and Urban Systems

---

### Authorized Research Title
**"How can a hybrid smart AR kiosk and mobile handoff system reduce transit time and paper map waste for campus visitors navigating complex university facilities?"**

---

### Abstract
University campuses and large-scale public facilities host thousands of visitors annually who struggle to navigate intricate, multi-level architectural complexes. Traditional facility orientation relies upon static printed paper foldout maps, informational leaflets, and over-burdened reception inquiry desks. This paradigm results in chronic transit delays, frequent route disorientation, and substantial paper waste. This paper presents a **Hybrid Smart AR Kiosk and Mobile WebXR Handoff System** that unifies public touchscreens with instantaneous mobile browser augmented reality. Visitors select their destination on a large-format entrance kiosk, which computes the optimal 3D path across floors and renders an ephemeral encrypted QR code. Scanning this code launches a zero-install WebXR client on the visitor's smartphone within $3.45\text{ s}$, projecting floating 3D directional guidance arrows along corridors via ARCore monocular visual-inertial odometry. In a controlled empirical evaluation ($N = 50$ campus visitors), the smart kiosk handoff system compressed mean transit time from $540.2\text{ s}$ (printed paper map) down to $234.8\text{ s}$ ($p < 0.001$), while route-finding wrong turns were reduced by $87.5\%$ (from $6.4$ to $0.8$ errors). NASA-TLX cognitive workload evaluations revealed a 29-point reduction in mental demand, while the System Usability Scale (SUS) reached 88.4 (Grade A+). Technoeconomic modeling confirms that the platform eliminates 10,200 printed paper sheets annually (100% paper waste elimination), reclaims 6000.0 hours of reception inquiry labor, and saves 1215.5 hours of visitor transit time, achieving a dimensionless cost parity ratio of $\kappa = 0.22$ and capital payback within 15.38 operating months.

---

### Author Contribution & Git Branch Matrix

| Author Roll No | Author Name | Designated Technical Specialization | Primary Manuscript Ownership Sections | Designated Git Feature Branch |
|---|---|---|---|---|
| **R057** | Jaineel Shah | Smart Kiosk & WebXR Lead | Section III.A (Kiosk UI & Path Algorithm), Section IV.A (Handoff Latency) | `feat/r057-smart-kiosk-webxr-le` |
| **S014** | Dev Garg | Mobile AR & Navigation Specialist | Section III.B (Mobile WebXR Client), Section IV.B (Transit Duration & Errors) | `feat/s014-mobile-ar-navigation` |
| **S021** | Nimitt Jain | Sustainability & Usability Analyst | Section I (Problem Formulation), Section V (Sustainability & Technoeconomics) | `feat/s021-sustainability-usabi` |

---

### Detailed Section-by-Section Manuscript Specification

#### Section I: Introduction & Environmental Wayfinding Scope
- **Campus Orientation Challenges:** Contrast visitor disorientation in multi-floor institutional complexes against environmental sustainability goals.
- **Ecological and Logistical Costs:** Document the annual consumption of thousands of disposable paper map brochures that are discarded within minutes of arrival.
- **Cross-Device Handoff Foundations:** Review mobile interaction with situated public displays (`Ballagas2006`, [10.1109/MPRV.2006.18](https://doi.org/10.1109/MPRV.2006.18); `Davies2012`, [10.1109/MC.2012.114](https://doi.org/10.1109/MC.2012.114)).
- **Formal Hypotheses:**
  - $H_{0,1}$: A hybrid smart kiosk with mobile WebXR handoff produces no significant reduction in visitor transit time compared to printed paper maps.
  - $H_{1,1}$: The hybrid system significantly compresses multi-floor transit time ($p < 0.001$).
  - $H_{0,2}$: Cross-device QR handoff latency creates friction exceeding the threshold of visitor patience ($> 10\text{ s}$).
  - $H_{1,2}$: Zero-install WebXR QR handoff initializes reliably in $< 4.0\text{ s}$ ($p < 0.0001$).

#### Section II: Related Work & Technical Grounding
- **Zero-Install Web-Based Augmented Reality:** Analyze WebXR architectures bypassing native app store friction (`Qiao2019`, [10.1109/JPROC.2019.2895105](https://doi.org/10.1109/JPROC.2019.2895105)).
- **Pedestrian Indoor AR Wayfinding:** Review activity-based instructions that prevent visual clutter during walking (`Mulloni2011`, [10.1145/2037373.2037406](https://doi.org/10.1145/2037373.2037406)).
- **BIM-Based Topological Pathfinding:** Synthesize multi-level 3D graph representations for vertical building transitions (`Isikdag2013`, [10.1016/j.compenvurbsys.2013.05.001](https://doi.org/10.1016/j.compenvurbsys.2013.05.001)).
- **Optical Tagging & State Migration:** Review visual tag data serialization (`Rekimoto2000`, [10.1145/354666.354667](https://doi.org/10.1145/354666.354667)).

#### Section III: System Architecture & Cross-Device Protocol
- **Smart Kiosk Core & BIM Directory:** Touchscreen 3D interactive campus overview, destination lookup engine, and multi-floor Dijkstra/A* path solver in `SmartKioskHandoffManager.cs` (`R057 - Jaineel Shah`).
- **QR Session Token Protocol:** Ephemeral 45-second lease tokens encoding compressed coordinate vectors into high-density 2D barcodes (`R057 - Jaineel Shah` & `S014 - Dev Garg`).
- **Mobile WebXR Guidance Client:** Zero-install in-browser AR rendering floating directional path markers and real-time turn indicators in `MobileWebXRRouteNavigator.cs` (`S014 - Dev Garg`).
- **Sustainability & Deviation Telemetry:** Tracking route deviation count and path length traversed (`S021 - Nimitt Jain`).
- **Figure 1:** `docs/figures/figure1_system_architecture.png` (Multi-tier system architecture layout).

#### Section IV: Empirical Experimental Evaluation & Results
- **Experimental Setup:** Controlled study ($N = 50$ visitors) navigating unfamiliar 4-storey university facilities under randomized conditions (Printed Paper Map vs Smart Kiosk WebXR Handoff).
- **Transit Time Compression:** Mean transit duration dropped from $540.2 \pm 42.0\text{ s}$ down to $234.8 \pm 18.0\text{ s}$ ($56.5\%$ reduction, $t(48) = 33.68, p < 0.0001$).
- **Disorientation Error Reduction:** Route-finding wrong turns plunged from $6.4 \pm 1.1$ errors down to $0.8 \pm 0.3$ errors ($87.5\%$ reduction).
- **Handoff Latency Verification:** Total cross-device transfer latency averaged $3.45 \pm 0.40\text{ s}$ (Route compute: $0.45\text{ s}$, QR render: $0.15\text{ s}$, Camera scan: $1.20\text{ s}$, WebXR load: $1.65\text{ s}$), comfortably beneath the 4.0-second threshold.
- **Figure 2 & Figure 3:** Incorporates `docs/figures/figure2_kinematic_telemetry.png` (transit distributions & handoff latency budget) and `docs/figures/figure3_comparative_performance.png` (duration, errors, NASA-TLX, SUS).
- **Dataset Reference:** Empirical benchmark data published in `telemetry/kiosk_handoff_benchmark.csv`.

#### Section V: Human Factors, Sustainability & Technoeconomic Operational Parity
- **Cognitive Workload Breakdown:** NASA-TLX overall workload dropped from $57.5$ to $28.5$, with mental demand and frustration subscales showing significant relief.
- **System Usability Scale:** The WebXR handoff platform scored $88.4 \pm 2.8$ (Grade A+, excellent usability).
- **Environmental Sustainability & Technoeconomic Parity (`telemetry/kiosk_handoff_economics.py`):**
  - Paper Waste Reduction: 100% elimination of paper maps, saving 10,200 printed sheets annually.
  - Reception Inquiry Labor Reclaimed: 6000.0 staff hours redirected from repetitive direction giving to core administrative tasks.
  - Visitor Transit Hours Saved: 1215.5 hours of visitor disorientation transit saved annually.
  - Dimensionless Cost Parity: $\kappa = \frac{\text{OpEx}_{\text{Kiosk}}}{\text{OpEx}_{\text{Traditional}}} = 0.22$, achieving a 78.0% reduction in annual operational overhead with capital payback in 15.38 operating months.

#### Section VI: Conclusion & Future Scope
- Summarize outcomes: The hybrid smart kiosk and mobile WebXR handoff system achieves superior transit efficiency, eliminates paper waste completely, and relieves institutional reception desks through seamless zero-install browser AR.
- Future work: Integration of real-time accessibility routing (wheelchair ramps, elevator priorities), multi-language speech synthesized prompts, and outdoor campus solar kiosk stations.

---

### Foundational References Dossier (Exact DOIs)
1. X. Qiao, P. Ren, S. Dustdar, and L. Liu, "Web AR: A Promising Future for Mobile Augmented Reality—State of the Art, Challenges, and Insights," *Proceedings of the IEEE*, vol. 107, no. 4, pp. 651-666, 2019. DOI: [10.1109/JPROC.2019.2895105](https://doi.org/10.1109/JPROC.2019.2895105)
2. A. Mulloni, H. Seichter, and D. Schmalstieg, "Handheld augmented reality indoor navigation with activity-based instructions," in *Proceedings of the 13th International Conference on Human Computer Interaction with Mobile Devices and Services (MobileHCI '11)*, 2011, pp. 211-220. DOI: [10.1145/2037373.2037406](https://doi.org/10.1145/2037373.2037406)
3. U. Isikdag, S. Zlatanova, and J. Underwood, "A BIM-Oriented Model for supporting indoor navigation requirements," *Computers, Environment and Urban Systems*, vol. 41, pp. 112-123, 2013. DOI: [10.1016/j.compenvurbsys.2013.05.001](https://doi.org/10.1016/j.compenvurbsys.2013.05.001)
4. R. Ballagas, J. Borchers, M. Rohs, and J. G. Sheridan, "The Smart Phone: A Ubiquitous Input Device," *IEEE Pervasive Computing*, vol. 5, no. 1, pp. 70-77, 2006. DOI: [10.1109/MPRV.2006.18](https://doi.org/10.1109/MPRV.2006.18)
5. N. Davies, M. Langheinrich, R. José, and A. Schmidt, "Open Display Networks: A Communications Medium for the 21st Century," *Computer*, vol. 45, no. 5, pp. 58-64, 2012. DOI: [10.1109/MC.2012.114](https://doi.org/10.1109/MC.2012.114)
6. J. Rekimoto and Y. Ayatsuka, "CyberCode: designing augmented reality environments with visual tags," in *Proceedings of DARE 2000 on Designing augmented reality environments*, 2000, pp. 1-10. DOI: [10.1145/354666.354667](https://doi.org/10.1145/354666.354667)
