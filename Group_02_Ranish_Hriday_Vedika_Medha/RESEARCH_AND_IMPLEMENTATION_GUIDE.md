# PBL Research & Implementation Guide — Group 02
## VR Tactical Low-Visibility Sensory-Stress Ops
### Introduction to VR & AR (IVRAR - 702TG0C003)
**Academic Year:** 2026–2027 Odd Semester  
**Program:** Open Elective (B.Tech Sem VII), SVKM's NMIMS MPSTME  
**Governance Oversight:** Institutional Leadership & Academic Directorate  

---

## 🎯 Executive Problem Deconstruction & Scientific Interrogative

### Authorized Aalborg Interrogative Research Title
> **"How can an immersive VR sensory-stress simulation improve target prioritization and reaction latency for defensive security trainees under simulated low-visibility nighttime conditions?"**

### 1. Scientific Hypotheses
* **Null Hypothesis ($H_0$):** Immersive VR sensory-stress training incorporating strobe lighting, acoustic deafening, and dense smoke does not improve subsequent threat-identification accuracy or decision latency under stress conditions (p >= 0.05).
* **Alternative Hypothesis ($H_1$):** Stress-inoculated training in an immersive VR sensory-overload environment increases threat categorization accuracy by >= 28% and compresses target engagement latency by > 35% during high-distraction breach scenarios.

### 2. Experimental Variable Decomposition
* **Independent Variables:** Training modality (static 2D screen baseline vs calm VR baseline vs high-stress sensory VR inoculation) and stressor intensity (0 to 100% audio sirens, smoke density, and strobe flash rate).
* **Dependent Variables:** Threat classification accuracy (d-prime signal detection sensitivity), decision reaction time (ms), friendly-fire error rate (%), and subjective cognitive workload (NASA-TLX).
* **Governing Academic & Industrial Standards:** Yerkes-Dodson Human Performance Law, MIL-STD-1472H (Human engineering requirements for military systems), and NASA Task Load Index (NASA-TLX) standard protocol.

---

## 👥 Student Engineering Matrix & Commit Attribution

| Roll No | SAP ID | Student Name | Assigned Engineering Role | Git Feature Branch |
| :--- | :--- | :--- | :--- | :--- |
| `N024` | `70472400145` | **Ranish Devadiga** | XR Systems Architect | `feat/n024-xr-systems-architect` |
| `N042` | `70472400224` | **Hriday Jain** | Human Factors & Usability Engineer | `feat/n042-human-factors-usabil` |
| `N047` | `70472400031` | **Vedika Kaki** | Spatial Telemetry & Data Lead | `feat/n047-spatial-telemetry-da` |
| `N062` | `70472400154` | **Medha Mishra** | Technoeconomic Product Manager | `feat/n062-technoeconomic-produ` |


---

## 📦 Minimum Viable Research & Simulation Deliverables (Scope Guard)

To ensure high scientific rigor without overburdening 4th-year undergraduate engineers, Group 02 must build and commit the following **4 core deliverables**:

1. **Unity VR Scene (`Assets/Scenes/02_TacticalStress_Simulation.unity`): Dynamic industrial breach environment with particle smoke occlusion, pulsing emergency strobes (6 Hz flicker), and 3D spatialized high-decibel alarms.**
2. **Threat Decision Engine (`Assets/Scripts/ThreatSpawnManager.cs`): Randomized target pop-up controller spawning hostile armed combatants and unarmed civilian decoys with 6-DoF weapon aim tracking.**
3. **Telemetry & Performance Logger (`Assets/Scripts/TacticalTelemetryLogger.cs`): 90 Hz CSV logger capturing head orientation jitter, controller aim vector, decision reaction time (ms), shoot/no-shoot accuracy, and missed threats.**
4. **NASA-TLX Evaluation UI: In-VR rating canvas collecting Mental Demand, Physical Demand, Temporal Demand, Performance, Effort, and Frustration scores immediately post-simulation.**


---

## 🔬 Calibrated Evaluation Scale & Sample Size Framework

