# IVRAR Git Branching & Submission Step-by-Step Guide

```
main (Faculty Reviewed & Protected)
  ▲
  │ Pull Request (via GitHub UI)
  │
feat/i066-spatial-telemetry (Individual Student Working Branch)
```

### Step 1: Clone the Monorepo
```bash
git clone https://github.com/sunny-nanade/NMIMS-MPSTME-IVRAR-2026.git
cd NMIMS-MPSTME-IVRAR-2026
```

### Step 2: Create Your Feature Branch
```bash
git checkout -b feat/<your-roll-no>-onboarding
```

### Step 3: Edit Your Group's Directory
Only make changes inside your designated folder: `Group_XX_<Names>/`

### Step 4: Stage and Commit
```bash
git status
git add Group_XX_<Names>/
git commit -m "docs(group-XX): complete sprint 0 team roster and toolchain spec"
```

### Step 5: Push Branch to GitHub
```bash
git push -u origin feat/<your-roll-no>-onboarding
```

### Step 6: Open a Pull Request
Visit `https://github.com/sunny-nanade/NMIMS-MPSTME-IVRAR-2026` on your browser, click **Compare & pull request**, set the base to `main`, and submit!
