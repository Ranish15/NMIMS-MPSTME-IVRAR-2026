# IVRAR PBL Git Contribution Guidelines

All students enrolled in IVRAR (Academic Year 2026-27) must adhere strictly to these engineering contribution guidelines.

## 1. Branch Naming Standard
* Feature Branch: `feat/<roll_no>-<module_name>` (e.g. `feat/i066-acoustics-rt60`)
* Bugfix Branch: `fix/<roll_no>-<bug_description>` (e.g. `fix/n083-raycast-collider`)
* Documentation: `docs/<roll_no>-<paper_section>` (e.g. `docs/n087-ieee-methodology`)

## 2. Conventional Commit Format
Each commit message must follow the Conventional Commits specification:
* `feat(scope): add spatial raycast listener`
* `fix(scope): resolve null reference in audio listener`
* `perf(scope): reduce draw calls by combining mesh instances`
* `docs(scope): complete introduction and related work draft`
* `test(scope): add telemetry logging assertions for latency`

## 3. Pull Request (PR) Requirements
1. Pull Requests must target the `main` branch.
2. PR title must include your Group number and brief scope: `[Group-01] Sprint 0 Onboarding & Toolchain Spec`.
3. PR description must include:
   - What changed
   - Verified local Unity Editor version
   - Screenshots / video GIF of scene or test script output
4. Every group member must participate in code reviews before requesting faculty merge.
