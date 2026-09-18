# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 09 - AI-Adaptive VR Social-Engineering Simulation
## Target Publication: Computers & Security / IEEE Transactions on Information Forensics and Security / ACM Transactions on Computer-Human Interaction (TOCHI)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, IEEE Xplore, ScienceDirect, ACM Digital Library, and Sage Journals to identify foundational works on social engineering taxonomy, phishing susceptibility cognitive modeling, Cialdini persuasion principles, and virtual environment social interaction. Candidate works were evaluated against four strict inclusion criteria:
1. Peer-reviewed indexing in premier cybersecurity, human-computer interaction, or applied psychology venues (Computers & Security, JISA, ACM CHI, ACM SOUPS, Communication Research, Psychological Inquiry).
2. Theoretical and mathematical formulation of cognitive susceptibility (e.g., Suspicion, Cognition, and Automaticity Model - SCAM) or persuasion mechanics.
3. Quantitative empirical benchmarking of phishing detection accuracy, eye-gaze visual fixation, or behavioral compliance rates.
4. Active CrossRef Digital Object Identifier (DOI) verification.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Krombholz2015` | Advanced social engineering attacks | Social engineering attack taxonomy & attack vectors | Multi-channel pretexting taxonomy: Physical, Social, Technical, Reverse-Social | Scenario generation framework for enterprise VR pretexts | [10.1016/j.jisa.2014.09.005](https://doi.org/10.1016/j.jisa.2014.09.005) |
| `Sheng2010` | Who falls for phish? A demographic analysis of phishing susceptibility and effectiveness of interventions | Demographic & cognitive susceptibility to phishing | Logistic regression of demographic predictors: $\text{logit}(P) = \beta_0 + \sum \beta_i X_i$ | Structuring employee cohort testing and risk factor weighting | [10.1145/1753326.1753383](https://doi.org/10.1145/1753326.1753383) |
| `Vishwanath2018` | Suspicion, Cognition, and Automaticity Model of Phishing Susceptibility | Cognitive modeling of email & physical deception | SCAM path model: Suspicion $\to$ Cognitive Elaboration $\to$ Threat Detection | Primary cognitive framework for attention and decision state machine | [10.1177/0093650215627483](https://doi.org/10.1177/0093650215627483) |
| `Ferreira2015` | Principles of Persuasion in Social Engineering and Their Use in Phishing | Cialdini persuasion vectors in cyber attacks | Tactic weighting matrix: Authority, Scarcity, Urgency, Consistency, Social Proof | Dialogue branch node categorization in `SocialEngineeringDialogueTreeManager.cs` | [10.1007/978-3-319-20376-8_4](https://doi.org/10.1007/978-3-319-20376-8_4) |
| `Sheng2007` | Anti-Phishing Phil: the design and evaluation of an interactive game to teach people not to fall for phish | Serious games for anti-phishing training | Formative scaffolding: immediate consequence feedback vs passive lecturing | Immediate experiential feedback loops in VR interactive scenarios | [10.1145/1280680.1280692](https://doi.org/10.1145/1280680.1280692) |
| `Blascovich2002` | Immersive Virtual Environment Technology as a Methodological Tool for Social Psychology | Social interaction in immersive virtual environments | Social presence and behavioral realism thresholding: $P_{\text{social}} = f(R_{\text{behav}}, R_{\text{vis}})$ | Guiding avatar non-verbal gestures, gaze tracking, and stress induction | [10.1207/S15327965PLI1302_01](https://doi.org/10.1207/S15327965PLI1302_01) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Krombholz, Hobel, Huber, & Weippl (2015) - Advanced Social Engineering
- **Core Contribution:** Developed a comprehensive taxonomy categorizing social engineering attacks into physical pretexting, telephone social engineering (vishing), spear phishing, and reverse social engineering.
- **Project Role:** Directly shapes the multi-scenario repository in `Assets/Scripts/SocialEngineeringDialogueTreeManager.cs`, focusing specifically on on-site visitor impersonation and urgent remote IT support scams.

### 3.2 Sheng, Holbrook, Kumaraguru, Cranor, & Downs (2010) - Phishing Susceptibility
- **Core Contribution:** Conducted large-scale empirical studies establishing that passive training material (leaflets, compliance slides) fails to produce durable behavioral resistance against sophisticated pretexting, whereas embedded interactive training reduces susceptibility by over 40%.
- **Project Role:** Establishes the baseline control group expectations and metric rubrics used in our comparative benchmark evaluation.

### 3.3 Vishwanath, Harrison, & Ng (2018) - SCAM Model of Phishing Susceptibility
- **Core Contribution:** Introduced the Suspicion, Cognition, and Automaticity Model (SCAM), proving that individuals fall for social engineering lures when processing information automatically via heuristic cues rather than systematic cognitive elaboration.
- **Project Role:** Theoretical backbone for `Assets/Scripts/PhishingGazeTelemetryLogger.cs`. The simulation intentionally tracks whether employees pause to visually inspect security credentials (heuristic vs systematic processing).

### 3.4 Ferreira, Coventry, & Lenzini (2015) - Principles of Persuasion
- **Core Contribution:** Mapped Robert Cialdini's weapons of influence (Authority, Scarcity, Urgency, Liking, Reciprocity, Social Proof) directly into social engineering attack narratives, determining that artificial urgency coupled with perceived authority produces the highest rates of employee compliance.
- **Project Role:** Governs the branching state machine in `SocialEngineeringDialogueTreeManager.cs`, where the AI attacker dynamically shifts persuasion vectors if initial lures are resisted.

### 3.5 Sheng, Magnien, Kumaraguru, et al. (2007) - Anti-Phishing Phil
- **Core Contribution:** Pioneered usable privacy and security game design, proving that teaching users through interactive failure-state consequences significantly improves recall and URL inspection retention over standard compliance web pages.
- **Project Role:** Informs the post-encounter debrief UI in VR, displaying immediate visual replays of missed deceptive artifacts.

### 3.6 Blascovich, Loomis, Beall, Swinth, Hoyt, & Bailenson (2002) - Immersive Social Psychology
- **Core Contribution:** Established foundational theoretical models for interpersonal communication and social influence in immersive virtual reality, demonstrating that humans exhibit physiological and psychological stress responses to virtual social agents that closely mirror physical human-to-human interactions.
- **Project Role:** Justifies the use of fully immersive VR headsets over 2D screens: the visceral social pressure exerted by a physically co-present avatar in VR activates genuine corporate compliance reflexes.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While previous studies designed 2D web games (`Sheng2007`) or theoretical susceptibility models (`Vishwanath2018`), **none systematically evaluated an AI-adaptive virtual reality simulation pairing dynamic conversational branch trees with 90 Hz eye-gaze attention tracking to measure real-time employee hesitation, lure fixation dwell times, and persuasion breakdown**. Group 09 addresses this gap.
