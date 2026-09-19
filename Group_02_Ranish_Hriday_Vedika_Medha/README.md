# IVRAR Group 02: Immersive VR Sensory-Stress Defensive Security Training under Low Visibility

---

## 1. Authorized Research Title & Problem Statement

> "How can an immersive VR sensory-stress simulation improve target prioritization and reaction latency for defensive security trainees under simulated low-visibility nighttime conditions?"

### Core Engineering Focus
* **Interactive VR Environment:** Unity OpenXR tactical breach environment simulating nighttime mesopic illumination ($0.5-5.0\text{ lux}$) and volumetric fog (`Assets/index.html` and `Assets/Scripts/`).
* **Sensory-Stress Inoculation:** High-intensity acoustic startle cues (85-95 dB) and peripheral strobe glares during critical engagement windows (`Assets/Scripts/SensoryStressTargetManager.cs`).
* **Signal Detection Telemetry:** Continuous 90 Hz logging of millisecond reaction times, hit rates, false alarm rates, and Signal Detection Theory metrics ($d', \beta$) (`Assets/Scripts/ReactionLatencyTelemetryLogger.cs`).
* **Techno-Managerial Systems Evaluation:** Dimensionless operational model analyzing live-fire ammunition replacement and shoot-house facility rental substitution (`telemetry/sensory_stress_defense_roi.py`).
* **Human Factors & Cognitive Workload:** Standardized assessments via NASA-TLX, Kennedy SSQ cybersickness, and Situational Awareness Global Assessment Technique (SAGAT) (`telemetry/`).

---

## 2. Student Engineering Team Matrix

| Roll No | SAP ID | Student Name | Degree Programme | Technical Role | Assigned Git Branch | Core Viva Defense Area |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `N024` | `70472400145` | **Ranish Devadiga** | MBA (Tech) (Computer) | Lead XR Systems Architect | `feat/n024-xr-systems-architect` | Unity OpenXR nighttime mesopic rendering, volumetric low-light fog shaders, dynamic flashlight cone inverse-square falloff, peripheral strobe startle cues, and 6-DoF defensive weapon rig. |
| `N042` | `70472400224` | **Hriday Jain** | MBA (Tech) (Computer) | Human Factors & Usability Engineer | `feat/n042-human-factors-usabil` | Multi-dimensional NASA-TLX cognitive workload assessment, Situational Awareness Global Assessment Technique (SAGAT), Kennedy SSQ cybersickness monitoring, and stress inoculation training (SIT) efficacy. |
| `N047` | `70472400031` | **Vedika Kaki** | MBA (Tech) (Computer) | Spatial Telemetry & Data Lead | `feat/n047-spatial-telemetry-da` | Signal Detection Theory (SDT) sensitivity index (d') and decision criterion (beta), shoot/don't-shoot millisecond reaction latency logging, false alarm error rate reduction, and paired t-test hypothesis testing. |
| `N062` | `70472400154` | **Medha Mishra** | MBA (Tech) (Computer) | Techno-Managerial Product Manager | `feat/n062-technoeconomic-produ` | Defensive training operational economics, live-fire ammunition consumption substitution (350 rounds/trainee), physical shoot-house facility rental replacement, operational cost parity ($\kappa = 0.042$), and capital payback horizon (10.9 months). |

---

## 3. Foundational Literature Benchmarks (Strict 2:4 Ratio)

The research project is theoretically anchored on six foundational peer-reviewed publications validated against global bibliographic databases (IEEE, Taylor & Francis, Springer, MDPI, Elsevier).

| # | Author (Year) | Type | Paper Title | Venue / Indexing | Active DOI Link | Primary Extracted Formulation | Student Lead |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **Oudejans (2008)** | Seminal | *Reality-based practice under pressure improves handgun shooting performance of police officers* | Ergonomics | [10.1080/00140130701577435](https://doi.org/10.1080/00140130701577435) | `P(hit | A_high) = \Phi(\beta_0 + \beta_1 T - \beta_2 HR)` | Vedika Kaki (N047) & Medha Mishra (N062) |
| 2 | **Stanislaw & Todorov (1999)** | Seminal | *Calculation of signal detection theory measures* | Behavior Research Methods, Instruments, & Computers | [10.3758/BF03207704](https://doi.org/10.3758/BF03207704) | `d' = z(H) - z(F), \beta = \exp(-d' \cdot c)` | Vedika Kaki (N047) |
| 3 | **Li et al. (2022)** | Recent | *A Self-Powered Triboelectric Nanogenerator Based on Intelligent Interactive System for Police Shooting Training Monitoring and Virtual Reality Interaction* | Materials | [10.3390/ma15186228](https://doi.org/10.3390/ma15186228) | `F(t) = m \ddot{x} + k x(t)` | Ranish Devadiga (N024) |
| 4 | **Rutkowski et al. (2024)** | Recent | *Training using a commercial immersive virtual reality system on hand–eye coordination and reaction time in students: a randomized controlled trial* | Virtual Reality | [10.1007/s10055-023-00898-6](https://doi.org/10.1007/s10055-023-00898-6) | `\Delta t_{gain} = (t_{pre} - t_{post}) / t_{pre}` | Hriday Jain (N042) & Vedika Kaki (N047) |
| 5 | **Chen et al. (2025)** | Recent | *Application of a virtual reality-based measurement of simple reaction time in adults: a psychometric evaluation* | Virtual Reality | [10.1007/s10055-025-01165-6](https://doi.org/10.1007/s10055-025-01165-6) | `t_{true} = t_{logged} - \tau_{display} - \tau_{input}` | Hriday Jain (N042) |
| 6 | **Brinkmann & Lorei (2026)** | Recent | *Capability of Virtual Reality for Military Stress Inoculation Training: Stress induction using heart rate considering the influence of cybersickness, interest in technology, technology anxiety and movement* | Acta Psychologica | [10.1016/j.actpsy.2026.106338](https://doi.org/10.1016/j.actpsy.2026.106338) | `\text{Stress Score} = w_1 \Delta HR + w_2 TLX` | Medha Mishra (N062) & Ranish Devadiga (N024) |

For the exhaustive literature analysis, mathematical derivations, and viva defense questions, refer to:
* [`docs/LITERATURE_REVIEW_AND_FOUNDATIONAL_PAPERS.md`](docs/LITERATURE_REVIEW_AND_FOUNDATIONAL_PAPERS.md)
* [`docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md`](docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md)

---

## 4. Repository Directory Architecture

```
.
|-- README.md                                          <- Front-page research charter, student roster & literature
|-- RESEARCH_AND_IMPLEMENTATION_GUIDE.md               <- Comprehensive technical engineering guide
|-- Assets/
|   |-- .gitkeep
|   |-- index.html                                     <- WebXR browser-based interactive stress visualizer
|   `-- Scripts/
|       |-- ReactionLatencyTelemetryLogger.cs          <- SDT telemetry and millisecond reaction logger
|       `-- SensoryStressTargetManager.cs              <- Mesopic lighting, fog shaders & stressor dynamics
|-- docs/
|   |-- TEAM_ROSTER.json                               <- Machine-readable team configuration & verified papers
|   |-- LITERATURE_REVIEW_AND_FOUNDATIONAL_PAPERS.md   <- Exhaustive literature dossier (6 verified papers)
|   |-- RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md         <- 4-page IEEE conference manuscript blueprint
|   |-- toolchain_spec.md                              <- Unity/WebXR environment configuration guide
|   `-- figures/
|       |-- figure1_system_architecture.png            <- 300 DPI system architecture diagram
|       |-- figure2_kinematic_telemetry.png            <- 300 DPI reaction latency and ROC curves
|       `-- figure3_comparative_performance.png        <- 300 DPI comparative benchmark visualization
`-- telemetry/
    |-- .gitkeep
    |-- reaction_latency_trial_log.csv                 <- Empirical benchmark trial dataset (N = 50 runs)
    |-- sensory_stress_defense_roi.py                  <- Techno-managerial operational economics model
    |-- kennedy_ssq_calculator.py                      <- Simulator sickness questionnaire evaluation tool
    |-- nasa_tlx_calculator.py                         <- Cognitive workload assessment tool
    |-- sus_calculator.py                              <- System usability scale evaluator
    `-- test_evaluation_tools.py                       <- Unit tests for evaluation framework
```

---

## 5. Pedagogical Boundaries: Guidance vs Student Ownership

To ensure academic rigor and authentic student learning, this repository enforces strict boundaries between scaffolding and student deliverables:

* **Provided by Course Scaffolding:**
  * Tactical breach scene architecture, target prefabs, and OpenXR weapon controller bindings (`Assets/`).
  * Telemetry toolchain and usability evaluation calculators (`telemetry/`).
  * Literature foundation, mathematical formulations, and conference paper blueprint (`docs/`).
  * Dimensionless techno-managerial operational framework (`telemetry/sensory_stress_defense_roi.py`).
* **Student Technical Deliverables (Required for Evaluation):**
  * Implement and calibrate the dynamic sensory stressor envelope and Signal Detection Theory algorithms in `Assets/Scripts/SensoryStressTargetManager.cs` and `Assets/Scripts/ReactionLatencyTelemetryLogger.cs` (completing all `# TODO [Student Roll / Name]` blocks).
  * Execute simulation trials across baseline vs stress-inoculated training cohorts to collect empirical data in `telemetry/reaction_latency_trial_log.csv`.
  * Run `telemetry/sensory_stress_defense_roi.py` to regenerate 300 DPI publication figures.
  * Author the final 4-page IEEE conference paper in `docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md` and defend individual Git commits during the oral viva.

---

## 6. Sprint 0 Onboarding & Toolchain Verification

Verify your local Python, Unity/WebXR, and telemetry toolchain:

```powershell
# Step 1: Clone repository and navigate to group directory
git clone <repository_url>
cd <repository_root>/Group_02_Ranish_Hriday_Vedika_Medha

# Step 2: Checkout your individual feature branch
# Example for lead student:
git checkout -b feat/n024-xr-systems-architect

# Step 3: Run telemetry and evaluation unit tests
python telemetry/test_evaluation_tools.py

# Step 4: Verify 300 DPI publication figures and techno-managerial ROI model
python telemetry/sensory_stress_defense_roi.py
```
