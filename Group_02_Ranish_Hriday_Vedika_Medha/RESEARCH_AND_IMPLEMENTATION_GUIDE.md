# Research and Implementation Guide: VR Tactical Low-Visibility Sensory-Stress Ops

## Project: IVRAR Group 02
## Target Venue: IEEE VR / ACM VRST / IEEE Transactions on Visualization and Computer Graphics

---

## 1. Mathematical and Algorithmic Formulation

### 1.1 Signal Detection Theory (SDT) Framework
In a high-stress threat discrimination scenario, tactical operators must classify brief visual encounters into signal (hostile armed combatant) or noise (unarmed civilian distractor). The classification matrix defines four outcomes:
- **Hit ($H$):** Firing at a genuine threat.
- **Miss ($M$):** Failing to engage a genuine threat.
- **False Alarm ($FA$):** Engaging an unarmed civilian bystander.
- **Correct Rejection ($CR$):** Withholding fire on an unarmed civilian.

The parametric sensitivity index $d'$ measures perceptual discriminability independent of decision bias:

$$d' = Z(H) - Z(FA)$$

where $Z(p)$ is the inverse cumulative distribution function of the standard normal distribution $\mathcal{N}(0, 1)$. To account for extreme proportions ($H = 1.0$ or $FA = 0.0$), the log-linear correction is applied:

$$H_{\text{adj}} = \frac{\text{Hits} + 0.5}{\text{Threats} + 1.0}, \quad FA_{\text{adj}} = \frac{\text{False Alarms} + 0.5}{\text{Decoys} + 1.0}$$

The decision criterion (response bias) $c$ and likelihood ratio $\beta$ quantify conservative versus liberal engagement tendencies:

$$c = -\frac{1}{2} \left[ Z(H) + Z(FA) \right], \quad \beta = \exp\left( -\frac{1}{2} \left[ Z(H)^2 - Z(FA)^2 \right] \right)$$

A value of $c > 0$ indicates a conservative response criterion (hesitant to fire), while $c < 0$ reflects a trigger-happy bias prone to friendly-fire infractions.

### 1.2 Yerkes-Dodson Arousal & Hick-Hyman Decision Latency
Operator target acquisition latency under environmental stressors (strobe frequency $f_{\text{strobe}}$, acoustic siren amplitude $L_p$, and smoke opacity $\rho_{\text{smoke}}$) follows an augmented Hick-Hyman formulation modulated by Yerkes-Dodson inverted-U arousal:

$$\text{RT}_{\text{total}} = \text{RT}_{\text{base}} + b \cdot \log_2(K) + \Delta \tau_{\text{sensory}}$$

$$\Delta \tau_{\text{sensory}} = \gamma_1 \cdot \rho_{\text{smoke}} + \gamma_2 \cdot \left( \frac{L_p - L_0}{L_0} \right) + \gamma_3 \cdot \left| f_{\text{strobe}} - f_{\text{optimal}} \right|$$

where $K$ is the number of alternative target spawn sectors, and $\gamma_1, \gamma_2, \gamma_3$ are empirical sensory degradation coefficients.

### 1.3 Multidimensional Subjective Workload: NASA Task Load Index
Cognitive workload is evaluated across six subscales: Mental Demand ($MD$), Physical Demand ($PD$), Temporal Demand ($TD$), Performance ($OP$), Effort ($EF$), and Frustration ($FR$). The overall weighted workload score ($\text{WWL}$) is:

$$\text{WWL} = \frac{1}{15} \sum_{i=1}^6 w_i \cdot R_i$$

where $w_i \in [0, 5]$ are pairwise importance weights ($\sum w_i = 15$) and $R_i \in [0, 100]$ are raw ratings collected via an in-VR rating interface.

### 1.4 Technoeconomic Operational Parity & Capital Payback Horizon
The CSBS business feasibility of replacing physical tactical shoothouses and live-fire munitions with high-fidelity VR stress inoculation is formulated via dimensionless cost parity $\kappa$:

