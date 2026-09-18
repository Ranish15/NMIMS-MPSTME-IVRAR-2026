# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 12 - Immersive VR Phishing Simulation vs 2D Web-Based Cybersecurity Training
## Target Publication: IEEE Transactions on Visualization and Computer Graphics (TVCG) / ACM Transactions on Computer-Human Interaction (TOCHI) / Computers & Security

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature search was conducted across CrossRef, IEEE Xplore, the ACM Digital Library, and Elsevier ScienceDirect to identify seminal and contemporary research at the intersection of usable cybersecurity, human visual attention, immersive virtual reality simulations, and cognitive decision heuristics. Candidate articles were evaluated against four strict inclusion criteria:
1. Peer-reviewed publication in premier venues in human factors, usable security, educational technologies, or information systems (ACM CHI, ACM SOUPS, ACM TOIT, IEEE TLT, Computers & Security, Decision Support Systems).
2. Rigorous empirical evaluation comparing training modalities (experiential, game-based, or VR vs passive instruction) or measuring eye-gaze attention metrics across phishing indicators.
3. Explicit analysis of cognitive biases (urgency, authority, familiarity) or longitudinal knowledge retention over delayed post-test intervals ($> 7\text{ days}$).
4. Active, verified CrossRef Digital Object Identifier (DOI) resolution.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Dhamija2006` | Why phishing works | Human vulnerability to visual deception and spoofing | Visual deception susceptibility: $P(\text{Deceived}) = f(\text{Visual Similarity}, \text{Attention})$ | Design of visual homograph and browser UI spoof cues | [10.1145/1124772.1124861](https://doi.org/10.1145/1124772.1124861) |
| `Sheng2007` | Anti-Phishing Phil: The Design and Evaluation of an Interactive Game to Teach People Not to Fall for Phish | Interactive anti-phishing pedagogical games | Pre/post knowledge gain delta: $\Delta K = K_{\text{post}} - K_{\text{pre}}$, false positive/negative trade-offs | Gamified active learning principles applied in VR simulation | [10.1145/1280680.1280692](https://doi.org/10.1145/1280680.1280692) |
| `Kumaraguru2010` | Teaching Johnny not to fall for phish | Embedded anti-phishing training and teachable moments | Immediate vs delayed retention decay: $R(t) = R_0 \cdot e^{-\lambda t}$ | 14-day delayed retention evaluation protocol design | [10.1145/1754393.1754396](https://doi.org/10.1145/1754393.1754396) |
| `Vishwanath2011` | Why do people get phished? Testing individual differences in phishing vulnerability within an integrated, information processing model | Cognitive heuristics and information processing models | Dual-process theory (Heuristic System 1 vs Systematic System 2 processing) | Scenarios triggering Urgency, Authority, and Scarcity biases | [10.1016/j.dss.2011.03.002](https://doi.org/10.1016/j.dss.2011.03.002) |
| `Baltuttis2024` | Effects of visual risk indicators on phishing detection behavior: An eye-tracking experiment | Eye-tracking analysis of phishing indicator fixations | Fixation dwell time ratio: $T_{\text{inspect}} = \frac{T_{\text{cues}}}{T_{\text{total}}}$, gaze heatmaps | Eye-gaze raycast tracking in `EyeGazeAttentionTracker.cs` | [10.1016/j.cose.2024.103940](https://doi.org/10.1016/j.cose.2024.103940) |
| `Buttussi2021` | A Comparison of Procedural Safety Training in Three Conditions: Virtual Reality Headset, Smartphone, and Printed Materials | Experiential VR safety training vs 2D digital media | Multimodal retention advantage: $E_{\text{VR}} > E_{\text{2D}}$ across complex threat tasks | Empirical comparison framework between VR and 2D web training | [10.1109/TLT.2020.3033766](https://doi.org/10.1109/TLT.2020.3033766) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Dhamija, Tygar, & Hearst (2006) - Why Phishing Works
- **Core Contribution:** Established that visual deception succeeds because users rely on surface-level visual cues (such as familiar logos, padlocks, and color palettes) rather than validating digital certificates or domain syntax. Even technically knowledgeable users showed a $23\%$ failure rate on high-fidelity clones.
- **Project Role:** Directly informs the threat modeling executed by `D021 - Madhav Gaonkar` in `PhishingThreatSimulationManager.cs`, specifically governing the generation of realistic homograph domains and deceptive visual security badges.

### 3.2 Sheng et al. (2007) - Anti-Phishing Phil Interactive Game
- **Core Contribution:** Demonstrated that active, interactive learning games significantly outperform passive reading or video watching in training users to identify deceptive URLs and avoid phishing traps.
- **Project Role:** Provides the theoretical foundation for experiential, consequences-driven training implemented in Unity VR rather than static corporate compliance videos.

### 3.3 Kumaraguru, Sheng, Acquisti, Cranor, & Hong (2010) - Teaching Johnny Not to Fall for Phish
- **Core Contribution:** Formulated the concept of embedded training delivered at "teachable moments" and demonstrated that while immediate post-test training produces substantial knowledge gains, knowledge decays sharply over delayed retention intervals (7 to 28 days) unless reinforced by experiential episodic memory.
- **Project Role:** Dictates the 14-day delayed retention testing protocol executed by `I080 - Anuvrat Tripathi`, establishing the baseline forgetting curve against which the VR simulation is benchmarked.

### 3.4 Vishwanath, Herath, Chen, Wang, & Rao (2011) - Information Processing & Heuristic Vulnerability
- **Core Contribution:** Proposed an integrated information processing model proving that phishing vulnerability is mediated by cognitive heuristics: users switch from systematic analytical scrutiny (System 2) to automated heuristic shortcuts (System 1) when presented with time pressure, authority cues, or fear of negative consequences.
- **Project Role:** Governs the classification and algorithmic induction of cognitive biases (Authority, Urgency, Scarcity, Familiarity) evaluated in Group 12's experimental trials.

### 3.5 Baltuttis & Teubner (2024) - Eye-Tracking Phishing Detection
- **Core Contribution:** Leveraged high-precision eye tracking to demonstrate that successful threat detection correlates directly with visual fixation duration on diagnostic Area-of-Interest (AOI) targets, particularly sender address headers and hyperlink hover URLs, rather than body text.
- **Project Role:** Directly implemented in `EyeGazeAttentionTracker.cs` by `D065 - Diya Shah`, measuring real-time AOI fixation duration, saccade frequency, and threat inspection ratios.

### 3.6 Buttussi & Chittaro (2021) - VR vs 2D Safety Training
- **Core Contribution:** Proved through rigorous empirical testing that immersive VR safety training elicits stronger emotional arousal and deeper sensorimotor encoding, resulting in superior procedural recall and threat mitigation over longitudinal retention intervals compared to mobile or desktop screens.
- **Project Role:** Provides the comparative empirical framework and statistical power guidelines for benchmarking VR vs 2D web-based cybersecurity training.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While usable security literature has evaluated 2D games (`Sheng2007`), cognitive heuristics (`Vishwanath2011`), and eye-tracking attention (`Baltuttis2024`), **no prior investigation has integrated real-time eye-gaze tracking within an immersive 6-DoF VR office environment to measure both visual attention allocation and longitudinal cognitive bias retention across a 14-day interval compared to standard enterprise 2D web training**. Group 12 addresses this critical gap.
