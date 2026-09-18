# Research and Implementation Guide: VR Cybersecurity Escape Room

## Project: IVRAR Group 04
## Target Venue: IEEE Transactions on Learning Technologies / Computers & Security / IEEE VR

---

## 1. Mathematical and Algorithmic Formulation

### 1.1 Pedagogical Efficacy: Hake's Normalized Learning Gain
To evaluate cybersecurity awareness training across participants with varying baseline competencies, learning efficacy is quantified via Hake's normalized gain index $g$:

$$g = \frac{\text{Post} - \text{Pre}}{100 - \text{Pre}}$$

where $\text{Pre}$ and $\text{Post}$ represent percentage scores on standardized pre- and post-training security assessments. Following Hake's established empirical thresholds:
- **Low Gain:** $g < 0.30$ (typical of passive compliance slide decks)
- **Medium Gain:** $0.30 \le g \le 0.70$ (typical of interactive web quizzes)
- **High Gain:** $g > 0.70$ (achieved through experiential immersive simulations)

### 1.2 Physical Social Engineering Attack Vector Probability
In an enterprise facility with $K$ distinct security checkpoints (Reception Turnstile, Open Workstation, Server Vault), the overall probability of a successful intrusion breach $P(\text{Breach})$ is modeled as:

$$P(\text{Breach}) = 1 - \prod_{k=1}^K (1 - p_k)$$

where $p_k \in [0, 1]$ represents the participant's failure rate at checkpoint $k$:
1. $p_{\text{tailgate}}$: Permitting an unbadged avatar to enter through an open door ($T_{\text{linger}} > 3.5$ s).
2. $p_{\text{usb}}$: Connecting an untrusted baiting storage drive into an active terminal.
3. $p_{\text{shoulder}}$: Entering a PIN code without body/hand gaze occlusion.

### 1.3 Keypad Gaze Occlusion & Line-of-Sight Geometry
Adversary observation of authentication credentials during PIN keypad entry is modeled geometrically:

$$\Delta t_{\text{exposed}} = \int_{0}^{T_{\text{entry}}} \mathbb{I}\left( \frac{\mathbf{v}_{\text{adv}} \cdot \mathbf{n}_{\text{keypad}}}{\|\mathbf{v}_{\text{adv}}\|} > \cos(\theta_{\text{crit}}) \right) \cdot (1 - O_{\text{shield}}) \, dt$$

where $\mathbf{v}_{\text{adv}}$ is the gaze vector from the adversary avatar to the keypad, $\mathbf{n}_{\text{keypad}}$ is the normal vector of the input terminal, $\theta_{\text{crit}} \approx 45^{\circ}$ is the visual capture cone, and $O_{\text{shield}} \in \{0, 1\}$ indicates whether the user's non-dominant hand or body mesh colliders occlude line-of-sight.

### 1.4 Technoeconomic Operational Parity
The economic justification for replacing recurring physical red-team contractor audits with automated VR escape training is formulated via dimensionless cost parity $\kappa$:

$$\kappa = \frac{\text{OpEx}_{\text{VR}}}{\text{OpEx}_{\text{Traditional}}} = \frac{C_{\text{hmd\_maintenance}} + C_{\text{scenario\_authoring}}}{C_{\text{consultant\_fees}} + C_{\text{physical\_props}} + C_{\text{admin\_overhead}}}$$

The capital investment payback horizon in operating months is:

$$\text{Payback Months} = \frac{K_{\text{capex}}}{1 - \kappa} \times 12$$

---

## 2. Individual Student Work Boundaries & Responsibilities

```
===================================================================================================
Student Roll & Name        Assigned Technical Module                       Primary Deliverable
===================================================================================================
K068 - Rishi Vishwakarma   Cyber Vulnerability Architect                   Assets/Scripts/CyberEscapeRoomManager.cs
                                                                           (NIST SP 800-53 Vectors & Threat Logic)
K075 - Rahul Behera        XR Systems Architect & Multi-Zone Facility      Assets/Scripts/CyberEscapeRoomManager.cs
                                                                           (Facility Geometry, OpenXR Rig, Puzzles)
K081 - Srinidi Subramaniam Human Factors & Security QA Lead                Assets/Scripts/SocialEngineeringTelemetryLogger.cs
                                                                           (Hake Gain Analytics & Telemetry CSV)
===================================================================================================
```

### 2.1 K068 - Rishi Vishwakarma (Cyber Vulnerability Architect)
- Implement NIST SP 800-53 attack rules for turnstile tailgating, rogue USB baiting, and shoulder surfing.
- Author adversary NPC behavioral state machines and pretexting dialogue prompts.
- Validate vulnerability scoring thresholds and security compliance criteria.
- **Git Branch:** `feat/k068-cyber-vulnerability-`
- **Oral Viva Focus:** NIST SP 800-53 physical control mappings, social engineering psychological levers, and quantitative breach probability formulations.

