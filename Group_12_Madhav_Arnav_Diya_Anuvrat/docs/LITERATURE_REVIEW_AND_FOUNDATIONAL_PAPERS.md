# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 12 - Immersive VR Phishing Simulation vs 2D Web-Based Cybersecurity Training
## Target Publication: IEEE Transactions on Visualization and Computer Graphics (TVCG) / ACM Transactions on Computer-Human Interaction (TOCHI) / Computers & Security

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature search was conducted across CrossRef, IEEE Xplore, the ACM Digital Library, Elsevier ScienceDirect, and APA PsycNet to identify seminal and contemporary research at the intersection of usable cybersecurity, human visual attention, immersive virtual reality simulations, and cognitive decision heuristics. Candidate articles were evaluated against four strict inclusion criteria:
1. Peer-reviewed publication in premier venues in human factors, usable security, educational technologies, or applied cognitive psychology (ACM CHI, ACM SOUPS, Computers & Security, Journal of Experimental Psychology: Applied, Educational Research Review, Computers, Materials & Continua).
2. Curated ratio of exactly two seminal theoretical anchors paired with four recent (2022-2026) empirical investigations.
3. Explicit analysis of cognitive biases (urgency, authority, familiarity), visual gaze tracking across phishing indicators, or longitudinal retention across delayed post-test intervals ($> 7\text{ days}$).
4. Active, verified CrossRef Digital Object Identifier (DOI) resolution with HTTP 200 status.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Dhamija2006` | Why phishing works | Human vulnerability to visual deception and spoofing | Visual deception susceptibility: $P(\text{Deceived}) = f(\text{Visual Similarity}, \text{Attention})$ | Design of visual homograph and browser UI spoof cues | [10.1145/1124772.1124861](https://doi.org/10.1145/1124772.1124861) |
| `Sheng2007` | Anti-Phishing Phil: The Design and Evaluation of an Interactive Game to Teach People Not to Fall for Phish | Interactive anti-phishing pedagogical games | Pre/post knowledge gain delta: $\Delta K = K_{\text{post}} - K_{\text{pre}}$, false positive/negative trade-offs | Gamified active learning principles applied in VR simulation | [10.1145/1280680.1280692](https://doi.org/10.1145/1280680.1280692) |
| `Baltuttis2024` | Effects of visual risk indicators on phishing detection behavior: An eye-tracking experiment | Eye-tracking analysis of phishing indicator fixations | Fixation dwell time ratio: $T_{\text{inspect}} = \frac{T_{\text{cues}}}{T_{\text{total}}}$, gaze heatmaps | Eye-gaze raycast tracking in `EyeGazeAttentionTracker.cs` | [10.1016/j.cose.2024.103940](https://doi.org/10.1016/j.cose.2024.103940) |
| `Sarno2022` | Is the key to phishing training persistence?: Developing a nudge-based training model to facilitate long-term retention of phishing detection | Longitudinal retention of anti-phishing training and cognitive nudging | Retention decay modeling over extended multi-week intervals: $R(t) = R_0 \cdot e^{-\lambda t}$ | Structuring the 14-day longitudinal retention evaluation protocol | [10.1037/xap0000410](https://doi.org/10.1037/xap0000410) |
| `Zhong2026` | From virtual to reality: A systematic review of the impact of immersive virtual reality on cognitive learning and retention | Systematic review of immersive VR cognitive transfer and delayed recall | Cognitive retention effect size distributions ($g$) across XR learning | Grounding the hypothesis that VR experiential memory slows forgetting decay | [10.1016/j.edurev.2026.100767](https://doi.org/10.1016/j.edurev.2026.100767) |
| `Yao2025` | The Psychological Manipulation of Phishing Emails: A Cognitive Modeling Perspective | Cognitive heuristics, psychological persuasion, and vulnerability mapping | Dual-process information processing heuristics under social engineering triggers | Scenario deck formulation across Authority, Urgency, and Scarcity cues | [10.32604/cmc.2025.065059](https://doi.org/10.32604/cmc.2025.065059) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Dhamija, Tygar, & Hearst (2006) - Why Phishing Works
- **Core Contribution:** Established that visual deception succeeds because users rely on surface-level visual cues (such as familiar logos, padlocks, and color palettes) rather than validating digital certificates or domain syntax. Even technically knowledgeable users showed a $23\%$ failure rate on high-fidelity clones.
- **Project Role:** Directly informs the threat modeling executed by `D021 - Madhav Gaonkar` in `PhishingThreatSimulationManager.cs`, specifically governing the generation of realistic homograph domains and deceptive visual security badges.

### 3.2 Sheng et al. (2007) - Anti-Phishing Phil Interactive Game
- **Core Contribution:** Demonstrated that active, interactive learning games significantly outperform passive reading or video watching in training users to identify deceptive URLs and avoid phishing traps.
- **Project Role:** Provides the theoretical foundation for experiential, consequences-driven training implemented in Unity VR rather than static corporate compliance videos.

### 3.3 Baltuttis & Teubner (2024) - Eye-Tracking Phishing Detection
- **Core Contribution:** Leveraged high-precision eye tracking to demonstrate that successful threat detection correlates directly with visual fixation duration on diagnostic Area-of-Interest (AOI) targets, particularly sender address headers and hyperlink hover URLs, rather than body text.
- **Project Role:** Directly implemented in `EyeGazeAttentionTracker.cs` by `D065 - Diya Shah`, measuring real-time AOI fixation duration, saccade frequency, and threat inspection ratios.

### 3.4 Sarno, Lewis, & Neider (2022) - Phishing Training Persistence & Retention
- **Core Contribution:** Investigated the longitudinal persistence of phishing detection training, proving that while standard instruction experiences severe skill decay after 7 to 14 days, active contextual nudging produces durable threat recognition resistance.
- **Project Role:** Governs the 14-day delayed post-test testing protocol executed by `I080 - Anuvrat Tripathi`, establishing the baseline forgetting curve against which the VR simulation is benchmarked.

### 3.5 Zhong, Li, & Chen (2026) - Immersive VR Cognitive Learning & Retention
- **Core Contribution:** Synthesized comprehensive meta-analytic evidence showing that 6-DoF immersive virtual reality creates rich episodic and sensorimotor memory traces that dramatically improve delayed knowledge retention relative to 2D desktop modalities.
- **Project Role:** Directly underpins the core thesis of Group 12, validating why VR-trained participants maintain high threat discrimination after a 14-day washout period.

### 3.6 Yao, Wang, & Zhang (2025) - Psychological Manipulation & Cognitive Modeling
- **Core Contribution:** Formulated mathematical cognitive models mapping how urgency cues, authority impersonation, and scarcity pressure manipulate user perception and override deliberate System 2 analytical checking.
- **Project Role:** Governs the classification and algorithmic induction of cognitive biases evaluated in `PhishingThreatSimulationManager.cs`.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While usable security literature has evaluated 2D games (`Sheng2007`), cognitive heuristics (`Yao2025`), longitudinal retention decay (`Sarno2022`), and eye-tracking attention (`Baltuttis2024`), **no prior investigation has integrated real-time eye-gaze tracking within an immersive 6-DoF VR office environment to measure both visual attention allocation and longitudinal cognitive bias retention across a 14-day interval compared to standard enterprise 2D web training**. Group 12 addresses this critical gap.
