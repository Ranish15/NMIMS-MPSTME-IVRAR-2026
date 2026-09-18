# Literature Review and Foundational Papers Dossier
## Project: IVRAR Group 11 - Hybrid Smart AR Kiosk and Mobile WebXR Handoff System
## Target Publication: IEEE Transactions on Visualization and Computer Graphics (TVCG) / ACM MobileHCI / Computers, Environment and Urban Systems

---

## 1. Literature Search Methodology & Boundary Conditions
A systematic literature analysis was conducted across CrossRef, IEEE Xplore, ScienceDirect, and the ACM Digital Library to identify foundational works on public display networks, cross-device mobile handoff, Web-based augmented reality (WebXR), and indoor wayfinding algorithms. Candidate works were evaluated against four strict inclusion criteria:
1. Peer-reviewed indexing in premier pervasive computing, spatial computing, or architectural informatics venues (Proceedings of the IEEE, Computer, IEEE Pervasive Computing, ACM MobileHCI, CEUS, ACM DARE).
2. Rigorous algorithmic formulation of 3D topological indoor pathfinding, cross-device session migration, or optical tag encoding.
3. Empirical benchmarking of visitor transit time, wayfinding error rates, or public display handoff latency.
4. Active CrossRef Digital Object Identifier (DOI) verification.

---

## 2. Synthesis Matrix of 6 Foundational Papers

| Citation Key | Canonical Title | Primary Focus & Domain | Mathematical / Algorithmic Core | Direct Integration in Project | Verified DOI |
|---|---|---|---|---|---|
| `Qiao2019` | Web AR: A Promising Future for Mobile Augmented Reality-State of the Art, Challenges, and Insights | Zero-install browser-based mobile AR architectures | Client-edge-cloud pipeline latency: $T_{\text{total}} = T_{\text{trans}} + T_{\text{infer}} + T_{\text{render}}$ | Zero-install WebXR client architecture in `MobileWebXRRouteNavigator.cs` | [10.1109/JPROC.2019.2895105](https://doi.org/10.1109/JPROC.2019.2895105) |
| `Mulloni2011` | Handheld augmented reality indoor navigation with activity-based instructions | Mobile indoor pedestrian guidance UX | State machine guidance: Walking mode (floating arrows) vs Paused mode (floor map) | Waypoint progression logic and 3D directional arrows | [10.1145/2037373.2037406](https://doi.org/10.1145/2037373.2037406) |
| `Isikdag2013` | A BIM-Oriented Model for supporting indoor navigation requirements | BIM-based indoor topological graph modeling | Directed graph extraction: $G = (V, E)$ with vertical transition edge weights | Multi-floor architectural connectivity graph in `SmartKioskHandoffManager.cs` | [10.1016/j.compenvurbsys.2013.05.001](https://doi.org/10.1016/j.compenvurbsys.2013.05.001) |
| `Ballagas2006` | The Smart Phone: A Ubiquitous Input Device | Mobile phone interaction with situated public screens | Optical scanning and point-and-shoot interaction primitives | Camera scanning mechanics bridging kiosk displays to phones | [10.1109/MPRV.2006.18](https://doi.org/10.1109/MPRV.2006.18) |
| `Davies2012` | Open Display Networks: A Communications Medium for the 21st Century | Ubiquitous public display kiosks and interactive screens | Public screen interaction lifecycle: Ambient $\to$ Attracted $\to$ Engaged | Kiosk UI states and 45-second session lease timeout mechanics | [10.1109/MC.2012.114](https://doi.org/10.1109/MC.2012.114) |
| `Rekimoto2000` | CyberCode: designing augmented reality environments with visual tags | Visual tag systems for physical-digital linking | 2D visual barcode matrix decoding and camera pose recovery | High-density QR code serialization encoding compressed route tokens | [10.1145/354666.354667](https://doi.org/10.1145/354666.354667) |

---

## 3. Deep Methodological Deconstruction of Foundational Papers

### 3.1 Qiao, Ren, Dustdar, & Liu (2019) - Web AR Survey
- **Core Contribution:** Established that the primary barrier to consumer AR adoption is the friction of downloading native application binaries ($> 100\text{ MB}$); WebXR executes within standard mobile browsers instantly, eliminating app-store installation overhead.
- **Project Role:** Governs the zero-friction architectural choice for Group 11: visitors scan the kiosk QR code and immediately launch in-browser 3D guidance without downloading an app.

### 3.2 Mulloni, Seichter, & Schmalstieg (2011) - Handheld AR Indoor Navigation
- **Core Contribution:** Demonstrated that continuous dense 3D visual overlays induce cognitive tunneling; proposed activity-based guidance that presents lightweight, discrete 3D chevrons along path segments and switches to spatial confirmations only at decision junctures.
- **Project Role:** Informs the visual guidance design in `Assets/Scripts/MobileWebXRRouteNavigator.cs`, keeping screen clutter minimal so visitors walk safely.

### 3.3 Isikdag, Zlatanova, & Underwood (2013) - BIM Indoor Navigation
- **Core Contribution:** Formulated semantic methodologies for extracting 3D geometric navigation graphs directly from architectural BIM building data, specifically modeling elevator and stairwell vertical transition penalties.
- **Project Role:** Provides the multi-storey campus graph algorithms utilized in `SmartKioskHandoffManager.cs`.

### 3.4 Ballagas, Borchers, Rohs, & Sheridan (2006) - Smart Phone Ubiquitous Input
- **Core Contribution:** Conceptualized smartphones as universal mobile intermediaries that bridge users across situated public displays, establishing visual scanning (barcodes/QR) as the fastest and most intuitive physical interaction primitive.
- **Project Role:** Serves as the foundational theoretical basis for the kiosk-to-mobile handoff interaction loop.

### 3.5 Davies, Langheinrich, José, & Schmidt (2012) - Open Display Networks
- **Core Contribution:** Formulated architectural principles for public interactive digital signage, emphasizing brief engagement windows (under 60 seconds) and seamless session migration to personal devices to prevent kiosk queue congestion.
- **Project Role:** Dictates the kiosk session lifecycle and automated 45-second timeout logic to ensure kiosks never lock up during peak visitor arrival rushes.

### 3.6 Rekimoto & Ayatsuka (2000) - CyberCode Visual Tags
- **Core Contribution:** Developed the pioneering visual tag matrix for ubiquitous computing, demonstrating that high-contrast 2D matrices can securely encode contextual state vectors for instant optical transfer.
- **Project Role:** Governs the QR token schema used in `SmartKioskHandoffManager.cs` to transmit compressed waypoint coordinates.

---

## 4. Theoretical Synthesis & Research Gaps Identified
While smart kiosks (`Davies2012`) and WebAR (`Qiao2019`) have been investigated separately, **no prior research has integrated public entrance kiosks with instantaneous QR-based WebXR mobile handoffs to completely eliminate physical paper map waste while measuring comparative transit duration and disorientation error reductions across multi-storey university complexes**. Group 11 resolves this challenge.
