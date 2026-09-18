# Research and Implementation Guide: AR Visual-Marker Multi-Storey Indoor Navigation System

## Project: IVRAR Group 06
## Target Venue: IEEE Transactions on Visualization and Computer Graphics (TVCG) / IEEE ISMAR / ACM MobileHCI

---

## 1. Mathematical and Algorithmic Formulation

### 1.1 Perspective-n-Point (PnP) Optical Pose Estimation
Ground-truth pose estimation from planar visual markers (ArUco DICT_6X6_250 / QR) is modeled through perspective projection:

$$\mathbf{u}_i = \pi\left( \mathbf{K} \left( \mathbf{R} \mathbf{X}_i + \mathbf{t} \right) \right)$$

where:
- $\mathbf{X}_i \in \mathbb{R}^3$ are known marker corner coordinates in object space ($[-L/2, L/2] \times [-L/2, L/2]$ with edge size $L = 0.18$ m).
- $\mathbf{K}$ is the camera intrinsic calibration matrix:
  $$\mathbf{K} = \begin{bmatrix} f_x & 0 & c_x \\ 0 & f_y & c_y \\ 0 & 0 & 1 \end{bmatrix}$$
- $[\mathbf{R} | \mathbf{t}]$ is the extrinsic Euclidean transformation from marker coordinates to camera space.
The pose is solved via Levenberg-Marquardt non-linear optimization minimizing total reprojection error:

$$E_{\text{reproj}}(\mathbf{R}, \mathbf{t}) = \sum_{i=1}^4 \left\| \mathbf{u}_i - \pi\left( \mathbf{K} (\mathbf{R} \mathbf{X}_i + \mathbf{t}) \right) \right\|^2$$

Readings with $E_{\text{reproj}} > 2.0$ pixels are discarded to prevent outlier injection.

### 1.2 Monocular VIO Drift Suppression
Between sparse visual anchors, the mobile device dead-reckons using visual-inertial odometry (ARCore / ARKit). Uncorrected drift scales with path length $d$:

$$\mathbf{e}_{\text{drift}}(d) = \alpha \cdot d + \beta \cdot d^2$$

where $\alpha \approx 0.022$ m/m ($2.2\%$ linear drift). When the user encounters anchor $k$ at ground-truth position $\mathbf{T}_{\text{BIM}}^{\text{marker}}$, the global tracking transformation is updated:

$$\mathbf{T}_{\text{BIM}}^{\text{camera}} = \mathbf{T}_{\text{BIM}}^{\text{marker}} \cdot \left( \mathbf{T}_{\text{camera}}^{\text{marker}} \right)^{-1}$$

This resets cumulative error to $\epsilon_{\text{anchor}} < 0.05$ m, completely eliminating runaway dead-reckoning drift.

### 1.3 Multi-Level 3D Graph $A^*$ Pathfinding
The university academic building is represented as a weighted directed graph $G = (V, E)$. The cost function $f(n) = g(n) + h(n)$ balances traveled cost with vertical transition heuristics:

$$g(u, v) = \sqrt{(x_u - x_v)^2 + (y_u - y_v)^2} + |z_u - z_v| \cdot W_{\text{stair}}$$

$$h(n) = \sqrt{(x_n - x_{\text{goal}})^2 + (y_n - y_{\text{goal}})^2} + |z_n - z_{\text{goal}}| \cdot W_{\text{stair}}$$

where $W_{\text{stair}} = 15.0$ represents the energetic penalty for vertical floor climbing via stairwells.

### 1.4 Technoeconomic Cost Parity
The economic feasibility of passive optical fiducials versus active radio frequency (BLE / Wi-Fi) positioning is formulated via dimensionless cost parity $\kappa$:

$$\kappa = \frac{\text{OpEx}_{\text{AR}}}{\text{OpEx}_{\text{BLE}}} = \frac{C_{\text{marker\_cleaning}} + C_{\text{reprint}}}{C_{\text{battery\_replacement}} + C_{\text{rf\_site\_surveys}} + C_{\text{beacon\_theft}}}$$