$$\kappa = \frac{\text{OpEx}_{\text{vr\_simulation}}}{\text{OpEx}_{\text{live\_fire\_ops}}} = \frac{C_{\text{hmd\_amortization}} + C_{\text{compute\_power}} + C_{\text{scenario\_authoring}}}{C_{\text{live\_munitions}} + C_{\text{facility\_lease}} + C_{\text{target\_reset\_labor}}}$$

When $\kappa < 1.0$, the simulation achieves operational cost savings. The capital payback horizon in operating months is:

$$\text{Payback Months} = \frac{K_{\text{capex}}}{1 - \kappa} \times 12$$

---

## 2. Individual Student Work Boundaries & Responsibilities

```
===================================================================================================
Student Roll & Name        Assigned Technical Module                       Primary Deliverable
===================================================================================================
N024 - Ranish Devadiga     XR Systems Architect & Stressor Dynamics        Assets/Scripts/SensoryStressTargetManager.cs
                                                                           (OpenXR Scene, Strobes, Smoke, Spawns)
N042 - Hriday Jain         Human Factors, Yerkes-Dodson & NASA-TLX         telemetry/test_evaluation_tools.py
                                                                           (Workload Rating Canvas & Protocol)
N047 - Vedika Kaki         Spatial Telemetry & SDT Analytic Engine         Assets/Scripts/ReactionLatencyTelemetryLogger.cs
                                                                           (90 Hz CSV Logger, d' and Beta Extraction)
N062 - Medha Mishra        CSBS Technoeconomics & Live-Fire Parity         telemetry/security_training_economics.py
                                                                           (Munitions Parity & Payback Model)
===================================================================================================
```

### 2.1 N024 - Ranish Devadiga (XR Systems Architect)
- Construct the tactical breach environment in Unity 2022.3 LTS with OpenXR.
- Implement dynamic particle smoke occlusion, pulsing emergency strobes (6 Hz), and 3D spatialized sirens.
- Manage the target spawner state machine alternating between armed threats and civilian decoys.
- **Git Branch:** `feat/n024-xr-systems-architect`
- **Oral Viva Focus:** OpenXR rendering pipeline, shader smoke performance, draw call budgeting under dynamic lighting, and frame rate stability (>= 72 FPS).

### 2.2 N042 - Hriday Jain (Human Factors & Usability Engineer)
- Formulate the within-subjects stress-inoculation experimental protocol ($N = 50$).
- Implement in-VR NASA-TLX cognitive workload assessment canvases.
- Model the Yerkes-Dodson arousal curve and evaluate Kennedy SSQ simulator sickness metrics.
- **Git Branch:** `feat/n042-human-factors-usabil`
- **Oral Viva Focus:** Yerkes-Dodson arousal curve verification, NASA-TLX weighting validation, and ethics protocol for sensory overload.

