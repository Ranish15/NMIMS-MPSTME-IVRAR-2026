# Academic & Industrial Engineering Standards Manual
## Introduction to Virtual Reality & Augmented Reality (IVRAR - 702COI002)
**Course:** Institute Open Elective, B.Tech Semester V  
**Academic Year:** 2026–2027 (Semester V, Odd Semester)  
**Pedagogical Framework:** Aalborg-UNESCO Problem-Based Learning (PBL) & CDIO  

---

## 1. Executive Summary & Purpose
This manual establishes the formal, binding technical and academic standards governing all 18 Problem-Based Learning (PBL) projects within the IVRAR cohort. Under international XR engineering standards (IEEE, ISO, W3C, Khronos), virtual and augmented reality applications must be evaluated using validated psychometric instruments and quantitative performance benchmarks.

---

## 2. Spatial Computing & XR Systems Standards

### 2.1 IEEE 2888 Standards Family — Spatial Computing, Sensors & Actuators
* **Applicability:** All 18 IVRAR Groups.
* **IEEE 2888.1:** Standard for Specification of Sensor and Actuator for Virtual and Augmented Reality. Defines standardized latency, field-of-view (FOV), and refresh rate requirements for spatial tracking.
* **IEEE 2888.2:** Standard for Interfacing Cyber and Physical Worlds. Governs spatial anchoring, world-locking coordinates, and event telemetry pipelines.

### 2.2 ISO 9241-210 & ISO 9241-920 — Ergonomics of Human-System Interaction
* **Applicability:** All 18 IVRAR Groups.
* **ISO 9241-210:2019:** Human-centred design for interactive systems. Mandates iterative stakeholder feedback, accessibility, and cognitive load minimization.
* **ISO 9241-920:2016:** Guidance on tactile and haptic interactions in virtual environments. Specifies vibration duration, amplitude modulation, and latency thresholds ($< 20\text{ ms}$) for haptic confirmation.

### 2.3 W3C WebXR Device API & Khronos OpenXR 1.1
* **Applicability:** Web-based, mobile AR, and standalone VR deployments.
* **OpenXR Specification:** Direct hardware runtime interface guaranteeing cross-platform motion-to-photon latency $\le 20\text{ ms}$ at $\ge 72\text{ Hz}$ (or $90\text{ Hz}$ on desktop HMDs) to avoid vestibulo-ocular reflex mismatches.
* **Spatial Reference Spaces:** `viewer`, `local`, `local-floor`, and `bounded-floor`.

---

## 3. Validated Psychometric Evaluation Instruments

Every student project presenting empirical human evaluation must employ at least two of the following **three gold-standard instruments**:

### 3.1 System Usability Scale (SUS) — Brooke (1996) / ISO 9241-11
* **Structure:** 10 standardized 5-point Likert questions alternating between positive and negative phrasing.
* **Scoring Formula:**
  $$\text{SUS Score} = 2.5 \times \left( \sum_{i=1,3,5,7,9} (Q_i - 1) + \sum_{j=2,4,6,8,10} (5 - Q_j) \right)$$
* **Benchmark Grading:**
  * $> 80.3$: Grade A (Excellent usability)
  * $> 68.0$: Grade C (Acceptable industry average)
  * $< 51.0$: Grade F (Unacceptable usability)

### 3.2 NASA Task Load Index (NASA-TLX) — Hart & Staveland (1988)
* **Structure:** 6 subjective subscales scored from 0 to 100: Mental Demand, Physical Demand, Temporal Demand, Performance, Effort, Frustration.
* **Target Objective:** VR training or simulation interventions must demonstrate a statistically significant reduction in Cognitive Workload compared to traditional 2D or physical controls.

### 3.3 Kennedy Simulator Sickness Questionnaire (SSQ) — Kennedy et al. (1993)
* **Structure:** 16-symptom diagnostic checklist yielding 3 sub-scores and Total Severity ($TS$):
  * **Nausea ($N$):** Weighted sum $\times 9.54$
  * **Oculomotor ($O$):** Weighted sum $\times 3.74$
  * **Disorientation ($D$):** Weighted sum $\times 13.92$
  * **Total Score ($TS$):** Weighted sum $\times 3.74$
* **Acceptance Criteria:** Safe VR applications must maintain $TS \le 15.0$ to prevent adverse cybersickness effects.

---

## 4. Human-Subject Evaluation Protocol & Sample Sizing
* **Target Sample Size:** $N = 12\text{ to }25$ evaluated participants across within-subject (repeated measures) or between-subject A/B test designs.
* **Statistical Rigor:** Paired Student's $t$-test or Wilcoxon Signed-Rank test for non-parametric Likert scales, with Cohen's $d$ or rank-biserial correlation effect sizes.
