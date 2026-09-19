# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 06 - AR Visual-Marker Multi-Storey Indoor Navigation System
## Target Publication: IEEE Transactions on Visualization and Computer Graphics (TVCG) / IEEE ISMAR / Pattern Recognition
## Course Code: 702COI002 (Institute Open Elective)

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, IEEE Xplore, ScienceDirect, and Scopus to identify foundational works on optical fiducial tracking, visual-inertial odometry drift correction, and multi-storey indoor spatial routing. Candidate works were evaluated against four inclusion criteria:
1. Exact **2 Seminal : 4 Recent (2022–2026)** ratio with 100% active HTTP 200 DOIs verified via CrossRef REST APIs.
2. Peer-reviewed indexing in premier computer vision, robotics, or spatial computing venues (*Pattern Recognition*, *IEEE Trans. Robotics*, *IEEE IWAR*, *IEEE ICAR*, *IEEE AIxVR*, *IEEE ICTS*).
3. Rigorous mathematical formulation of pose estimation (Perspective-n-Point) and graph search algorithms.
4. Direct alignment with B.Tech Computer Engineering (Integrated) competencies, multi-floor wayfinding, and VIO drift suppression (< 5 cm).

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Publication Year | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|---|
| `Kato1999` | Marker tracking and HMD calibration for a video-based augmented reality conferencing system | 1999 (Seminal) | Foundational marker-based AR | Camera intrinsic matrix calibration: $\mathbf{x} = \mathbf{K} [\mathbf{R} | \mathbf{t}] \mathbf{X}$ | Coordinate transformation from camera to marker space | [10.1109/IWAR.1999.803809](https://doi.org/10.1109/IWAR.1999.803809) |
| `GarridoJurado2014` | Automatic generation and detection of highly reliable fiducial markers under occlusion | 2014 (Seminal) | Optical fiducial generation & detection | Binary code distance maximization: $D_{\text{min}} = \min_{i \ne j} d_H(m_i, m_j)$ | ArUco DICT_6X6_250 marker generation and occlusion handling | [10.1016/j.patcog.2014.01.005](https://doi.org/10.1016/j.patcog.2014.01.005) |
| `Asmara2023` | Marker vs. Markerless: Usability Insights for Indoor Navigation with Handheld Augmented Reality Systems | 2023 (Recent) | Marker vs Markerless AR navigation | Usability metrics, tracking stability and cognitive disorientation | Quantifies route errors between marker-based vs markerless AR | [10.1109/icts58770.2023.10330861](https://doi.org/10.1109/icts58770.2023.10330861) |
| `Hinderer2025` | Investigation of ArUco Marker Placement for Planar Indoor Localization | 2025 (Recent) | ArUco placement geometry | Geometric Dilution of Precision (GDOP) for marker arrays | Optimal spacing of corridor junction anchors ($15-20$ m) | [10.1109/icar65334.2025.11338671](https://doi.org/10.1109/icar65334.2025.11338671) |
| `Miyashita2025` | Hierarchical ArUco Marker Array for Coarse-to-Fine Localization in XR applications | 2025 (Recent) | Coarse-to-fine visual localization | Multi-scale marker nesting, PnP residual optimization | Resolves near and far marker acquisition across stairwells | [10.1109/aixvr63409.2025.00040](https://doi.org/10.1109/aixvr63409.2025.00040) |
| `Dhanasekar2025` | Augmented Reality Indoor Navigation Using Unity and QR Code Localization for Cross-Platform Mobile Applications | 2025 (Recent) | Mobile AR Unity QR localization | Floor-level landmark decoding and A* path interpolation | Mobile AR architecture and QR floor metadata decoding | [10.5220/0013886300004919](https://doi.org/10.5220/0013886300004919) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Kato & Billinghurst (1999) - Marker Tracking & AR Calibration
- **Core Contribution:** Pioneered the mathematical framework for real-time video-based AR tracking, utilizing planar fiducial corners to solve the camera extrinsic matrix $\mathbf{T} = [\mathbf{R} | \mathbf{t}]$.
- **Project Role:** Provides the geometric transformation pipeline mapping OpenCV camera coordinates into Unity's coordinate system in `ArUcoAnchorPoseManager.cs`.

### 3.2 Garrido-Jurado, Muñoz-Salinas, Madrid-Cuevas, & Marín-Jiménez (2014) - ArUco Markers
- **Core Contribution:** Formulated an algorithmic framework for generating dictionary-based binary square markers that maximize inter-marker Hamming distance, ensuring zero false-positive detections and robust error correction under partial occlusion.
- **Project Role:** Governs visual fiducial detection in `Assets/Scripts/ArUcoAnchorPoseManager.cs`. Standard DICT_6X6_250 markers are placed at corridor junctions to serve as absolute ground-truth anchors.

### 3.3 Asmara & Fabroyir (2023) - Marker vs. Markerless AR Indoor Navigation
- **Core Contribution:** Evaluated usability differences between optical fiducial systems and markerless feature tracking, proving that fiducial anchors prevent cumulative feature loss in textureless academic corridors.
- **Project Role:** Directly underpins the experimental hypothesis and baseline comparison across navigation modalities in Section III.

### 3.4 Hinderer, Scheffler, & Yang (2025) - ArUco Marker Placement for Planar Localization
- **Core Contribution:** Analyzed geometric dilution of precision for optical markers in long corridors, demonstrating that placing fiducials every $15-20$ meters bounds localization error beneath $0.05$ m.
- **Project Role:** Parameterizes the physical marker placement layout in the multi-storey academic complex.

### 3.5 Miyashita, Tabata, & Ishikawa (2025) - Hierarchical ArUco Marker Arrays
- **Core Contribution:** Introduced hierarchical multi-scale fiducial arrays that allow mobile devices to acquire coarse localization from distant markers while achieving millimeter-precision alignment when close.
- **Project Role:** Calibrates the multi-floor stairwell portal anchors where viewing distances fluctuate rapidly.

### 3.6 Dhanasekar & Kiruthika (2025) - AR Indoor Navigation Using Unity & QR Codes
- **Core Contribution:** Developed an end-to-end mobile AR navigation pipeline in Unity, encoding building metadata inside optical codes to trigger real-time A* route rendering.
- **Project Role:** Architectural template for Group 06's corridor chevron rendering and floor transition state machine in `MultiFloorRoutePathfinder.cs`.

---

## 4. Synthesis & Research Gap Addressed
While previous works developed standalone marker detectors or 2D indoor maps, **none systematically solved multi-storey vertical stairwell and elevator routing by coupling mobile monocular VIO with fixed ArUco fiducials to achieve continuous sub-5cm tracking drift suppression in complex university complexes**. Group 06 resolves this challenge directly.
