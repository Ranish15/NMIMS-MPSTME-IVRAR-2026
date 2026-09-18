# Research and Implementation Guide: Unity VR Home Cinema Acoustics

## Project: IVRAR Group 01
## Target Venue: IEEE VR / ACM VRST / Audio Engineering Society (AES)

---

## 1. Mathematical and Algorithmic Formulation

### 1.1 Statistical Room Acoustics: Sabine and Eyring Formulations
In an enclosure of volume $V$ ($m^3$) and total surface area $S$ ($m^2$), the classical Sabine formula calculates reverberation time $\text{RT}_{60}$ (time required for sound pressure level to decay by 60 dB):

$$\text{RT}_{60,\text{Sabine}} = \frac{0.161 \cdot V}{\sum_{i=1}^{M} S_i \alpha_i} = \frac{0.161 \cdot V}{A_{\text{total}}}$$

where $S_i$ is the area of surface $i$, $\alpha_i$ is its absorption coefficient at frequency $f$, and $A_{\text{total}}$ is the total absorption in metric Sabins.

For dead or heavily treated rooms (such as CEDIA-certified home cinemas where average absorption $\bar{\alpha} > 0.25$), the Eyring-Norris equation provides superior accuracy by accounting for energy loss per reflection:

$$\text{RT}_{60,\text{Eyring}} = \frac{0.161 \cdot V}{-S_{\text{total}} \ln(1 - \bar{\alpha}) + 4 m V}$$

where $\bar{\alpha} = \frac{1}{S_{\text{total}}} \sum_{i} S_i \alpha_i$ and $m$ is the atmospheric air attenuation coefficient.

### 1.2 Geometrical Acoustic Raycasting & Energy Decay
From each of the 7.1.4 loudspeaker sources, $N_{\text{rays}}$ (typically 500 to 2000) are emitted along randomized spherical vectors $\mathbf{v}_k \in S^2$. When a ray strikes surface $i$ with absorption $\alpha_i(f)$, the reflected ray direction $\mathbf{r}$ is computed via Snell's law:

$$\mathbf{r} = \mathbf{d} - 2 (\mathbf{d} \cdot \mathbf{n}) \mathbf{n}$$

where $\mathbf{d}$ is the incident ray unit vector and $\mathbf{n}$ is the surface normal. Ray energy $E_k$ decays according to:

$$E_k(n+1) = E_k(n) \cdot (1 - \alpha_i(f)) \cdot e^{-m \cdot d_n}$$

When a ray intersects a spherical listener detection volume around the primary listening position (radius $r_{\text{receiver}} = 0.50$ m), its arrival time $t_a = \sum d_n / c_{\text{sound}}$ and residual energy are accumulated into a discrete energy histogram $h(t)$.

### 1.3 Schroeder Backward Integration
Reverberation decay curves are extracted from the impulse response $h(t)$ using Schroeder backward integration:

$$E_{\text{decay}}(t) = \int_{t}^{\infty} h^2(\tau) d\tau \approx \sum_{j = t/\Delta t}^{N_{\text{bins}}} h^2(j \Delta t) \Delta t$$

The decay curve in decibels is:
$$L(t) = 10 \log_{10} \left( \frac{E_{\text{decay}}(t)}{E_{\text{decay}}(0)} \right)$$

Linear regression between $-5$ dB and $-25$ dB yields $T_{20}$, and between $-5$ dB and $-35$ dB yields $T_{30}$. The reverberation time is then extrapolated:
$$\text{RT}_{60} = 3 \cdot T_{20} = 2 \cdot T_{30}$$

### 1.4 CEDIA/CTA-RP22 Recommended Reverberation Target
According to CEDIA/CTA-RP22 (Level 1 to Level 4 Home Theater Performance), recommended mid-frequency $\text{RT}_{60}$ ($500$ Hz to $2000$ Hz) scales with room volume $V$:

$$\text{RT}_{60,\text{target}} = 0.30 \cdot \left( \frac{V}{100} \right)^{0.33} \pm 0.05 \text{ seconds}$$

For a typical $102.8 \text{ m}^3$ room ($7.2 \times 5.1 \times 2.8$ m), the target range is $0.25$ s to $0.35$ s.

### 1.5 SMPTE/THX Sightline Clearance Criteria
Primary viewer seated at distance $D_{\text{screen}}$ from screen width $W_{\text{screen}}$ must satisfy:
- Horizontal field of view: $\theta_H = 2 \arctan\left(\frac{W_{\text{screen}}}{2 D_{\text{screen}}}\right) \in [36^{\circ}, 40^{\circ}]$
- Maximum vertical gaze angle to top of display: $\phi_V \le 15^{\circ}$