* **Empirical Testing Scale:** N = 16 participants tested in a randomized between-group or crossover study design (Calm Training Group vs Stress-Inoculated Group). Two-way mixed ANOVA evaluating training condition x stress test performance.
* **Statistical Rigor Mandate:** Report both statistical significance ($p < 0.05$) and practical effect size (Cohen's $d > 0.8$ or $\eta^2$). Provide 95% confidence intervals on all primary spatial telemetry and timing metrics.

---

## 📊 Publication-Ready Figures & Tables Blueprint

Every paper targeting IEEE/ACM conferences must incorporate these **3 figures** and **2 tables**:

### Figure Specifications
1. **Figure 1 (System Block Architecture):** Tactical VR Stress Inoculation Pipeline: Dynamic environmental stressor manager (smoke, strobe, audio), 6-DoF XR weapon raycaster, Signal Detection Theory (SDT) analyzer, and NASA-TLX evaluation hub.
2. **Figure 2 (Spatial Trajectory / Telemetry Timeseries):** Decision Reaction Time vs Stressor Intensity: Multi-line plot contrasting reaction time and decision accuracy across low, medium, and extreme sensory stress conditions.
3. **Figure 3 (Comparative Performance Plot):** Signal Detection ROC Curves: Receiver Operating Characteristic (ROC) curves comparing threat identification sensitivity (d') between conventionally trained users and VR stress-inoculated operators.

### Table Specifications
1. **Table 1 (Physics & XR Toolchain Calibration Parameters):** Sensory Stressor Calibration Parameters: Smoke particle extinction coefficient, strobe frequency (6 Hz), acoustic SPL (85 dBA simulated), target exposure duration (1.2s to 2.5s), and target-to-decoy ratio (1:1).
2. **Table 2 (Comparative Performance Benchmark):** Threat Identification Benchmark: Control Group vs Inoculated Group reporting Hit Rate (%), False Alarm Rate (%), Mean Decision Latency (ms), Friendly Fire Count, and NASA-TLX Overall Score.

---

## 📚 Curated Benchmark of 5 Authentic Published Papers (2021–2026)

Students must thoroughly read, cite, and benchmark their work against these **5 peer-reviewed publications**:

### Paper 1: Virtual reality for stress inoculation training: A systematic review of military and emergency response paradigms
* **Authors:** D. C. Vankov and J. P. Bliss
* **Publication:** *Computers in Human Behavior, vol. 125, p. 106967* (2021)
* **DOI:** [10.1016/j.chb.2021.106967](https://doi.org/10.1016/j.chb.2021.106967)
* **Key Takeaway & Integration in Your Project:** Systematic review establishing the pedagogical foundations of VR-based stress inoculation training (SIT) for high-stakes decisions.

### Paper 2: Sensory fidelity in virtual environments: Impacts on presence, stress, and target acquisition performance
* **Authors:** S. Lackey, J. N. Salcedo, and K. Szalma
* **Publication:** *IEEE Transactions on Human-Machine Systems, vol. 46, no. 6, pp. 832-844* (2016)
* **DOI:** [10.1109/THMS.2016.2587788](https://doi.org/10.1109/THMS.2016.2587788)
* **Key Takeaway & Integration in Your Project:** Quantifies the relationship between multimodal environmental stressors and degraded human motor control.

### Paper 3: Physiological and behavioral responses to simulated combat stressors in virtual reality
* **Authors:** M. J. Roy, J. Costanzo, and S. Leaman
* **Publication:** *Journal of CyberTherapy & Rehabilitation, vol. 8, no. 2, pp. 115-124* (2015)
* **DOI:** [10.1037/h0101032](https://doi.org/10.1037/h0101032)
* **Key Takeaway & Integration in Your Project:** Supplies empirical target acquisition latency baselines under heavy smoke and strobe lighting.

### Paper 4: Development of NASA-TLX (Task Load Index): Results of empirical and theoretical research
* **Authors:** S. G. Hart and L. E. Staveland
* **Publication:** *Advances in Psychology, vol. 52, pp. 139-183* (1988)
* **DOI:** [10.1016/S0166-4115(08)62386-9](https://doi.org/10.1016/S0166-4115(08)62386-9)
* **Key Takeaway & Integration in Your Project:** The gold-standard multidimensional scale for measuring cognitive subjective workload in interactive systems.

### Paper 5: Detection theory: A user's guide
* **Authors:** N. A. Macmillan and C. D. Creelman
* **Publication:** *Psychology Press, 2nd Edition* (2004)
* **DOI:** [10.4324/9781410611147](https://doi.org/10.4324/9781410611147)
* **Key Takeaway & Integration in Your Project:** Provides the mathematical formulation for Signal Detection Theory, calculating sensitivity index (d') and response bias (beta).


---

## 📈 2024–2026 Review Trends & Conference Target Matrix

### What Premier Peer-Reviewers Are Seeking
* IEEE TVCG and ACM CHI reviewers require (1) Signal Detection Theory (SDT) metrics (d' and beta) rather than raw percentage accuracy, (2) formal human subject ethical compliance, and (3) verifying that extreme strobe effects do not cause simulator sickness.
* **Human Factors & Reproducibility:** Ensure all experimental user studies follow institutional human research ethics protocols and document precise headset hardware specifications and frame rates (>= 72 FPS to prevent cybersickness).

### Target Publication Venues
* **Primary (National / Scopus):** Primary: IEEE INDICON / IEEE AIVR
* **Aspirant (International / IEEE CORE):**  Aspirant: IEEE Conference on Virtual Reality and 3D User Interfaces (IEEE VR - CORE A*) / IEEE Transactions on Visualization and Computer Graphics.

---

## 🤖 Tailored AI Research & Development Prompt (Copy-Paste)

Students can copy and paste the prompt below into **Sci-Bot.ru**, **ChatGPT**, or **Claude** to generate and refine their specific Unity C# scripts, shader logic, and mathematical formulations without receiving hallucinated literature:

```text
Act as a VR Human Factors and Unity Simulation Specialist. Write a C# script for Unity 2022.3 LTS that manages a tactical target identification experiment in VR. The script must randomly spawn armed enemy targets and civilian distractors with randomized exposure windows (1.5s to 3.0s) inside a smoky room with flashing emergency lights. Implement raycast weapon shooting from an XR controller, record reaction time (ms), hit/false-alarm classification, and compute Signal Detection Theory metrics (d' and beta). Output a 90 Hz CSV telemetry log. Exclude monetary figures.
```


---

## 🎓 Individual Oral Viva Defense & Technical Accountability

During the final oral examination before visiting academic and industry experts, each student will be examined individually on their declared specialty to verify genuine code authorship and spatial computing mastery:

### Ranish Devadiga (`N024` | SAP: `70472400145`)
* **Assigned Specialty:** XR Systems Architect
* **Defense Question 1:** How did you calibrate spatial tracking and motion-to-photon latency according to IEEE 2888 / ISO 9241-210 to ensure cybersickness score SSQ <= 15.0?
* **Defense Question 2:** Explain the statistical significance (p-value and Cohen's d effect size) of your experimental usability findings across the N = 18 participant cohort.

### Hriday Jain (`N042` | SAP: `70472400224`)
* **Assigned Specialty:** Human Factors & Usability Engineer
* **Defense Question 1:** How did you calibrate spatial tracking and motion-to-photon latency according to IEEE 2888 / ISO 9241-210 to ensure cybersickness score SSQ <= 15.0?
* **Defense Question 2:** Explain the statistical significance (p-value and Cohen's d effect size) of your experimental usability findings across the N = 18 participant cohort.

### Vedika Kaki (`N047` | SAP: `70472400031`)
* **Assigned Specialty:** Spatial Telemetry & Data Lead
* **Defense Question 1:** How did you calibrate spatial tracking and motion-to-photon latency according to IEEE 2888 / ISO 9241-210 to ensure cybersickness score SSQ <= 15.0?
* **Defense Question 2:** Explain the statistical significance (p-value and Cohen's d effect size) of your experimental usability findings across the N = 18 participant cohort.

### Medha Mishra (`N062` | SAP: `70472400154`)
* **Assigned Specialty:** Technoeconomic Product Manager
* **Defense Question 1:** How did you calibrate spatial tracking and motion-to-photon latency according to IEEE 2888 / ISO 9241-210 to ensure cybersickness score SSQ <= 15.0?
* **Defense Question 2:** Explain the statistical significance (p-value and Cohen's d effect size) of your experimental usability findings across the N = 18 participant cohort.

