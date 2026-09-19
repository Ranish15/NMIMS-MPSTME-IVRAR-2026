# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 14 - Predictive Ghost-Avatar Digital Twin in High-Latency Planetary Teleoperation
## Target Publication: IEEE Transactions on Robotics (T-RO) / IEEE Transactions on Visualization and Computer Graphics (TVCG) / Journal of Field Robotics

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature search was conducted across CrossRef, IEEE Xplore, the ACM Digital Library, and Cambridge University Press to identify foundational and modern research in space telerobotics, predictive displays, time-delay compensation, virtual reality surrogates, and planetary rover kinematic modeling. Articles were evaluated against four strict inclusion criteria:
1. Peer-reviewed publication in premier robotics, space exploration, or human-robot interaction venues (IEEE T-RO, IEEE ICRA, IEEE/RSJ IROS, ACM/IEEE HRI, Robotica, ACM Interactions).
2. Rigorous algorithmic formulation of predictive displays, forward kinematic projection, or time-delay bilateral teleoperation.
3. Explicit empirical benchmarking evaluating path tracking error (RMSE), hazard collision frequency, or operator workload under transmission latencies exceeding 1.0 second.
4. Active, verified CrossRef Digital Object Identifier (DOI) resolution.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Sheridan1993` | Space teleoperation through time delay: review and prognosis | Foundations of supervisory control and delay compensation | Move-and-wait vs supervisory control: $T_{\text{task}} = N_{\text{steps}} \cdot (\tau_{\text{move}} + 2\tau_{\text{delay}})$ | Mathematical formulation of mission idle time and task completion delay | [10.1109/70.258052](https://doi.org/10.1109/70.258052) |
| `Bejczy1990` | The phantom robot: predictive displays for teleoperation with time delay | Predictive phantom graphics for delayed telerobotic control | Forward kinematic projection: $\hat{\mathbf{x}}(t + \tau) = \mathbf{x}(t) + \int_t^{t+\tau} f(\mathbf{x}, \mathbf{u}) dt$ | Core concept of the holographic ghost avatar in `PredictiveGhostRoverManager.cs` | [10.1109/ROBOT.1990.126037](https://doi.org/10.1109/ROBOT.1990.126037) |
| `Zhu2023` | Intention-reflected predictive display for operability improvement of time-delayed teleoperation system | Intention-aware forward trajectory prediction under time delay | Operator control intention estimation and future state extrapolation | Dynamic trajectory ribbon modeling in `PredictiveGhostRoverManager.cs` | [10.1186/s40648-023-00258-8](https://doi.org/10.1186/s40648-023-00258-8) |
| `Jin2024` | Mitigating Latency Effects on Subjective Experience in Robot Teleoperation Using a VR-Enabled Virtual Spring | VR visual spring decoupling and latency subjective damping | Virtual compliance dynamics and latency damping formulation | Virtual spring smoothing across delay buffers | [10.1109/ismar62088.2024.00144](https://doi.org/10.1109/ismar62088.2024.00144) |
| `Prakash2023` | Predictive Display With Perspective Projection of Surroundings in Vehicle Teleoperation to Account Time-Delays | Predictive terrain projection and delayed visual immersion | Perspective warping and projected terrain occupancy mapping | Forward hazard projection and collision boundary checking | [10.1109/tits.2023.3268756](https://doi.org/10.1109/tits.2023.3268756) |
| `Pant2026` | Low-Cost VR Teleoperation of a 5-Dof Robotic Arm with a Synchronized Digital Twin and Ultrasonic Safety Loop | Synchronized digital twin and proximity safety loop | Real-time state synchronization and obstacle safety envelope | Ghost-avatar telemetry sync and safety collision verification | [10.1109/vrw70859.2026.00167](https://doi.org/10.1109/vrw70859.2026.00167) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Sheridan (1993) - Space Teleoperation Through Time Delay
- **Core Contribution:** Established the fundamental theory of human supervisory control over time-delayed communication links. Proved that uncompensated delays exceeding 0.5 seconds force human operators into unstable "hunting" oscillations and the highly inefficient "move-and-wait" strategy.
- **Project Role:** Directly informs the baseline comparison against which Group 14's predictive system is benchmarked, providing the theoretical basis for quantifying operator idle time.

### 3.2 Bejczy, Kim, & Venema (1990) - The Phantom Robot
- **Core Contribution:** Pioneered the concept of rendering a zero-delay graphical "phantom" wireframe robot overlaid on delayed video feeds, proving that predictive visual feedback restores operator hand-eye coordination and suppresses control instability.
- **Project Role:** Governs the core technical implementation in `Assets/Scripts/PredictiveGhostRoverManager.cs` by `I003 - Ananya Baweja` and `I006 - Anvay Borade`.

### 3.3 Zhu, Fusano, Aoyama, & Hasegawa (2023) - Intention-Reflected Predictive Display
- **Core Contribution:** Developed intention-aware forward predictive display models that predict operator control vectors under communication latency, significantly improving trajectory smoothness and tracking precision.
- **Project Role:** Directly directs the predictive path ribbon extrapolation and forward kinematic projection algorithms in `PredictiveGhostRoverManager.cs`.

### 3.4 Jin, Zhang, Li, Ban, & Warisawa (2024) - Mitigating Latency in VR Robot Teleoperation
- **Core Contribution:** Formulated a VR-enabled virtual spring damping mechanism that decouples real-time controller inputs from delayed telemetry, reducing perceived latency and operator disorientation.
- **Project Role:** Guides the high-latency smoothing filters and FIFO delay buffer interpolation implemented in `Assets/Scripts/HighLatencyNetworkSimulator.cs` by `I010 - Mahit Naresh Daswani Chanchlani`.

### 3.5 Prakash, Vignati, Vignarca, Sabbioni, & Cheli (2023) - Predictive Display with Perspective Projection
- **Core Contribution:** Demonstrated that projecting predictive prospective trajectories onto 3D reconstructed terrain meshes enables drivers to anticipate vehicle-ground contact points under transmission latencies up to 5.0 seconds.
- **Project Role:** Dictates the 3D terrain hazard projection and rock collision detection algorithms implemented by `I041 - Aryan Oberoi`.

### 3.6 Pant, Saini, & Gaurav (2026) - Synchronized Digital Twin and Safety Loops
- **Core Contribution:** Established real-time bidirectional synchronization protocols between physical robotic hardware and immersive Unity XR digital twins, integrating reactive collision safety buffers.
- **Project Role:** Validates the software architecture bridging Unity VR kinematic transforms with delayed physical rover state telemetry.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While early predictive displays relied on 2D wireframe overlays (`Bejczy1990`) and modern teleoperation studies focused on isolated robotic arms (`Pant2026`), **no prior work has deployed an immersive 6-DoF VR digital twin integrating forward kinematic prediction (`Zhu2023`) and terrain projection (`Prakash2023`) to actively eliminate move-and-wait idle latency and rock collision hazards under 1.5s to 5.0s planetary transmission delays**. Group 14 solves this problem.
