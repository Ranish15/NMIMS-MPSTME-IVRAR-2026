# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 07 - Interactive VR Spatial Crime Scene Reconstruction
## Target Publication: Forensic Science International / Science & Justice / IEEE Transactions on Visualization and Computer Graphics (TVCG)
## Course Code: 702COI002 (Institute Open Elective)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, ScienceDirect, IEEE Xplore, and Scopus to identify foundational works on 3D crime scene digitization, spatial forensic reconstruction, immersive evidence tagging, and investigative timeline sequencing. Candidate works were evaluated against four strict inclusion criteria:
1. Exact **2 Seminal : 4 Recent (2022–2026)** ratio with 100% active HTTP 200 DOIs verified via CrossRef REST APIs.
2. Peer-reviewed indexing in premier forensic science and virtual reality venues (*Forensic Science International*, *Science & Justice*, *Virtual Reality*, *WIREs Forensic Science*).
3. Inclusion of formal mathematical and psychometric models for spatial localization error, Kendall-Tau rank correlation, and chain-of-custody digital integrity (ISO/IEC 27037).
4. Direct alignment with MBA Tech Computer Engineering competencies in software architecture, digital forensics, and technoeconomic operational modeling.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Publication Year | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|---|
| `Buck2019` | 3D crime scene reconstruction | 2019 (Seminal) | Forensic photogrammetry & ballistics | Epipolar geometry, 3D line-of-flight ballistic vector solving | Spatial evidence tagging and 3D coordinate localization in Unity | [10.1016/j.forsciint.2019.109901](https://doi.org/10.1016/j.forsciint.2019.109901) |
| `Mayne2020` | Virtual reality for teaching and learning in crime scene investigation | 2020 (Seminal) | Pedagogical efficacy of VR in forensics | Formative assessment rubrics, search traversal coverage | Benchmark evaluation of student investigator performance vs 2D baseline | [10.1016/j.scijus.2020.07.006](https://doi.org/10.1016/j.scijus.2020.07.006) |
| `Albeedan2024` | Designing and evaluation of a mixed reality system for crime scene investigation training: a hybrid approach | 2024 (Recent) | Mixed reality crime scene training | Spatial presence evaluation, diagnostic tagging error rate | Multi-modal XR evidence tagging and UI billboard interaction | [10.1007/s10055-024-01018-8](https://doi.org/10.1007/s10055-024-01018-8) |
| `Pringle2026` | Progressive scaffolding of forensic science students crime scene investigation skills through authentic simulated crime scene assessments | 2026 (Recent) | Progressive forensic pedagogy & scaffolding | Multi-tier assessment rubrics, procedural competence index | Scaffolding architecture for 4-sprint PBL crime scene progression | [10.1016/j.scijus.2026.101428](https://doi.org/10.1016/j.scijus.2026.101428) |
| `Wickenheiser2023` | Proactive crime scene response optimizes crime investigation | 2023 (Recent) | Optimized forensic workflow & throughput | Chain-of-custody preservation rates, scene triage throughput | Digital custody hashing (ISO/IEC 27037) and evidence registry | [10.1016/j.fsisyn.2023.100325](https://doi.org/10.1016/j.fsisyn.2023.100325) |
| `Harrison2025` | Considerations of Space and Time: Fire Investigation and Forensic Archaeology in Crime Scene Reconstruction | 2025 (Recent) | Spatiotemporal crime reconstruction | 4D spatiotemporal event sequencing, archaeological stratigraphy | Kendall-Tau timeline concordance and chronological event ordering | [10.1002/wfs2.70006](https://doi.org/10.1002/wfs2.70006) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Buck (2019) - 3D Crime Scene Reconstruction
- **Core Contribution:** Formalized mathematical validation standards for spatial forensic measurements, proving that 3D photogrammetric reconstructions achieve sub-centimeter spatial accuracy when calibrated with reference scale bars.
- **Project Role:** Governs the spatial localization error thresholding ($\le 5\text{ cm}$) and provides the ballistic line-of-flight vector algorithms used in scene analysis in `CrimeSceneEvidenceManager.cs`.

### 3.2 Mayne & Green (2020) - VR in Forensic Crime Scene Investigation
- **Core Contribution:** Evaluated the pedagogical outcomes of virtual crime scene training, finding that students trained in VR identified subtle contextual cues and spatial linkages that were overlooked in 2D photographic logs.
- **Project Role:** Informs the experimental study design, rubric formulation, and comparative evaluation protocols deployed in `telemetry/generate_paper_figures.py` and benchmark CSV datasets.

### 3.3 Albeedan, Kolivand, & Hammady (2024) - Mixed Reality for Crime Scene Training
- **Core Contribution:** Developed a hybrid mixed reality system for forensic training, demonstrating that spatial contextualization dramatically reduces evidence tagging omission rates compared to paper logs.
- **Project Role:** Establishes the XR raycast interaction model and evidence billboard visualization in `CrimeSceneEvidenceManager.cs`.

### 3.4 Pringle et al. (2026) - Progressive Scaffolding of Forensic Skills
- **Core Contribution:** Demonstrated that authentic simulated crime scene assessments provide structured scaffolding that enhances investigative self-efficacy without incurring the recurring logistical friction of physical staging.
- **Project Role:** Directly shapes the 4-sprint PBL progression and oral viva defense rubric.

### 3.5 Wickenheiser (2023) - Proactive Crime Scene Response
- **Core Contribution:** Synthesized systemic improvements in crime scene management, showing that digital integrity logging and early spatial triage accelerate downstream courtroom admissibility.
- **Project Role:** Directly underpins the simulated SHA-256 digital chain-of-custody integrity verification implemented in `CrimeSceneEvidenceManager.cs`.

### 3.6 Harrison (2025) - Considerations of Space and Time in Scene Reconstruction
- **Core Contribution:** Formalized the integration of spatial 3D coordinates with temporal sequence stratigraphy, establishing mathematical criteria for chronological event ordering.
- **Project Role:** Primary mathematical foundation for Kendall-Tau rank correlation metrics implemented in `ForensicTimelineTelemetryLogger.cs`.

---

## 4. Synthesis & Research Gap Addressed
While prior studies established the feasibility of scanning crime scenes or using VR for exploratory walkthroughs, **none systematically quantified the combined impact of interactive 3D spatial evidence tagging and chronological timeline event sequencing on student investigators' diagnostic accuracy and NASA-TLX workload compared to standardized 2D photographic logs**. Group 07 addresses this critical gap directly.