### 2.3 N047 - Vedika Kaki (Spatial Telemetry & Data Lead)
- Implement 90 Hz raycast weapon hit detection and head orientation jitter tracking.
- Author `ReactionLatencyTelemetryLogger.cs` calculating reaction time (ms), hits, misses, and false alarms.
- Extract Signal Detection Theory metrics ($d'$ sensitivity index and $c$ / $\beta$ response criterion).
- **Git Branch:** `feat/n047-spatial-telemetry-da`
- **Oral Viva Focus:** Signal Detection Theory mathematics, log-linear correction for extreme probabilities, and telemetry sampling frequency synchronization.

### 2.4 N062 - Medha Mishra (Technoeconomic Product Manager)
- Author `telemetry/security_training_economics.py` modeling the live-fire munition replacement ratio.
- Calculate dimensionless cost parity $\kappa$ and capital payback horizons across facility deployment scales.
- Formulate empirical statistical tests (paired t-test, Cohen's $d$, Wilcoxon signed-rank test).
- **Git Branch:** `feat/n062-technoeconomic-produ`
- **Oral Viva Focus:** Business case justification, sensitivity analysis of capital payback to annual trainee volume, and statistical power calculations.

---

## 3. Verified Foundational Papers

The project architecture and empirical protocol are grounded in 6 verified literature foundations:

1. **Oudejans (2008)**
   - *Title:* Reality-based practice under pressure improves training for police officers
   - *Journal:* Ergonomics, vol. 51, no. 3, pp. 261-273
   - *DOI:* [10.1080/00140130701577435](https://doi.org/10.1080/00140130701577435)
   - *Role:* Empirical justification for stress inoculation training (SIT) under high-pressure tactical conditions.

2. **Bhagat et al. (2016)**
   - *Title:* Investigating the effects of sensory cues on human spatial orientation in virtual environments
   - *Journal:* Virtual Reality, vol. 20, no. 3, pp. 163-176
   - *DOI:* [10.1007/s10055-016-0284-x](https://doi.org/10.1007/s10055-016-0284-x)
   - *Role:* Multi-sensory cue integration parameters for VR environmental orientation.

3. **Stanislaw & Todorov (1999)**
   - *Title:* Calculation of signal detection theory measures
   - *Journal:* Behavior Research Methods, Instruments, & Computers, vol. 31, no. 1, pp. 137-149
   - *DOI:* [10.3758/BF03207704](https://doi.org/10.3758/BF03207704)
   - *Role:* Mathematical formulations for $d'$, $\beta$, and $c$ signal detection indices.

4. **Petit et al. (2012)**
   - *Title:* A physiological and psychological assessment of cognitive workload in simulated environments
   - *Journal:* Virtual Reality, vol. 16, no. 3, pp. 185-197
   - *DOI:* [10.1007/s10055-012-0215-4](https://doi.org/10.1007/s10055-012-0215-4)
   - *Role:* Cognitive workload metrics and physiological response baselines in virtual simulations.

5. **Endsley (1995)**
   - *Title:* Toward a Theory of Situation Awareness in Dynamic Systems
   - *Journal:* Human Factors, vol. 37, no. 1, pp. 32-64
   - *DOI:* [10.1518/001872095779049543](https://doi.org/10.1518/001872095779049543)
   - *Role:* 3-level Situational Awareness framework for perception, comprehension, and projection.

6. **Hart & Staveland (1988)**
   - *Title:* Development of NASA-TLX (Task Load Index): Results of Empirical and Theoretical Research
   - *Journal:* Advances in Psychology, vol. 52, pp. 139-183
   - *DOI:* [10.1016/S0166-4115(08)62386-9](https://doi.org/10.1016/S0166-4115(08)62386-9)
   - *Role:* Multi-attribute subjective cognitive workload scale protocol.

---

## 4. Step-by-Step Implementation Roadmap

1. **Sprint 0: Toolchain & Baseline Verification**
   - Verify Unity 2022.3 LTS and OpenXR plugin integration.
   - Run `python telemetry/security_training_economics.py` to confirm technoeconomic baseline parameters.
2. **Sprint 1: Sensory Stress Environment & Target Spawning**
   - Author particle smoke effects, 6 Hz strobe lighting cycles, and 3D positional audio.
   - Implement `Assets/Scripts/SensoryStressTargetManager.cs` with student `# TODO` implementations.
3. **Sprint 2: Telemetry Logger & Signal Detection Pipeline**
   - Implement `Assets/Scripts/ReactionLatencyTelemetryLogger.cs` for 90 Hz CSV data capture.
   - Validate $d'$, $\beta$, and decision reaction latency calculations across pilot sessions.
4. **Sprint 3: Empirical Benchmarking & Figure Generation**
   - Run `python telemetry/generate_paper_figures.py` to generate the $N = 50$ benchmark CSV and 300 DPI figures.
   - Verify that all generated figures conform to IEEE single-column and double-column publication standards.
5. **Sprint 4: Blueprint Manuscript Assembly & Final Audit**
   - Assemble experimental results into `docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md`.
   - Execute the automated audit script to guarantee zero compliance infractions.
