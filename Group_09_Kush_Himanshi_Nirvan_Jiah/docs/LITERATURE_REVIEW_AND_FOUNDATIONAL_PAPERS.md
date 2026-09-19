# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 09 - AI-Adaptive VR Social-Engineering Simulation
## Target Publication: Computers & Security / IEEE Transactions on Information Forensics and Security / ACM Transactions on Computer-Human Interaction (TOCHI)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, IEEE Xplore, ScienceDirect, ACM Digital Library, and Springer Nature to identify foundational and cutting-edge works on social engineering taxonomy, phishing susceptibility cognitive modeling, immersive virtual reality cybersecurity education, and ecological simulation defense. Candidate works were evaluated against four strict inclusion criteria:
1. Peer-reviewed indexing in premier cybersecurity, human-computer interaction, or applied simulation venues (Virtual Reality, Computers & Security, ACM CHI, Symmetry, The Journal of Defense Modeling and Simulation).
2. Curated ratio of exactly two seminal theoretical anchors paired with four recent (2022-2026) high-impact empirical investigations.
3. Quantitative empirical benchmarking of phishing detection accuracy, eye-gaze visual fixation, or simulated behavioral attack mitigation.
4. Active CrossRef Digital Object Identifier (DOI) verification resolving with HTTP 200 status.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Sheng2010` | Who falls for phish? A demographic analysis of phishing susceptibility and effectiveness of interventions | Demographic & cognitive susceptibility to phishing | Logistic regression of demographic predictors: $\text{logit}(P) = \beta_0 + \sum \beta_i X_i$ | Structuring employee cohort testing and baseline susceptibility benchmarks | [10.1145/1753326.1753383](https://doi.org/10.1145/1753326.1753383) |
| `Vishwanath2018` | Suspicion, Cognition, and Automaticity Model of Phishing Susceptibility | Cognitive modeling of email & physical deception | SCAM path model: Suspicion $\to$ Cognitive Elaboration $\to$ Threat Detection | Primary cognitive framework for attention and decision state machine | [10.1177/0093650215627483](https://doi.org/10.1177/0093650215627483) |
| `Rehman2026` | Evaluating the impact of immersive virtual reality in cybersecurity education for user empowerment against cyber threats | Immersive VR in cybersecurity defense and threat empowerment | Statistical analysis of experiential inoculation vs passive instructional delivery | Validating immersive threat encounters over standard slide decks | [10.1007/s10055-025-01309-8](https://doi.org/10.1007/s10055-025-01309-8) |
| `Abril2025` | Exploring a novel approach to cybersecurity: the role of ecological simulations on cybersecurity risk behaviors | Ecological simulated environments & risk behavior modulation | Behavioral compliance state transitions under ambient environmental triggers | Informing realistic 3D corporate office context and conversational pacing | [10.1007/s10055-025-01228-8](https://doi.org/10.1007/s10055-025-01228-8) |
| `Alnajim2023` | Exploring Cybersecurity Education and Training Techniques: A Comprehensive Review of Traditional, Virtual Reality, and Augmented Reality Approaches | Comparative meta-analysis of XR vs traditional training modalities | Multi-dimensional pedagogic effectiveness taxonomy and retention scoring | Structuring the comparative benchmark ($N=50$) and cognitive workload metrics | [10.3390/sym15122175](https://doi.org/10.3390/sym15122175) |
| `Shin2025` | Simulating cyber defense: the impact of phishing training and system updates on mitigating damage from hybrid phishing and watering hole attacks | Multi-stage cyber defense simulation & attack mitigation dynamics | Dynamic attack progression state models and enterprise breach suppression rates | Framework for non-linear attack trees and technoeconomic exposure calculations | [10.1177/15485129251365259](https://doi.org/10.1177/15485129251365259) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Sheng, Holbrook, Kumaraguru, Cranor, & Downs (2010) - Phishing Susceptibility
- **Core Contribution:** Conducted large-scale empirical studies establishing that passive training material (leaflets, compliance slides) fails to produce durable behavioral resistance against sophisticated pretexting, whereas embedded interactive training reduces susceptibility by over 40%.
- **Project Role:** Establishes the baseline control group expectations and metric rubrics used in our comparative benchmark evaluation.

### 3.2 Vishwanath, Harrison, & Ng (2018) - SCAM Model of Phishing Susceptibility
- **Core Contribution:** Introduced the Suspicion, Cognition, and Automaticity Model (SCAM), proving that individuals fall for social engineering lures when processing information automatically via heuristic cues rather than systematic cognitive elaboration.
- **Project Role:** Theoretical backbone for `Assets/Scripts/PhishingGazeTelemetryLogger.cs`. The simulation intentionally tracks whether employees pause to visually inspect security credentials (heuristic vs systematic processing).

### 3.3 Rehman, Zahid, Khattak, et al. (2026) - Immersive VR Cybersecurity Education
- **Core Contribution:** Demonstrated that immersive VR modalities significantly enhance threat empowerment and retention against evolving social engineering tactics compared to screen-based lectures.
- **Project Role:** Validates the experiential embodiment hypothesis, proving that interactive confrontation with deceptive agents yields statistically superior behavioral retention.

### 3.4 Abril, Read, & Horton (2025) - Ecological Simulations & Risk Behaviors
- **Core Contribution:** Formulated ecological simulation frameworks demonstrating how contextual presence modulates cybersecurity risk behaviors and reduces reflexive compliance.
- **Project Role:** Guides the high-fidelity office environment design and ambient conversational cues implemented in `Assets/Scripts/SocialEngineeringDialogueTreeManager.cs`.

### 3.5 Alnajim, Alahmadi, & Aksoy (2023) - XR Cybersecurity Education Techniques
- **Core Contribution:** Provided a rigorous systematic review contrasting traditional e-learning, VR, and AR approaches, demonstrating consistent 30-50% improvements in experiential threat recognition.
- **Project Role:** Forms the foundational benchmark criteria for comparing traditional video lectures against immersive VR simulations across cognitive workload and usability dimensions.

### 3.6 Shin, Park, & Lee (2025) - Simulating Cyber Defense Against Hybrid Attacks
- **Core Contribution:** Developed simulation methodologies evaluating organizational defense efficacy against adaptive phishing, highlighting how iterative simulation training prevents credential compromise.
- **Project Role:** Directly influences the non-linear decision graph and technoeconomic operational cost parity model evaluating corporate risk exposure reduction.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While previous studies analyzed 2D interventions (`Sheng2010`) or theoretical susceptibility models (`Vishwanath2018`), recent advances demonstrate the efficacy of immersive environments (`Rehman2026`, `Abril2025`, `Alnajim2023`, `Shin2025`). **However, none systematically evaluate an AI-adaptive virtual reality simulation pairing dynamic conversational branch trees with 90 Hz eye-gaze attention tracking to measure real-time employee hesitation, lure fixation dwell times, and persuasion breakdown**. Group 09 addresses this critical gap.