### 1.6 Technoeconomic Operational Parity
AV integrator business feasibility is formulated as dimensionless cost parity $\kappa$:

$$\kappa = \frac{\text{OpEx}_{\text{vr\_simulation}}}{\text{OpEx}_{\text{physical\_rework}}} = \frac{C_{\text{software\_license}} + C_{\text{modeling\_time}}}{C_{\text{panel\_reinstallation}} + C_{\text{site\_revisit\_labor}}}$$

The capital investment payback horizon in operating months is:
$$\text{Payback Months} = \frac{K_{\text{capex}}}{1 - \kappa} \times 12$$

---

## 2. Individual Student Work Boundaries & Responsibilities

```
===================================================================================================
Student Roll & Name        Assigned Technical Module                       Primary Deliverable
===================================================================================================
I066 - Meahaan Sharma      XR Architecture, 7.1.4 Rig & Sightline Rays     Assets/Scripts/AcousticRaycaster.cs
                                                                           (OpenXR Scene & Sightlines)
C034 - Amogh Gupta         Acoustic Raycasting & Schroeder RT60 Engine     Assets/Scripts/RT60TelemetryLogger.cs
                                                                           (Ray Bounces & Energy Decay)
N083 - Harshit Rai         Human Factors, MUSHRA Protocol & Usability      telemetry/test_evaluation_tools.py
                                                                           (ISO 3382-2 & Cybersickness)
N087 - Rannbir Sachdeva    CEDIA Compliance, AV Rework & Business Economics telemetry/av_integration_economics.py
                                                                           (RP22 Parity & Payback Model)
===================================================================================================
```

### 2.1 I066 - Meahaan Sharma (XR Systems Architect)
- Construct the 3D home cinema scene in Unity OpenXR ($7.2 \times 5.1 \times 2.8$ m).
- Implement 7.1.4 virtual loudspeaker placement and SMPTE/THX sightline raycast evaluation.
- **Git Branch:** `feat/i066-xr-systems-architect`

### 2.2 C034 - Amogh Gupta (Spatial Acoustics Specialist)
- Implement multi-bounce Monte Carlo acoustic raycasting in C#.
- Implement surface material absorption coefficients across octave bands (125 Hz to 4 kHz).
- Implement Schroeder backward integration to calculate $T_{20}, T_{30}$, and $\text{RT}_{60}$.
- **Git Branch:** `feat/c034-spatial-acoustics-au`

### 2.3 N083 - Harshit Rai (Human Factors & Usability)
- Formulate MUSHRA listening test protocol comparing untreated vs CEDIA-treated conditions.
- Implement Kennedy SSQ and NASA-TLX evaluation scripts for user evaluation trials.
- **Git Branch:** `feat/n083-human-factors-usabil`

### 2.4 N087 - Rannbir Sachdeva (Technoeconomic Product Manager)
- Implement CEDIA/CTA-RP22 compliance verification scorecards.
- Formulate the on-site physical acoustic rework reduction model.
- Execute statistical hypothesis tests and compute dimensionless capital payback horizons.
- **Git Branch:** `feat/n087-technoeconomic-produ`

---

## 3. Step-by-Step Implementation Roadmap

1. **Sprint 0: Setup & Toolchain Verification**
   - Run `python telemetry/test_evaluation_tools.py` to confirm Python evaluation scripts.
   - Verify OpenXR package and Unity 2022.3 LTS toolchain.
2. **Sprint 1: Cinema Architecture & Sightlines**
   - Build cinema geometry and verify viewer horizontal FOV ($36^{\circ}-40^{\circ}$) and vertical elevation ($< 15^{\circ}$).
3. **Sprint 2: Acoustic Raycasting & Decay Extraction**
   - Run C# raycaster across varying ray budgets ($N = 250, 500, 1000$ rays/source).
   - Verify that Schroeder backward integration calculates RT60 within CEDIA bounds ($0.25-0.35$ s).
4. **Sprint 3: Benchmarking and Economics Simulation**
   - Run `python telemetry/generate_paper_figures.py` to produce benchmark CSV and 300 DPI figures.
   - Run `python telemetry/av_integration_economics.py` to evaluate contractor ROI.
5. **Sprint 4: Paper Preparation & Git Push**
   - Draft manuscript sections using `docs/RESEARCH_PAPER_MANUSCRIPT_BLUEPRINT.md`.
   - Run compliance audit script to guarantee zero emojis, zero currency, and strict compliance.
