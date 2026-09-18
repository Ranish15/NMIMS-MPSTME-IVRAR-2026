# IVRAR Group 02: Immersive VR Sensory-Stress Defensive Security Training

---

## 1. Authorized Research Title & Problem Statement

> "How can an immersive VR sensory-stress simulation improve target prioritization and reaction latency for defensive security trainees under simulated low-visibility nighttime conditions?"

### Core Engineering Focus
* **Immersive Nighttime Environment:** Unity OpenXR low-lux simulation with volumetric shadows, mesopic tone mapping, and dynamic flashlight cone attenuation (`Assets/index.html` and `Assets/Scripts/`).
* **Acute Sensory Stressors:** Dynamic auditory startle triggers (85-95 dB acoustic cues), peripheral strobe flashes, and randomized shoot/don't-shoot target encounters (`Assets/Scripts/SensoryStressTargetManager.cs`).
* **Signal Detection & Latency Telemetry:** High-precision millisecond reaction timer and Signal Detection Theory ($d', \beta$) automated telemetry engine (`Assets/Scripts/ReactionLatencyTelemetryLogger.cs`).
* **Human Factors & Workload Assessment:** Cognitive workload quantification via NASA-TLX, Situational Awareness (SAGAT), and Kennedy SSQ cybersickness tracking (`telemetry/`).
* **CSBS Technoeconomic Analysis:** Dimensionless ammunition substitution model, physical shoot-house facility cost parity, and capital payback analysis (`telemetry/security_training_economics.py`).

---

## 2. Student Engineering Team Matrix

| Roll No | SAP ID | Student Name | Technical Role | Assigned Git Branch | Core Viva Defense Area |
| :--- | :--- | :--- | :--- | :--- | :--- |
| `N024` | `70472400145` | **Ranish Devadiga** | Lead XR Systems Architect | `feat/n024-xr-systems-architect` | Unity OpenXR nighttime mesopic rendering, volumetric low-light fog shaders, dynamic flashlight cone inverse-square falloff, peripheral strobe startle cues, and 6-DoF defensive weapon rig. |
| `N042` | `70472400224` | **Hriday Jain** | Human Factors & Usability Engineer | `feat/n042-human-factors-usabil` | Multi-dimensional NASA-TLX cognitive workload assessment, Situational Awareness Global Assessment Technique (SAGAT), Kennedy SSQ cybersickness monitoring, and stress inoculation training (SIT) efficacy. |
| `N047` | `70472400031` | **Vedika Kaki** | Spatial Telemetry & Data Lead | `feat/n047-spatial-telemetry-da` | Signal Detection Theory (SDT) sensitivity index ($d'$) and decision criterion ($\beta$), shoot/don't-shoot millisecond reaction latency logging, false alarm error rate reduction, and paired t-test hypothesis testing. |
| `N062` | `70472400154` | **Medha Mishra** | Technoeconomic Product Manager | `feat/n062-technoeconomic-produ` | CSBS defensive training economics, live-fire ammunition consumption substitution (350 rounds/trainee), physical shoot-house facility rental replacement, operational cost parity ($\kappa = 0.042$), and capital payback horizon (10.9 months). |

---

## 3. Foundational Literature Benchmarks (6 Verified Peer-Reviewed Papers)

The research project is theoretically anchored on six foundational peer-reviewed publications validated against global bibliographic databases (IEEE, Springer, Elsevier, Taylor & Francis, APA).

| # | Author (Year) | Paper Title | Venue / Indexing | Active DOI Link | Primary Extracted Formulation | Student Lead |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | **Oudejans (2008)** | *Reality-based practice under pressure improves handgun shooting performance of police officers* | Ergonomics | [10.1080/00140130701577435](https://doi.org/10.1080/00140130701577435) | `P(hit) = f(AnxietyLevel, TrainingRegime)` | Vedika Kaki (N047) & Medha Mishra (N062) |
| 2 | **Bhagat et al. (2016)** | *A cost-effective interactive 3D virtual reality system applied to military live firing training* | Virtual Reality | [10.1007/s10055-016-0284-x](https://doi.org/10.1007/s10055-016-0284-x) | `kappa_ammo = N_virtual_rounds / N_live_rounds` | Ranish Devadiga (N024) & Medha Mishra (N062) |
| 3 | **Stanislaw & Todorov (1999)** | *Calculation of signal detection theory measures* | Behavior Research Methods, Instruments, & Computers | [10.3758/BF03207704](https://doi.org/10.3758/BF03207704) | `d' = z(H) - z(F), beta = exp(-d' * c)` | Vedika Kaki (N047) |
| 4 | **Petit et al. (2012)** | *Evaluation of tone mapping operators in night-time virtual worlds* | Virtual Reality | [10.1007/s10055-012-0215-4](https://doi.org/10.1007/s10055-012-0215-4) | `C_t = (L_target - L_bg) / L_bg` | Ranish Devadiga (N024) |
| 5 | **Endsley (1995)** | *Toward a Theory of Situation Awareness in Dynamic Systems* | Human Factors | [10.1518/001872095779049543](https://doi.org/10.1518/001872095779049543) | `SA = (1/M) * sum(S_actual / S_correct)` | Hriday Jain (N042) |
| 6 | **Hart & Staveland (1988)** | *Development of NASA-TLX (Task Load Index): Results of Empirical and Theoretical Research* | Advances in Psychology | [10.1016/S0166-4115(08)62386-9](https://doi.org/10.1016/S0166-4115(08)62386-9) | `TLX = (1/15) * sum(w_i * R_i)` | Hriday Jain (N042) |

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
|   |-- index.html                                     <- WebXR low-visibility interactive defensive simulator
|   `-- Scripts/
|       |-- SensoryStressTargetManager.cs              <- Nighttime lighting, fog & sensory stressor triggers
|       `-- ReactionLatencyTelemetryLogger.cs          <- Reaction latency & Signal Detection (d', beta) logger
|-- docs/
|   |-- TEAM_ROSTER.json                               <- Machine-readable team configuration & 6 verified papers
|   |-- LITERATURE_REVIEW_AND_FOUNDATIONAL_PAPERS.md   <- Exhaustive literature dossier (6 verified papers)
|   |-- RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md         <- 4-page IEEE conference manuscript blueprint
|   |-- toolchain_spec.md                              <- Unity/WebXR environment configuration guide
|   `-- figures/
|       |-- figure1_system_architecture.png            <- 300 DPI system architecture diagram
|       |-- figure2_kinematic_telemetry.png            <- 300 DPI reaction latency and ROC curves
|       `-- figure3_comparative_performance.png        <- 300 DPI comparative benchmark visualization
`-- telemetry/
    |-- .gitkeep
    |-- security_training_benchmark.csv                <- Empirical benchmark trial dataset (N = 50 runs)
    |-- security_training_economics.py                 <- CSBS dimensionless technoeconomic model
    |-- generate_paper_figures.py                      <- Standalone 300 DPI publication figure generator
    |-- kennedy_ssq_calculator.py                      <- Simulator sickness questionnaire evaluation tool
    |-- nasa_tlx_calculator.py                         <- Cognitive workload assessment tool
    |-- sus_calculator.py                              <- System usability scale evaluator
    `-- test_evaluation_tools.py                       <- Unit tests for evaluation framework
```

---

## 5. Pedagogical Boundaries: Guidance vs Student Ownership

To ensure academic rigor and authentic student learning, this repository enforces strict boundaries between scaffolding and student deliverables:

* **Provided by Course Scaffolding:**
  * Low-light 3D defensive environment, target prefabs, and stressor audio architecture (`Assets/`).
  * Telemetry toolchain, usability evaluation calculators, and statistical testing framework (`telemetry/`).
  * Literature foundation, mathematical formulations, and conference paper blueprint (`docs/`).
  * Dimensionless technoeconomic framework (`telemetry/security_training_economics.py`).
* **Student Technical Deliverables (Required for Evaluation):**
  * Implement dynamic volumetric fog attenuation, flashlight beam falloff, and audio startle envelopes in `Assets/Scripts/SensoryStressTargetManager.cs` (completing all `# TODO [Student Roll / Name]` blocks).
  * Implement Signal Detection Theory formulas ($d'$ and $\beta$) and millisecond latency timers in `Assets/Scripts/ReactionLatencyTelemetryLogger.cs`.
  * Execute experimental simulation trials across untrained control vs stress-inoculated cohorts to collect empirical data in `telemetry/security_training_benchmark.csv`.
  * Re-run `telemetry/generate_paper_figures.py` with live experimental data to regenerate publication figures.
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

# Step 4: Verify 300 DPI publication figures
python telemetry/generate_paper_figures.py

# Step 5: Execute CSBS technoeconomic model
python telemetry/security_training_economics.py
```
