# Toolchain & Hardware Specification
**Group ID:** IVRAR_GROUP_14  
**Project:** To what extent does a predictive ghost-avatar digital twin in Unity VR mitigate teleoperation path tracking error and collision frequency for planetary rover operators under simulated high-latency (1.5-second to 5-second) transmission delays?  

## Recommended Software Stack
* **Game Engine:** Unity 2022.3 LTS (Long Term Support)
* **XR Plugin Architecture:** OpenXR Plugin (>= 1.8.0)
* **Toolkit:** XR Interaction Toolkit (XRI >= 2.5.2 or 3.0.0)
* **Graphics Pipeline:** Universal Render Pipeline (URP - Mobile/Standalone Profile)
* **Code Editor:** Visual Studio 2022 / VS Code with C# Dev Kit

## Target Deployment Platforms
* Meta Quest 2 / Quest 3 / Quest Pro (via Android OpenXR)
* Standalone PC VR (OpenXR Link / AirLink)
* WebXR (via WebGL 2.0 / Babylon.js / Three.js where applicable)

## Telemetry & Data Collection Protocol
* Sampling Rate: >= 60 Hz head/hand transform recording
* Output Format: Structured CSV (`/telemetry/trial_<id>.csv`)
* Metrics: Completion Time (s), Path Trajectory Deviation (m), Error Rates, Collision Events
