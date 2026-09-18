# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 03 - Collaborative Multi-User VR Emergency Evacuation Simulator
## Target Publication: IEEE VR / ACM VRST / Fire Safety Journal

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted using CrossRef, Scopus, IEEE Xplore, and Elsevier databases to identify foundational research on virtual reality fire drills, crowd egress dynamics, and emergency communication. Papers were screened against four inclusion criteria:
1. Peer-reviewed journal or premier conference indexing (Nature, Physical Review E, Fire Safety Journal, Computers & Education, ACSIS).
2. Formal mathematical formulations of pedestrian egress forces or doorway bottleneck flux.
3. Empirical evaluation of virtual environments for emergency training or behavioral evacuation assessment.
4. Strict absence of predatory indexing, verified via active CrossRef Digital Object Identifiers (DOIs).

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Helbing1995` | Social force model for pedestrian dynamics | Microscopic pedestrian dynamics | $\mathbf{f}_{ij} = A_i e^{(r_{ij} - d_{ij})/B_i} \mathbf{n}_{ij} + k g(r_{ij} - d_{ij}) \mathbf{n}_{ij}$ | Crowd agent navigation and collision physics | [10.1103/PhysRevE.51.4282](https://doi.org/10.1103/PhysRevE.51.4282) |
| `Helbing2000` | Simulating dynamical features of escape panic | Escape panic and jamming | Non-linear friction forces, arching patterns at narrow exits | Stairwell door bottleneck clogging and panic behavior | [10.1038/35035023](https://doi.org/10.1038/35035023) |
| `Kobes2010` | Building safety and human behaviour in fire | Pre-movement and egress behavior | Evacuation Clearance Time: $\text{ECT} = T_{\text{det}} + T_{\text{alarm}} + T_{\text{pre}} + T_{\text{move}}$ | Floor-by-floor response delay modeling | [10.1016/j.firesaf.2009.08.005](https://doi.org/10.1016/j.firesaf.2009.08.005) |
| `Kinateder2014` | Virtual Reality for Fire Evacuation Research | VR fidelity and ecological validity | Presence vs behavioral transfer, social influence in VR | Multi-user VR trial protocol and validity calibration | [10.15439/2014F94](https://doi.org/10.15439/2014F94) |
| `Feng2018` | Immersive virtual reality serious games for evacuation training | Serious games and knowledge retention | Learning outcome meta-analysis, stress induction metrics | Educational gameplay loop and debriefing metrics | [10.1016/j.compedu.2018.09.002](https://doi.org/10.1016/j.compedu.2018.09.002) |
| `Sano2017` | A pedestrian merging flow model for stair evacuation | Stairwell merging flow dynamics | Merging ratio $\gamma = F_{\text{floor}} / (F_{\text{floor}} + F_{\text{stair}})$ | Multi-floor stairwell portal flow integration | [10.1016/j.firesaf.2017.02.008](https://doi.org/10.1016/j.firesaf.2017.02.008) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Helbing & Molnar (1995) - Social Force Model for Pedestrian Dynamics
- **Core Contribution:** Introduced the continuous social force concept where pedestrian acceleration is governed by the vector sum of desired acceleration, boundary repulsion, and interpersonal repulsion.
- **Formulation:**
  $$\frac{d\mathbf{v}_i}{dt} = \frac{\mathbf{v}_i^0 - \mathbf{v}_i}{\tau_i} + \sum_{j \ne i} \mathbf{f}_{ij} + \sum_W \mathbf{f}_{iW}$$
- **Project Role:** Governs basic crowd movement in `Assets/Scripts/EvacuationEgressManager.cs`, allowing automated student agents to walk toward exits at individual desired speeds $\mathbf{v}_i^0 \approx 1.34 \text{ m/s}$.

### 3.2 Helbing, Farkas, & Vicsek (2000) - Simulating Dynamical Features of Escape Panic
- **Core Contribution:** Demonstrated that under high panic, the "faster-is-slower" effect emerges due to arching jams at doorways. When individuals push with force $k(r_{ij} - d_{ij})$, tangential sliding friction $\kappa(r_{ij} - d_{ij}) (\Delta \mathbf{v}_{ij} \cdot \mathbf{t}_{ij}) \mathbf{t}_{ij}$ grinds egress flow to a halt.
- **Project Role:** Direct physics engine for stairwell bottleneck jams. If marshals do not actively meter crowd flow, doorway discharge collapses below the NBC threshold of $1.8 \text{ p/s/m}$.

### 3.3 Kobes, Helsloot, de Vries, & Post (2010) - Building Safety & Human Behaviour in Fire
- **Core Contribution:** Identified that pre-movement delay ($T_{\text{pre}}$) frequently dominates physical walking time ($T_{\text{move}}$). Occupants exhibit social proof, ignoring alarms until confirmed by peers or marshals.
- **Project Role:** Provides empirical baselines for floor-by-floor pre-evacuation delay in `telemetry/generate_paper_figures.py` (Figure 2c), justifying the critical role of student floor marshals.

### 3.4 Kinateder, Ronchi, Nilsson, Kobes, Müller, Pauli, & Mühlberger (2014) - VR for Fire Evacuation Research
- **Core Contribution:** Established methodological guidelines for VR evacuation research, proving that behavioral choices (exit selection, route deviation) in immersive VR strongly correlate with real-world fire drill observations.
- **Project Role:** Guides experimental design, participant recruitment ($N = 50$), and ethical immersion protocols.

### 3.5 Feng, González, Amor, Lovreglio, & Cabrera-Guerrero (2018) - Immersive VR Serious Games for Evacuation Training
- **Core Contribution:** Conducted a comprehensive systematic review demonstrating that interactive VR serious games deliver statistically significant gains in evacuation decision speed and spatial orientation compared to lecture or leaflet drills.
- **Project Role:** Informs user interface and feedback mechanisms for warden wrist terminals and scorecards.

### 3.6 Sano, Ronchi, Minegishi, & Nilsson (2017) - Pedestrian Merging Flow Model for Stair Evacuation
- **Core Contribution:** Analyzed multi-story stairwell merging dynamics where occupants entering from intermediate floors compete with occupants descending from higher floors, creating localized flow chokepoints.
- **Project Role:** Calibrates stairwell portal discharge rates and inter-floor priority coordination for floor marshals.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While previous studies analyzed single-user VR drills or macroscopic agent simulations, **none integrated multi-user collaborative role-playing (head warden and floor marshals) with real-time microscopic social force physics in a unified XR network environment**. Group 03 addresses this gap directly by combining OpenXR, Netcode, and 90 Hz bottleneck telemetry.
