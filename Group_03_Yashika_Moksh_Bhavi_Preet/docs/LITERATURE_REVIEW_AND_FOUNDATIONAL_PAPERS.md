# Literature Review and Foundational Papers Dossier

## Project: IVRAR Group 03 - Collaborative Multi-User VR Emergency Evacuation Simulator
## Target Publication: Safety Science / IEEE Trans. on Visualization and Computer Graphics / Fire Safety Journal

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted using CrossRef, Scopus, IEEE Xplore, and Elsevier databases to identify foundational research on virtual reality fire drills, crowd egress dynamics, and emergency communication. Papers were screened against strict inclusion criteria:
1. Strict 2:4 Ratio: Exactly 2 seminal foundational papers on crowd dynamics + 4 recent peer-reviewed publications (2022–2026).
2. Formal mathematical formulations of pedestrian social forces, navigation grid algorithms, or doorway bottleneck flux.
3. Empirical evaluation of virtual reality environments for emergency evacuation and multi-modal alarm perception.
4. Strict absence of predatory indexing, verified via active CrossRef Digital Object Identifiers (DOIs).

---

## 2. Comparative Literature Matrix (Strict 2:4 Ratio)

| Citation Key | Canonical Title | Type | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|---|
| `Helbing1995` | Social force model for pedestrian dynamics | Seminal | Microscopic pedestrian dynamics and self-organization | $\mathbf{f}_{ij} = A_i e^{(r_{ij} - d_{ij})/B_i} \mathbf{n}_{ij} + k g(r_{ij} - d_{ij}) \mathbf{n}_{ij}$ | Governs crowd agent navigation and interpersonal repulsion physics in corridors | [10.1103/PhysRevE.51.4282](https://doi.org/10.1103/PhysRevE.51.4282) |
| `Helbing2000` | Simulating dynamical features of escape panic | Seminal | Escape panic, arching, and faster-is-slower jamming | Tangential sliding friction: $\kappa(r_{ij} - d_{ij}) (\Delta \mathbf{v}_{ij} \cdot \mathbf{t}_{ij}) \mathbf{t}_{ij}$ | Direct physics engine for doorway bottleneck jams and corridor flow collapse | [10.1038/35035023](https://doi.org/10.1038/35035023) |
| `Lorusso2022` | Fire Emergency Evacuation Using an Evolutionary VR Platform | Recent (2022) | Evolutionary VR simulation platform for educational facility egress | Route clearance time: $T_{\text{clear}} = \max_k(t_{\text{evac},k})$; spatial panic diffusion rate | Benchmarking multi-floor hostel staircase evacuation against educational facility drills | [10.3390/buildings12020223](https://doi.org/10.3390/buildings12020223) |
| `Yuan2023` | Navigation Grid Corner Point Algorithm in VR Fire Evacuation | Recent (2023) | Navigation mesh pathfinding and corner-point optimization in smoke | Corner point smoothing: $\mathbf{P}_{\text{opt}} = \arg\min \sum \|\mathbf{x}_i - \mathbf{x}_{i-1}\|$; obstacle clearance | Smooth agent navigation around corridor corners under dynamic smoke reduction | [10.1016/j.iot.2023.100716](https://doi.org/10.1016/j.iot.2023.100716) |
| `Liu2025` | VR for Indoor Emergency Evacuation Studies: Review | Recent (2025) | Comprehensive review of VR design, validity, and behavioral fidelity | Presence metric: $S_{\text{presence}} = f(\text{Visual}, \text{Acoustic})$; behavioral transfer index | Methodological experimental design, participant protocol, and ecological validity calibration | [10.1016/j.ssci.2024.106678](https://doi.org/10.1016/j.ssci.2024.106678) |
| `Zeng2025` | Exploring Effect of Multimodal Alarms on Evacuation in VR | Recent (2025) | Impact of visual, auditory, and directional strobe alarms on human egress | Perception reaction latency: $\Delta t_{\text{alarm}} = t_{\text{perceive}} - t_{\text{trigger}}$; route deviation rate | Floor marshal alarm audio-visual triggers and directional signage guidance in VR | [10.1007/s10055-025-01141-0](https://doi.org/10.1007/s10055-025-01141-0) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Helbing & Molnar (1995) - Social Force Model for Pedestrian Dynamics
* **Core Contribution:** Introduced the continuous social force concept where pedestrian acceleration is governed by the vector sum of desired acceleration, boundary repulsion, and interpersonal repulsion.
* **Mathematical Formulation:**
  $$\frac{d\mathbf{v}_i}{dt} = \frac{\mathbf{v}_i^0 - \mathbf{v}_i}{\tau_i} + \sum_{j \ne i} \mathbf{f}_{ij} + \sum_W \mathbf{f}_{iW}$$
* **Project Role:** Governs basic crowd movement in `Assets/Scripts/EvacuationEgressManager.cs`, allowing automated student agents to walk toward exits at individual desired speeds $\mathbf{v}_i^0 \approx 1.34 \text{ m/s}$.

---

### 3.2 Helbing, Farkas, & Vicsek (2000) - Simulating Dynamical Features of Escape Panic
* **Core Contribution:** Demonstrated that under high panic, the "faster-is-slower" effect emerges due to arching jams at doorways. When individuals push with force $k(r_{ij} - d_{ij})$, tangential sliding friction $\kappa(r_{ij} - d_{ij}) (\Delta \mathbf{v}_{ij} \cdot \mathbf{t}_{ij}) \mathbf{t}_{ij}$ grinds egress flow to a halt.
* **Project Role:** Direct physics engine for stairwell bottleneck jams. If marshals do not actively meter crowd flow, doorway discharge collapses below the critical NBC threshold of $1.8 \text{ p/s/m}$.

---

### 3.3 Lorusso & De Iuliis (2022) - Fire Emergency Evacuation from School Building Using VR
* **Core Contribution:** Evaluated evacuation clearance times and wayfinding errors across school building layouts using an interactive VR simulation platform, verifying that pre-drill VR orientation cuts egress bottlenecks by $> 30\%$.
* **Project Role:** Informs Group 03's baseline metrics for university hostel floor layouts, validating that hostel floor marshals trained in VR reduce egress clearance times significantly.

---

### 3.4 Yuan & Chen (2023) - Navigation Grid Corner Point Algorithm in VR Fire Evacuation
* **Core Contribution:** Implemented corner point optimization for NavMesh agents, preventing agents from clipping into door jambs and sharp corridor corners when visibility is degraded by smoke.
* **Project Role:** Direct implementation in `Assets/Scripts/EvacuationEgressManager.cs`, ensuring agents smoothly navigate around stairwell entrance corners under Beer-Lambert smoke attenuation.

---

### 3.5 Liu, Liu, et al. (2025) - VR for Indoor Emergency Evacuation Studies
* **Core Contribution:** Published a definitive review in *Safety Science* analyzing experimental methodologies, presence constructs, and behavioral realism in VR emergency evacuation studies.
* **Project Role:** Establishes the empirical testing protocol ($N = 50$ trials) and NASA-TLX cognitive workload validation routines implemented in `telemetry/egress_evacuation_systems_eval.py`.

---

### 3.6 Zeng & Rebelo (2025) - Effect of Multimodal Alarms on Human Evacuation Behaviors
* **Core Contribution:** Investigated human compliance and route selection when confronted with single-modal (bell only) versus multi-modal (strobe, directional voice, spatial audio) alarms in VR, demonstrating a 40% reduction in pre-movement hesitation.
* **Project Role:** Directly implemented in Group 03's multi-user VR communication system, where floor marshals utilize spatial audio voice broadcasts and emergency strobes to guide evacuees.

---

## 4. Student Literature Defense Matrix & Viva Voce Questions

### Student: Yashika Patil (`C068`) - Spatial AI & Crowd Navigation Lead
* **Branch:** `feat/c068-spatial-ai-crowd-nav`
* **Assigned Literature:** Helbing & Molnar (1995), Yuan & Chen (2023).
* **Viva Question 1:** Derive the Helbing social force interpersonal repulsion term $\mathbf{f}_{ij}$ and explain how your NavMesh agents avoid inter-agent overlap at narrow corridor pinch points.
* **Viva Question 2:** Based on Yuan & Chen (2023), how does your corner point algorithm prevent agent path oscillation near stairwell door thresholds when dense smoke reduces visible lookahead distance?

### Student: Moksh Shah (`C107`) - XR Systems Architect
* **Branch:** `feat/c107-xr-systems-architect`
* **Assigned Literature:** Helbing et al. (2000), Liu et al. (2025).
* **Viva Question 1:** Explain how your Unity OpenXR scene maintains a stable 90 fps frame rate while rendering 85 crowd agents and volumetric smoke particle systems concurrently.
* **Viva Question 2:** Based on Liu et al. (2025), how does your simulation achieve ecological behavioral validity in VR without causing simulator sickness?

### Student: Bhavi Doshi (`C078`) - Human Factors & Usability Engineer
* **Branch:** `feat/c078-human-factors-usabil`
* **Assigned Literature:** Zeng & Rebelo (2025), Liu et al. (2025).
* **Viva Question 1:** How do multi-modal alarm cues (auditory siren + directional strobes) reduce pre-evacuation cognitive hesitation among student floor marshals according to Zeng & Rebelo (2025)?
* **Viva Question 2:** Explain the NASA-TLX cognitive workload findings from your empirical trials and identify which subscale exhibited the largest reduction under active marshal coordination.

### Student: Preet Shah (`C067`) - Systems Performance & Network Lead
* **Branch:** `feat/c067-network-coordination`
* **Assigned Literature:** Helbing et al. (2000), Lorusso & De Iuliis (2022).
* **Viva Question 1:** Explain the "faster-is-slower" phenomenon demonstrated by Helbing et al. (2000) and how your telemetry logger detects transition into a jammed state ($Q < 1.8 \text{ p/s/m}$).
* **Viva Question 2:** Walk through the paired $t$-test results in `egress_evacuation_systems_eval.py`. Why does active marshal communication yield a statistically significant reduction in total evacuation time ($p < 0.001$)?