The capital investment payback horizon in operating months is:

$$\text{Payback Months} = \frac{K_{\text{capex}}}{1 - \kappa} \times 12$$

---

## 2. Individual Student Work Boundaries & Responsibilities

```
===================================================================================================
Student Roll & Name        Assigned Technical Module                       Primary Deliverable
===================================================================================================
C136 - Mishika Shah        Spatial Vision & ArUco Pose Lead                Assets/Scripts/ArUcoAnchorPoseManager.cs
                                                                           (OpenCV Corner Detection, PnP Pose Core)
C172 - Parva Gaglani       XR Systems Architect & ARFoundation Rig         Assets/Scripts/ArUcoAnchorPoseManager.cs
                                                                           (ARSessionOrigin Alignment, BIM Transform)
C139 - Vansh Panchal       Graph Algorithms & Multi-Floor Pathfinder       Assets/Scripts/MultiFloorRoutePathfinder.cs
                                                                           (3D Topological Graph, Multi-Level A*)
C174 - Triesha Shah        Human Factors & Telemetry Engineer              Assets/Scripts/MultiFloorRoutePathfinder.cs
                                                                           (AR Guidance Arrows, Telemetry Logging)
===================================================================================================
```

### 2.1 C136 - Mishika Shah (Spatial Vision & AR Lead)
- Implement OpenCV ArUco DICT_6X6_250 detection pipeline with adaptive thresholding and subpixel corner refinement.
- Author Perspective-n-Point pose optimization and validate reprojection error filtering ($< 2.0$ px).
- Benchmark anchor detection latency across ambient lighting conditions ($50$ to $1000$ lux).
- **Git Branch:** `feat/c136-spatial-vision-ar-le`
- **Oral Viva Focus:** Camera intrinsic calibration matrix ($\mathbf{K}$), Levenberg-Marquardt optimization, Hamming distance error correction in ArUco dictionaries, and homography decomposition.

### 2.2 C172 - Parva Gaglani (XR Systems Architect)
- Configure Unity ARFoundation with ARCore / ARKit XR plugin subsystems.
- Implement ARSessionOrigin coordinate recalibration upon visual anchor lock to suppress VIO dead-reckoning drift.
- Manage architectural BIM coordinate alignments and anchor placement specs across 6 building floors.
- **Git Branch:** `feat/c172-xr-systems-architect`
- **Oral Viva Focus:** ARFoundation session management, monocular visual-inertial odometry drift mechanisms, coordinate frame transformation matrices, and frame rate stability (60 FPS).

### 2.3 C139 - Vansh Panchal (Graph Algorithms & Navigation Specialist)
- Author `MultiFloorRoutePathfinder.cs` constructing the 3D topological connectivity graph across 6 storeys (72 nodes).
- Implement multi-level $A^*$ pathfinding with weighted vertical penalties for stairwells and elevators.
- Handle dynamic detour recalculation when corridor segments are blocked.
- **Git Branch:** `feat/c139-graph-algorithms-nav`
- **Oral Viva Focus:** $A^*$ heuristic admissibility, 3D spatial graph data structures, Manhattan vs Euclidean distance heuristics, and computational complexity of multi-storey routing.

### 2.4 C174 - Triesha Shah (Human Factors & Usability Engineer)
- Design in-situ AR guidance visual cues (floating directional arrows, distance cards, destination placards).
- Author 90 Hz telemetry logging capturing trajectory length, wrong-turn counts, and transit time.
- Conduct empirical evaluations across $N = 50$ student journeys and analyze NASA-TLX workload and SUS usability scores.
- **Git Branch:** `feat/c174-human-factors-usabil`
- **Oral Viva Focus:** Human spatial wayfinding psychology, visual clutter in mobile AR interfaces, System Usability Scale (SUS) psychometric validation, and Student's t-test statistical hypotheses.

---

## 3. Verified Foundational Papers

The project architecture and empirical protocol are grounded in 6 verified literature foundations:

