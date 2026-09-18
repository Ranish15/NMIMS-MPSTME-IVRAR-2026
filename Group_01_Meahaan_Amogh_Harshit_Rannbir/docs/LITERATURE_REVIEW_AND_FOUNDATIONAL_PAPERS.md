# Exhaustive Literature Review & Foundational Benchmark Papers

## Project Group: IVRAR Group 01
## Domain: Geometrical Acoustic Raycasting, VR Spatial Audio & CEDIA Standards

---

## 1. Verified Foundational Literature Portfolio

The following five peer-reviewed benchmark publications form the theoretical and empirical baseline for this research project. Every citation includes an active, validated Digital Object Identifier (DOI).

```
========================================================================================================================
#  Authors (Year)               Title / Venue                                            DOI
========================================================================================================================
1  Savioja & Svensson (2015)    Overview of Geometrical Room Acoustic Modeling           10.1121/1.4926438
                                Techniques (J. Acoust. Soc. Am.)
2  Chandak et al. (2008)        AD-Frustum: Adaptive Frustum Tracing for Interactive    10.1109/TVCG.2008.111
                                Sound Propagation (IEEE TVCG)
3  Schroeder (1965)             New Method of Measuring Reverberation Time               10.1121/1.1909343
                                (J. Acoust. Soc. Am.)
4  Vorländer (2008)             Auralization: Fundamentals of Acoustics, Modelling,      10.1007/978-3-540-48830-9
                                Simulation, Algorithms and Acoustic Virtual Reality
5  Bradley (2011)               Review of Objective Room Acoustics Measures and          10.1016/j.apacoust.2011.04.004
                                Future Needs (Applied Acoustics)
========================================================================================================================
```

---

## 2. In-Depth Methodological Analysis of Each Paper

