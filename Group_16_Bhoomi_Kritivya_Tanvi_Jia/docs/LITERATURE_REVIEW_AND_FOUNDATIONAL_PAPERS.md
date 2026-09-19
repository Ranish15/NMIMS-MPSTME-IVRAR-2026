# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 16 - AI-Driven Immersive VR Simulation for Mass-Casualty Triage (START Protocol) Under Dynamic Industrial Hazard Conditions
## Target Publication: Academic Emergency Medicine / Prehospital and Disaster Medicine / European Journal of Emergency Medicine

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature review was executed across PubMed, CrossRef, Web of Science, and the Cochrane Database of Systematic Reviews to identify foundational clinical protocols, immersive simulation paradigms, and empirical evaluation frameworks for mass-casualty incident (MCI) triage training. Studies were evaluated against four rigorous inclusion criteria:
1. Peer-reviewed publication in premier emergency medicine, disaster preparedness, or medical simulation journals (*Academic Emergency Medicine*, *Prehospital and Disaster Medicine*, *European Journal of Emergency Medicine*, *Disaster Medicine and Public Health Preparedness*).
2. Explicit clinical evaluation of the Simple Triage and Rapid Treatment (START) algorithm or National SALT triage guidelines under mass-casualty surge scenarios.
3. Empirical assessment comparing virtual reality simulation modalities against traditional tabletop exercises, classroom didactics, or live-actor drill simulations.
4. Active, verified CrossRef Digital Object Identifier (DOI) resolution with zero broken hyperlinks.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Benson1996` | Disaster Triage: START, then SAVE-A New Method of Dynamic Triage for Victims of a Catastrophic Earthquake | Clinical triage protocol foundation | START triage decision tree: $\text{Category} = f(\text{Mobility}, \text{Respiration}, \text{Perfusion}, \text{Mental Status})$ | Core state machine logic in `STARTTriageSimulationManager.cs` | [10.1017/S1049023X0004276X](https://doi.org/10.1017/S1049023X0004276X) |
| `Wilkerson2008` | Using Virtual Reality Simulation for Mass-Casualty Incident Triage Training | VR triage simulation validation | Categorization accuracy vs drill costs; triage time per victim measurement | Immersive VR scenario design with chemical release and industrial fires | [10.1111/j.1553-2712.2008.00191.x](https://doi.org/10.1111/j.1553-2712.2008.00191.x) |
| `Baxter2026` | Mixed reality feature priorities for mass casualty incident triage simulation: a descriptive pre-post study | Mixed reality simulation features and ecological validity | Multi-sensory environmental cues and procedural immersion ratings | Environmental hazard stressors and dynamic industrial hazards | [10.1007/s10055-026-01341-2](https://doi.org/10.1007/s10055-026-01341-2) |
| `Eide2025` | Immersive Virtual Reality Simulation for Tactical Mass-Casualty Triage: An Observational Study of Usability, Realism, and Decision-Making in RAMP Training | Tactical triage decision-making under high-stress VR immersion | Stress-induced cognitive degradation and decision latency metrics | Stress inoculation and decision delay tracking in `TriageTelemetryLogger.cs` | [10.1017/dmp.2025.10289](https://doi.org/10.1017/dmp.2025.10289) |
| `Chumvanichaya2025` | A comparison of SIEVE, SORT, and START triage training effectiveness between immersive interactive 3D learning materials using virtual reality (VR-SSST) and traditional methods in mass casualty incidents | Direct comparison of VR-based START training vs traditional drills | Pre/post triage protocol accuracy, time-to-tag latency, and error distribution | Direct empirical benchmark validation for START accuracy and latency gains | [10.1186/s12245-025-00850-2](https://doi.org/10.1186/s12245-025-00850-2) |
| `Chen2025` | Bridging Simulation and Reality: Augmented Virtuality for Mass Casualty Triage Training - From Landscape Analysis to Empirical Insights | Augmented reality and virtuality for physical tactile triage | Physical-virtual spatial coordination and avatar fidelity modeling | Trainee spatial navigation and interaction design in `Assets/Scripts/` | [10.1145/3706598.3713794](https://doi.org/10.1145/3706598.3713794) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Benson, Koenig, & Schultz (1996) - Disaster Triage: START, then SAVE
- **Core Contribution:** Formulated and validated the Simple Triage and Rapid Treatment (START) protocol alongside the Secondary Assessment of Victim Endpoint (SAVE) model for catastrophic incidents, demonstrating that simple sequential evaluation of four physiological parameters (walking ability, spontaneous respiration, radial pulse/capillary refill, and mental command response) accurately categorizes victims into Minor (Green), Delayed (Yellow), Immediate (Red), and Expectant/Deceased (Black) within under 60 seconds per casualty.
- **Project Role:** Governs the clinical categorization decision tree encoded in `STARTTriageSimulationManager.cs` by `I004 - Bhoomi Bhandari`.

### 3.2 Wilkerson, Avstrekh, & Seymour (2008) - Virtual Reality Simulation for Mass-Casualty Triage
- **Core Contribution:** Established empirical evidence that immersive desktop/HMD virtual reality simulation reliably trains emergency medicine residents to apply disaster triage algorithms, demonstrating equivalent diagnostic accuracy to expensive real-world mock drills while eliminating physical consumable waste and actor coordination overhead.
- **Project Role:** Establishes the VR system design requirements and industrial disaster environment implemented in Unity by `I037 - Kritivya Mishra`.

### 3.3 Baxter, Pusa, Puthenkalam, Sjoberg, Schrom-Feiertag, & Gyllencreutz (2026) - Mixed Reality Triage Priorities
- **Core Contribution:** Identified feature priorities for disaster triage simulations in extended reality, proving that dynamic environmental stressors (smoke, ambient noise, physical debris) are essential to prevent unrealistic complacency during casualty evaluation.
- **Project Role:** Directly dictates the hazard environmental layers (smoke shaders, chemical hazard markers) implemented by `I037 - Kritivya Mishra` and `I069 - Jia Jadhav`.

### 3.4 Eide et al. (2025) - Immersive VR for Tactical MCI Triage
- **Core Contribution:** Evaluated paramedic triage performance under immersive VR simulation, establishing that immersive visual and auditory stressors elevate heart rate and induce authentic tactical decision latency.
- **Project Role:** Informs the decision latency and stress penalty metrics logged in `TriageTelemetryLogger.cs` by `I044 - Tanvi Paithankar`.

### 3.5 Chumvanichaya, Yuksen, Nuanprom, & Aramvanitch (2025) - VR-SSST START Triage Training
- **Core Contribution:** Directly compared interactive 3D VR materials against traditional didactic instruction for START triage, demonstrating that VR training yields statistically significant gains in categorization accuracy and slashes time-to-tag latency across mass casualty scenarios.
- **Project Role:** Serves as the primary modern empirical benchmark validating Group 16's target metrics (categorization accuracy $> 90\%$, triage latency $< 30\text{ s}$).

### 3.6 Chen et al. (2025) - Augmented Virtuality for Mass Casualty Triage
- **Core Contribution:** Investigated spatial interaction and user experience during mass casualty simulation, providing empirical guidelines for controller-based physical tag attachment and tactile feedback.
- **Project Role:** Governs the user interaction design, virtual triage tag dispatching, and usability scale evaluations administered by `I069 - Jia Jadhav`.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While static triage didactics and pre-scripted VR simulations have been evaluated, existing solutions suffer from three fundamental limitations:
1. **Lack of Dynamic Environmental Hazards:** Trainees are rarely exposed to evolving secondary threats (e.g., toxic gas migration, structural collapse) that force continuous reassessment and dynamic zone evacuation.
2. **Missing Real-Time Biometric & Spatial Telemetry:** Standard triage drills only record the final tag color, discarding spatial traversal path efficiency, decision latency at each physiological gate, and trainee gaze dwell times on critical indicators (e.g., cyanosis, thoracic excursion).
3. **Absence of Quantitative Technoeconomic Formulations:** Healthcare systems lack rigorous, non-monetary operational models quantifying nurse/paramedic labor reallocation and institutional training hour reclamation.

Group 16 addresses these precise gaps through an AI-driven, telemetry-logging VR mass-casualty simulation platform.
