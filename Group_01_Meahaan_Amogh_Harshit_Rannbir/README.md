# Research Project Group 01: Real-Time Acoustic Raycasting and Spatial Audio in Unity VR for CEDIA/CTA-RP22 Optimization

## Academic Cohort: Virtual and Augmented Reality Engineering (PBL Track)

---

## 1. Executive Research Charter

### Primary Research Problem
To what extent can real-time acoustic raycasting and spatial audio simulation in Unity VR enable residential AV integrators to optimize reverberation time (RT60) and sightline clearance according to CEDIA/CTA-RP22 standards?

### Core Investigation Domains
1. **Geometrical Acoustic Raycasting in Virtual Reality:** Simulating multi-bounce sound specular and diffuse reflections in 3D home cinema environments (7.1.4 immersive audio layouts).
2. **Impulse Response & Schroeder Backward Integration:** Calculating octave-band reverberation times ($T_{20}, T_{30}, \text{RT}_{60}$) across 125 Hz to 4 kHz and verifying convergence within CEDIA/CTA-RP22 recommended thresholds (0.2 s to 0.5 s).
3. **Ergonomic Sightline & Integration Economics:** Evaluating SMPTE/THX vertical and horizontal viewer clearance angles while modeling on-site AV integrator rework avoidance using dimensionless economic parity.

---

## 2. Student Engineering Matrix

| Roll No | Name | Technical Role | Branch Responsibility | Core Viva Defense Area |
| :--- | :--- | :--- | :--- | :--- |
| **I066** | **Meahaan Sharma** | XR Systems Architect | `feat/i066-xr-systems-architect` | Unity OpenXR integration, 7.1.4 speaker geometry, sightline raycasting |
| **C034** | **Amogh Gupta** | Spatial Acoustics & Audio Specialist | `feat/c034-spatial-acoustics-au` | Acoustic raycasting engine, absorption lookup, Schroeder RT60 integration |
| **N083** | **Harshit Rai** | Human Factors & Usability Engineer | `feat/n083-human-factors-usabil` | ISO 3382-2 compliance, MUSHRA listening tests, Kennedy SSQ / NASA-TLX |
| **N087** | **Rannbir Sachdeva** | Technoeconomic Product Manager | `feat/n087-technoeconomic-produ` | CEDIA/CTA-RP22 metrics, AV integrator rework reduction, payback parity |

---

## 3. Directory Architecture

```
Group_01_Meahaan_Amogh_Harshit_Rannbir/
├── README.md                                      <- Master project engineering charter
├── RESEARCH_AND_IMPLEMENTATION_GUIDE.md           <- In-depth technical specifications and student boundaries
├── docs/
│   ├── TEAM_ROSTER.json                           <- Machine-readable Git attribution schema
│   ├── toolchain_spec.md                          <- Verified XR development environment specs
│   ├── LITERATURE_REVIEW_AND_FOUNDATIONAL_PAPERS.md <- Exhaustive review of 5 peer-reviewed benchmark papers
│   ├── RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md     <- 4-page IEEE conference publication template
│   └── figures/                                   <- High-resolution publication diagrams (300 DPI)
│       ├── figure1_system_architecture.png
│       ├── figure2_kinematic_telemetry.png
│       └── figure3_comparative_performance.png
├── Assets/
│   ├── index.html                                 <- WebXR preview environment
│   └── Scripts/
│       ├── AcousticRaycaster.cs                   <- C# raycast engine and reflection tracker
│       └── RT60TelemetryLogger.cs                 <- C# Schroeder integration and CSV logger
└── telemetry/
    ├── av_integration_economics.py                <- CSBS rework avoidance and dimensionless ROI model
    ├── generate_paper_figures.py                  <- 300 DPI visualization engine and benchmark dataset generator
    ├── acoustic_benchmark_data.csv                <- 100-trial experimental benchmark dataset
    ├── kennedy_ssq_calculator.py                  <- Cybersickness assessment script
    ├── nasa_tlx_calculator.py                     <- Cognitive workload calculator
    └── sus_calculator.py                          <- System Usability Scale calculator
```

---

## 4. Key Academic & Industry Milestones

- **Milestone 1 (Sprint 0-1):** OpenXR toolchain verification, home cinema geometry construction, and Sabine/Eyring validation.
- **Milestone 2 (Sprint 2-3):** Implementation of multi-bounce acoustic raycasting and Schroeder backward integration in C#.
- **Milestone 3 (Sprint 4):** 100-trial benchmark evaluation comparing untreated, partially treated, and CEDIA-optimized room configurations.
- **Milestone 4 (Sprint 5):** Manuscript compilation following IEEE conference standards and reproducible Git audit defense.
