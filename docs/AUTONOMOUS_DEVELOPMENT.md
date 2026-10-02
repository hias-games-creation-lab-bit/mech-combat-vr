# AUTONOMOUS DEVELOPMENT RULES

This document defines how AI agents operate autonomously on this project.

## Roles

### Director (Human)
- Decides game design
- Evaluates VR feel in headset (sickness, weight, satisfaction)
- Approves Phase transitions
- Provides subjective feedback via HUMAN_FEEDBACK.md

### Dots (Lead / Producer)
- Reads PROJECT_STATUS.md to determine current state
- Decomposes tasks into implementable units
- Delegates implementation to Codex
- Monitors test results
- Handles failures (retry or escalate)
- Updates PROJECT_STATUS.md
- Reports Phase completion to Director
- Does NOT write game code directly

### Codex (Unity Engineer)
- Implements C# code
- Configures Unity settings via Editor Scripts
- Creates/modifies ScriptableObjects, Prefabs, Scenes
- Runs compile, unit tests, PlayMode tests
- Executes builds
- Commits to Git
- Follows AGENTS.md and DEVELOPMENT_RULES.md strictly

### Meta XR Operator (VR QA)
- Operates running XR app via MCP
- Executes controller input, head pose changes
- Takes screenshots
- Reports PASS/FAIL per TEST_PLAN.md test case
- Flags "Human QA Required" for subjective evaluations

## Autonomous Development Loop

```
1. Read PROJECT_STATUS.md
2. Identify current Phase and next incomplete task
3. Decompose task into smallest implementable unit
4. Delegate to Codex
5. Codex implements and runs:
   a. Compile
   b. Unit Test
   c. PlayMode Test
6. If XR test is applicable:
   a. XR Simulator test
   b. Meta XR Operator automated test (if available)
7. If FAIL:
   a. Analyze error
   b. Delegate fix to Codex
   c. Re-test
   d. If same root cause fails 3 times -> STOP, report to Director
8. If PASS:
   a. Git commit (format: "Phase MX: <description>")
   b. Update PROJECT_STATUS.md
   c. Proceed to next task
9. If all tasks in Phase complete:
   a. Set Human Gate = WAITING
   b. Report to Director
   c. STOP and wait for Director approval
```

## Human Gate

Phase transitions require Director approval.

```
Phase complete
 -> Automated tests: ALL PASS
 -> Set Human Gate = WAITING
 -> Report to Director:
    - Completed tasks
    - Test results
    - Git commits
    - Remaining issues
    - Items requiring Quest 3 real-device check
 -> STOP
 -> Wait for Director to:
    1. Test on Quest 3
    2. Provide feedback in HUMAN_FEEDBACK.md
    3. Say "Next phase" or "Fix these issues first"
```

### What Director checks at each gate:
- M0: Does it run on Quest 3?
- M1: Does it feel like sitting in a mech cockpit?
- M2: Is shooting fun? (Most critical gate)
- M3: Does combat feel satisfying?
- M4: Is a 5-minute stage engaging?
- M5: Does it look good enough?
- M6: Is performance stable at 90Hz?
- M7: Is it ready for release?

## When to STOP and Ask Director

- GAME_DESIGN.md doesn't cover a required decision
- Multiple design approaches exist that affect gameplay feel
- ProjectSettings needs major changes
- Unity/Meta XR SDK/OpenXR version change needed
- Security/authentication/payment settings
- Same error 3+ times after attempted fixes
- Anything requiring subjective VR evaluation

## When to Continue Autonomously

- Task is clearly defined in GAME_DESIGN.md
- Implementation choice doesn't affect gameplay feel
- Bug fix within existing architecture
- Test creation/update
- Documentation update
- Code cleanup within scope

## Git Rules

- Branch: `phase/m0-foundation`, `phase/m1-cockpit`, etc.
- Commit only after ALL tests pass
- Commit message: `Phase MX: <short description>`
- Never force push
- Never push directly to main without PR
- Never commit ProjectSettings changes without approval
- Keep commits small and focused

## Error Recovery

Retry limit is per ROOT CAUSE, not per attempt count.
Different errors with different causes each get their own 3-attempt budget.

```
Error occurs
 -> Identify root cause
 -> Attempt fix #1
 -> Re-test
 -> If SAME root cause: Attempt fix #2
 -> Re-test
 -> If SAME root cause: Attempt fix #3
 -> Re-test
 -> If same error: STOP

Report to Director:
- Error description
- 3 attempted fixes and why each failed
- Current project state (compiles? tests pass?)
- Recommended action
- Whether rollback is needed
```

## Performance Budget Breach = BLOCKED

If any of the following are exceeded, the task is BLOCKED:
- FPS < 72Hz (hard floor)
- GPU frame time > 13.9ms
- Draw calls > 300
- GC Alloc > 0 B/frame during gameplay
- Texture memory > 512MB

Do NOT attempt to fix by relaxing the budget.
Do NOT attempt endless optimization loops.
Report to Director with measurements and recommended action.

## Cloud vs Local

### Cloud (PC can sleep)
- Code generation
- Unit tests
- Static analysis
- Git operations
- Documentation updates

### Local (PC must be on + ChatGPT Desktop open)
- Unity Editor operations
- XR Simulator
- Meta VR CLI
- ADB / Quest 3 deployment
- Meta XR Operator
- Real device testing

## Performance Monitoring

After every significant implementation:
- Check frame time against 11.1ms budget
- Check draw call count against 150/eye target
- Check GC allocation (must be 0 during gameplay)
- If any budget exceeded: fix before proceeding
- Never relax performance budgets without Director approval