### Paper 1: Overview of Geometrical Room Acoustic Modeling Techniques
- **Authors:** Lauri Savioja, U. Peter Svensson
- **Venue:** *The Journal of the Acoustical Society of America*, Vol. 138, Iss. 2, pp. 708-730, 2015.
- **DOI:** [10.1121/1.4926438](https://doi.org/10.1121/1.4926438)
- **Key Contribution:** Comprehensive survey of computerized room acoustics based on geometrical acoustics (GA). Details ray tracing, beam tracing, image-source methods, and hybrid models. Analyzes frequency validity limits, surface scattering, and boundary absorption discretization.
- **Direct Relevance to Group 01:** Serves as the primary theoretical foundation for Group 01's raycasting algorithm in Unity, establishing valid assumptions for high-frequency specular reflections and diffuse energy distribution.

### Paper 2: AD-Frustum: Adaptive Frustum Tracing for Interactive Sound Propagation
- **Authors:** Anish Chandak, Christian Lauterbach, Micah Taylor, Zhimin Ren, Dinesh Manocha
- **Venue:** *IEEE Transactions on Visualization and Computer Graphics*, Vol. 14, No. 6, pp. 1707-1722, 2008.
- **DOI:** [10.1109/TVCG.2008.111](https://doi.org/10.1109/TVCG.2008.111)
- **Key Contribution:** Introduces adaptive volumetric frustum tracing to accurately model specular reflection and edge diffraction in real-time complex 3D scenes. Achieves interactive frame rates suitable for virtual reality walkthroughs.
- **Direct Relevance to Group 01:** Guides the computational efficiency trade-off between ray density (rays per source) and render frame rate ($\ge 75$ FPS) on standalone and PC-tethered VR headsets.

### Paper 3: New Method of Measuring Reverberation Time
- **Author:** Manfred R. Schroeder
- **Venue:** *The Journal of the Acoustical Society of America*, Vol. 37, Iss. 3, pp. 409-412, 1965.
- **DOI:** [10.1121/1.1909343](https://doi.org/10.1121/1.1909343)
- **Key Contribution:** Seminal formulation of the integrated impulse response method (Schroeder backward integration). Demonstrates that integrating the squared room impulse response backwards in time yields a smooth, deterministic decay curve identical to the ensemble average of band-filtered noise decays.
- **Direct Relevance to Group 01:** Directly supplies the mathematical algorithm implemented in `RT60TelemetryLogger.cs` to extract $T_{20}, T_{30}$, and $\text{RT}_{60}$ from virtual acoustic impulse responses.

### Paper 4: Auralization: Fundamentals of Acoustics, Modelling, Simulation, Algorithms and Acoustic Virtual Reality
- **Author:** Michael Vorländer
- **Venue:** *Springer-Verlag Berlin Heidelberg*, 2008.
- **DOI:** [10.1007/978-3-540-48830-9](https://doi.org/10.1007/978-3-540-48830-9)
- **Key Contribution:** Authoritative textbook on acoustic virtual reality and auralization. Connects binaural room impulse responses (BRIR) with head-related transfer functions (HRTFs), listener head-tracking, and psychoacoustic subjective evaluation criteria.
- **Direct Relevance to Group 01:** Provides the spatial audio pipeline architecture connecting virtual loudspeaker positions to binaural listening seats in the simulated home cinema.

### Paper 5: Review of Objective Room Acoustics Measures and Future Needs
- **Author:** J. S. Bradley
- **Venue:** *Applied Acoustics*, Vol. 72, Iss. 10, pp. 713-720, 2011.
- **DOI:** [10.1016/j.apacoust.2011.04.004](https://doi.org/10.1016/j.apacoust.2011.04.004)
- **Key Contribution:** Critical review of room acoustics parameters codified in ISO 3382-1 and ISO 3382-2. Evaluates the practical limits, Just Noticeable Differences (JNDs), and tolerance bands for reverberation time in speech and music reproduction spaces.
- **Direct Relevance to Group 01:** Establishes the target tolerance intervals ($0.2$ s to $0.5$ s across mid-frequencies) prescribed by CEDIA/CTA-RP22 and validates the JND criteria for human listener discrimination.

---

## 3. Comparative Literature Synthesis

| Study | Platform Type | Acoustic Modeling Technique | Target Acoustic Metric | Validation Protocol | Core Limitation | Active DOI |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Savioja & Svensson (2015)**| Desktop CAD | Geometrical Acoustics Survey | Broad Acoustic Indicators | Comparative Literature | Comprehensive review, no interactive VR | `10.1121/1.4926438` |
| **Chandak et al. (2008)** | Interactive 3D | Adaptive Frustum Tracing | Multi-bounce Specular / Diffuse | Real-time Frame Rates | High GPU memory overhead | `10.1109/TVCG.2008.111` |
| **Schroeder (1965)** | Analytical Math | Backward Impulse Integration | Reverberation Time (RT60) | Physical Test Enclosures | 1D decay formulation | `10.1121/1.1909343` |
| **Vorländer (2008)** | VR Auralization | GA + HRTF Binaural Filtering | Spatial Immersion & Clarity | Subjective Listening Tests | Offline computation focus | `10.1007/978-3-540-48830-9` |
| **Bradley (2011)** | Standardized Lab | ISO 3382 Objective Metrics | EDT, T20, T30, Clarity (C80) | Physical Room Testing | Focus on large halls, not home cinema | `10.1016/j.apacoust.2011.04.004` |
| **Group 01 Proposed** | Unity OpenXR VR | Real-Time Acoustic Raycasting | Octave-Band RT60 & Sightline | CEDIA/CTA-RP22 Compliance | Simulated ray resolution bounds | **Our Contribution** |

---

## 4. Research Gap and Proposed Innovation

Existing acoustic engineering tools (EASE, Odeon) are non-immersive desktop software that isolate acoustics from architectural aesthetics and screen sightlines. Group 01 bridges this gap by providing:
1. Real-time Monte Carlo acoustic raycasting directly inside Unity VR, calculating octave-band RT60 across 125 Hz to 4 kHz.
2. Concurrent geometric sightline verification conforming to SMPTE/THX standards ($36^{\circ}-40^{\circ}$ horizontal FOV, $< 15^{\circ}$ vertical elevation).
3. A CSBS technoeconomic model quantifying how VR pre-visualization eliminates physical AV contractor rework and guarantees first-time CEDIA/CTA-RP22 compliance.
