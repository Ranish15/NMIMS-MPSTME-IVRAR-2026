# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 07 - Interactive VR Spatial Crime Scene Reconstruction
## Target Publication: Forensic Science International / Science & Justice / IEEE Transactions on Visualization and Computer Graphics (TVCG)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, ScienceDirect, IEEE Xplore, and PubMed to identify foundational works on 3D crime scene digitization, spatial forensic reconstruction, immersive virtual memory palaces, and pedagogical evaluation in forensic science. Candidate works were evaluated against four strict inclusion criteria:
1. Peer-reviewed indexing in premier forensic science, spatial computing, or virtual reality journals (Forensic Science International, Science & Justice, Virtual Reality).
2. Rigorous methodological formulation of 3D spatial scanning, photogrammetric surface reconstruction, and spatial recall kinematics.
3. Empirical validation of evidence tagging accuracy, spatial localization error, or timeline event sequencing.
4. Active CrossRef Digital Object Identifier (DOI) verification.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Wang2019` | Virtual reality and integrated crime scene scanning for immersive and heterogeneous crime scene reconstruction | Heterogeneous 3D crime scene scanning & VR integration | Multi-sensor point cloud registration: $\min_{\mathbf{R},\mathbf{t}} \sum \|\mathbf{p}_i - (\mathbf{R}\mathbf{q}_i + \mathbf{t})\|^2$ | Multi-source terrestrial LiDAR and photogrammetry scene compilation in Unity | [10.1016/j.forsciint.2019.109943](https://doi.org/10.1016/j.forsciint.2019.109943) |
| `Buck2019` | 3D crime scene reconstruction | Forensic photogrammetry and ballistics trajectory mapping | Epipolar geometry, 3D line-of-flight ballistic vector solving | Spatial evidence tagging and ballistic trajectory alignment in Unity | [10.1016/j.forsciint.2019.109901](https://doi.org/10.1016/j.forsciint.2019.109901) |
| `Krokos2019` | Virtual memory palaces: immersion aids recall | Spatial cognitive recall in immersive HMD environments | Superior spatial recall index: $R_{\text{HMD}} > R_{\text{Desktop}}$ ($p < 0.05$) | Cognitive spatial anchoring for evidence recall and timeline sequencing | [10.1007/s10055-018-0346-3](https://doi.org/10.1007/s10055-018-0346-3) |
| `Mayne2020` | Virtual reality for teaching and learning in crime scene investigation | Pedagogical efficacy of VR in forensic training | Formative assessment rubrics and search traversal coverage metrics | Benchmark evaluation of student investigator performance vs 2D baseline | [10.1016/j.scijus.2020.07.006](https://doi.org/10.1016/j.scijus.2020.07.006) |
| `Noond2002` | Visualising the scene: Computer graphics and evidence presentation | Digital evidence presentation and courtroom visualization | Chronological event sequencing and spatial view synthesis | Chain-of-custody logging and viewpoint-consistent evidence logging | [10.1016/S1355-0306(02)71804-2](https://doi.org/10.1016/S1355-0306(02)71804-2) |
| `Urbanova2015` | Testing photogrammetry-based techniques for three-dimensional surface documentation in forensic pathology | Sub-millimeter photogrammetric surface documentation | Structure-from-Motion (SfM) bundle adjustment residual minimization | High-fidelity digital twin geometry and texture mapping for small evidence | [10.1016/j.forsciint.2015.03.005](https://doi.org/10.1016/j.forsciint.2015.03.005) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Wang, Li, Hu, et al. (2019) - Heterogeneous Crime Scene Scanning
- **Core Contribution:** Developed an integrated data pipeline uniting terrestrial laser scanning (TLS) and close-range photogrammetry to generate high-fidelity, photorealistic 3D crime scenes capable of real-time rendering in immersive virtual reality headsets.
- **Project Role:** Establishes the technical standards for asset decimation, level-of-detail (LOD) management, and collider generation in `CrimeSceneEvidenceManager.cs`.

### 3.2 Buck (2019) - 3D Crime Scene Reconstruction
- **Core Contribution:** Formalized mathematical validation standards for spatial forensic measurements, proving that 3D photogrammetric reconstructions achieve sub-centimeter spatial accuracy when calibrated with reference scale bars.
- **Project Role:** Governs the spatial localization error thresholding ($\le 5\text{ cm}$) and provides the ballistic line-of-flight vector algorithms used in scene analysis.

### 3.3 Krokos, Plaisant, & Varshney (2019) - Virtual Memory Palaces
- **Core Contribution:** Proved empirically through controlled cognitive trials that full immersion in an HMD yields statistically significant improvements in recall accuracy and spatial memory retention (8.8% recall advantage) over traditional desktop interfaces by engaging vestibular and proprioceptive spatial cues.
- **Project Role:** Serves as the theoretical cognitive foundation for the project hypothesis: investigating whether spatial immersion enhances forensic evidence recall and event sequence reconstruction.

### 3.4 Mayne & Green (2020) - VR in Forensic Crime Scene Investigation
- **Core Contribution:** Evaluated the pedagogical outcomes of virtual crime scene training, finding that students trained in VR identified subtle contextual cues and spatial linkages that were overlooked in 2D photographic logs.
- **Project Role:** Informs the experimental study design, rubric formulation, and comparative evaluation protocols deployed in `telemetry/generate_paper_figures.py` and benchmark CSV datasets.

### 3.5 Noond, Schofield, March, & Evison (2002) - Computer Graphics in Evidence Presentation
- **Core Contribution:** Introduced standardized criteria for digital evidence visualization to prevent prejudicial bias or spatial distortion in courtroom presentations, emphasizing immutable timeline event sequencing.
- **Project Role:** Informs the chronological event sequencing algorithms and Kendall-Tau correlation metrics implemented in `Assets/Scripts/ForensicTimelineTelemetryLogger.cs`.

### 3.6 Urbanová, Hejna, & Jurda (2015) - Photogrammetric Documentation
- **Core Contribution:** Conducted rigorous quantitative testing of optical photogrammetry across complex organic and non-organic surfaces, demonstrating that consumer-grade digital cameras with fixed focal lengths can achieve mean surface errors below 1 mm.
- **Project Role:** Guides the photogrammetric capture pipeline used by the spatial forensics lead for small-scale evidence assets (spent shell casings, biological spatters, weapons).

---

## 4. Theoretical Synthesis & Research Gaps Identified
While previous studies established the technical feasibility of scanning crime scenes or using VR for high-level exploratory walkthroughs, **none systematically quantified the combined impact of interactive 3D spatial evidence tagging and chronological timeline event sequencing on student investigators' cognitive load (NASA-TLX) and diagnostic accuracy compared to standardized 2D photographic logs**. Group 07 addresses this critical pedagogical and forensic gap directly.