1. **Garrido-Jurado et al. (2014)**
   - *Title:* Automatic generation and detection of highly reliable fiducial markers under occlusion
   - *Journal:* Pattern Recognition, vol. 47, no. 6, pp. 2280-2292
   - *DOI:* [10.1016/j.patcog.2014.01.005](https://doi.org/10.1016/j.patcog.2014.01.005)
   - *Role:* Theoretical and algorithmic foundation of the ArUco fiducial system.

2. **Olson (2011)**
   - *Title:* AprilTag: A robust and flexible visual fiducial system
   - *Journal:* IEEE International Conference on Robotics and Automation (ICRA), pp. 3400-3407
   - *DOI:* [10.1109/ICRA.2011.5979561](https://doi.org/10.1109/ICRA.2011.5979561)
   - *Role:* Subpixel corner localization and 2D homography estimation principles.

3. **Qin, Li, & Shen (2018)**
   - *Title:* VINS-Mono: A Robust and Versatile Monocular Visual-Inertial State Estimator
   - *Journal:* IEEE Transactions on Robotics, vol. 34, no. 4, pp. 1004-1020
   - *DOI:* [10.1109/TRO.2018.2853729](https://doi.org/10.1109/TRO.2018.2853729)
   - *Role:* Visual-inertial odometry drift characterization and loop-closure mechanics.

4. **Mulloni, Seichter, & Schmalstieg (2011)**
   - *Title:* Handheld augmented reality indoor navigation with activity-based instructions
   - *Journal:* ACM MobileHCI 2011, pp. 211-220
   - *DOI:* [10.1145/2037373.2037406](https://doi.org/10.1145/2037373.2037406)
   - *Role:* Activity-based user interface guidelines for handheld indoor AR wayfinding.

5. **Kato & Billinghurst (1999)**
   - *Title:* Marker tracking and HMD calibration for a video-based augmented reality conferencing system
   - *Journal:* IEEE and ACM International Workshop on Augmented Reality (IWAR), pp. 85-94
   - *DOI:* [10.1109/IWAR.1999.803809](https://doi.org/10.1109/IWAR.1999.803809)
   - *Role:* Planar marker camera extrinsic transformation matrices and coordinate alignment.

6. **Isikdag, Zlatanova, & Underwood (2013)**
   - *Title:* A BIM-Oriented Model for supporting indoor navigation requirements
   - *Journal:* Computers, Environment and Urban Systems, vol. 41, pp. 112-123
   - *DOI:* [10.1016/j.compenvurbsys.2013.05.001](https://doi.org/10.1016/j.compenvurbsys.2013.05.001)
   - *Role:* Multi-storey topological graph extraction from building models.

---

## 4. Step-by-Step Implementation Roadmap

1. **Sprint 0: Toolchain & Baseline Verification**
   - Verify Unity 2022.3 LTS, ARFoundation, and OpenCV for Unity packages.
   - Run `python telemetry/ar_navigation_economics.py` to confirm technoeconomic parity parameters.
2. **Sprint 1: Marker Detection & Coordinate Frame Alignment**
   - Print and register physical ArUco markers (DICT_6X6_250) across building junction waypoints.
   - Implement `Assets/Scripts/ArUcoAnchorPoseManager.cs` with student `# TODO` implementations.
3. **Sprint 2: Multi-Floor 3D Graph Pathfinder**
   - Construct the 3D building topological graph representing corridor nodes and stairwell links.
   - Implement `Assets/Scripts/MultiFloorRoutePathfinder.cs` with multi-level $A^*$ pathfinding.
4. **Sprint 3: Empirical Benchmarking & Figure Generation**
   - Run `python telemetry/generate_paper_figures.py` to produce benchmark dataset ($N = 50$) and 300 DPI figures.
   - Validate that VIO drift reset maintains position error $< 0.05$ m.
5. **Sprint 4: Blueprint Manuscript Assembly & Final Audit**
   - Assemble experimental findings into `docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md`.
   - Execute the automated compliance audit script to ensure zero defects.
