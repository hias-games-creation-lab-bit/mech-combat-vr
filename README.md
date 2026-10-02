# BFTK VR - Mech Combat for Quest 3

VR cockpit combat game inspired by "Bonds of the Battlefield" (Senjou no Kizuna).
Meta Quest 3 exclusive, single-player.

## Tech Stack
- Unity 6.3 LTS (6000.3.25f1, exact Editor version) / URP (Forward only) / OpenXR / Vulkan
- Meta XR SDK v207+ / Meta VR CLI / Meta XR Operator
- 90Hz acceptance target; application CPU/GPU frame times each < 11.1ms
- 72 FPS is an emergency floor, not a passing performance target

## Development
This project uses autonomous AI development.
See `AGENTS.md` for AI instructions and `docs/AUTONOMOUS_DEVELOPMENT.md` for the development loop.
Implementation commits include tests/evidence and the updated project status before final diff review and commit.
Failed feature code is not committed; restricted diagnostic/state-only commits preserve blockers and retry history.
A Phase finishes only after Technical Gate PASS and explicit Director/Device Gate approval.

The Unity pin is defined in `docs/DECISIONS.md` ADR-001. Do not substitute another patch or auto-upgrade.
M0 must verify SDK compatibility, Android build, and Quest 3 launch; the version selection is not a claim that those tests have already passed.

## Project Docs
- `docs/PROJECT_STATUS.md` - Current state and task list
- `docs/GAME_DESIGN.md` - Complete game specification
- `docs/DEVELOPMENT_RULES.md` - Code quality rules
- `docs/TEST_PLAN.md` - Test cases per phase
- `docs/KNOWN_ISSUES.md` - Bug tracking
- `docs/AUTONOMOUS_DEVELOPMENT.md` - AI autonomy rules
- `docs/HUMAN_FEEDBACK.md` - Director's VR feedback