### 2.2 K075 - Rahul Behera (XR Systems Architect)
- Construct the 3-zone enterprise facility in Unity 2022.3 LTS with OpenXR.
- Implement physical hand grabbing for RFID keycards and USB flash drives using Unity XR Interaction Toolkit.
- Design the PIN keypad terminal with raycast gaze-occlusion detection and dynamic digit scrambling.
- **Git Branch:** `feat/k075-xr-systems-architect`
- **Oral Viva Focus:** OpenXR interaction pipeline, grab pose physics, spatial UI rendering, and motion-to-photon latency budgeting.

### 2.3 K081 - Srinidi Subramaniam (Human Factors & Security QA Lead)
- Author `SocialEngineeringTelemetryLogger.cs` calculating Hake's normalized learning gain $g$ and capturing 90 Hz CSV telemetry.
- Design pre- and post-training diagnostic questionnaires evaluating cognitive retention.
- Execute within-subjects statistical evaluations (Student's t-test, Cohen's $d$, Wilcoxon signed-rank test).
- **Git Branch:** `feat/k081-human-factors-securi`
- **Oral Viva Focus:** Hake's normalized gain mathematics, NASA-TLX workload evaluation, and statistical significance of training transfer.

---

## 3. Verified Foundational Papers

The project architecture and empirical protocol are grounded in 6 verified literature foundations:

1. **Hake (1998)**
   - *Title:* Interactive-engagement versus traditional methods: A six-thousand-student survey of mechanics test data for introductory physics courses
   - *Journal:* American Journal of Physics, vol. 66, no. 1, pp. 64-74
   - *DOI:* [10.1119/1.18809](https://doi.org/10.1119/1.18809)
   - *Role:* Theoretical and mathematical definition of normalized learning gain ($g$).

2. **Mouton, Leenen, & Venter (2016)**
   - *Title:* Social engineering attack examples, templates and scenarios
   - *Journal:* Computers & Security, vol. 59, pp. 186-209
   - *DOI:* [10.1016/j.cose.2016.03.004](https://doi.org/10.1016/j.cose.2016.03.004)
   - *Role:* Taxonomy and scenario generation for simulated physical social-engineering attacks.

3. **Bošnjak & Brumen (2020)**
   - *Title:* Shoulder surfing experiments: A systematic literature review
   - *Journal:* Computers & Security, vol. 99, p. 102023
   - *DOI:* [10.1016/j.cose.2020.102023](https://doi.org/10.1016/j.cose.2020.102023)
   - *Role:* Line-of-sight exposure parameters and defensive shielding mechanics.

4. **Workman (2007)**
   - *Title:* Gaining Access with Social Engineering: An Empirical Study of the Threat
   - *Journal:* Information Systems Security, vol. 16, no. 6, pp. 315-331
   - *DOI:* [10.1080/10658980701788165](https://doi.org/10.1080/10658980701788165)
   - *Role:* Empirical tailgating compliance baselines and perimeter penetration tests.

5. **Vykopal, Seda, Švábenský, & Čeleda (2023)**
   - *Title:* Smart Environment for Adaptive Learning of Cybersecurity Skills
   - *Journal:* IEEE Transactions on Learning Technologies, vol. 16, no. 2, pp. 237-250
   - *DOI:* [10.1109/TLT.2022.3216345](https://doi.org/10.1109/TLT.2022.3216345)
   - *Role:* Gamified feedback systems, difficulty scaling, and automated telemetry in cyber defense.

6. **Williams & El-Gayar (2021)**
   - *Title:* Design of a Virtual Cybersecurity Escape Room
   - *Journal:* Lecture Notes in Networks and Systems, vol. 310, pp. 67-82
   - *DOI:* [10.1007/978-3-030-84614-5_6](https://doi.org/10.1007/978-3-030-84614-5_6)
   - *Role:* Structural architecture and puzzle dependency mapping in virtual escape rooms.

---

## 4. Step-by-Step Implementation Roadmap

1. **Sprint 0: Toolchain & Baseline Verification**
   - Verify Unity 2022.3 LTS, OpenXR package, and XR Interaction Toolkit.
   - Run `python telemetry/cyber_training_economics.py` to confirm technoeconomic parity metrics.
2. **Sprint 1: Facility Architecture & Attack State Machine**
   - Model the 3-zone corporate environment (Reception, Workstation, Server Vault).
   - Implement `Assets/Scripts/CyberEscapeRoomManager.cs` with student `# TODO` implementations.
3. **Sprint 2: Telemetry Logger & Hake Gain Calculation**
   - Author `Assets/Scripts/SocialEngineeringTelemetryLogger.cs` for 90 Hz CSV telemetry capture.
   - Validate Hake normalized learning gain calculations across diagnostic pre/post tests.
4. **Sprint 3: Empirical Benchmarking & Figure Generation**
   - Run `python telemetry/generate_paper_figures.py` to generate the $N = 50$ benchmark CSV and 300 DPI figures.
   - Confirm that all plots adhere to IEEE publication standards.
5. **Sprint 4: Blueprint Manuscript Assembly & Final Audit**
   - Assemble experimental findings into `docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md`.
   - Execute the automated compliance audit script to ensure zero defects.
