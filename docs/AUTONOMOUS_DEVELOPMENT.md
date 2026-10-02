# AUTONOMOUS DEVELOPMENT RULES

This document defines how AI agents operate autonomously on this project.
Task PASS authority is AGENTS.md. Test/measurement authority is TEST_PLAN.md.

## Roles

### Director (Human)
- Decides game design and approves version/settings changes.
- Evaluates VR feel in headset (sickness, weight, satisfaction).
- Supplies Director Gate and Device Gate decisions for an identified candidate.
- Approves Phase transitions and provides feedback via HUMAN_FEEDBACK.md.

### Dots (Lead / Producer)
- Reads PROJECT_STATUS.md, decomposes tasks, and delegates implementation to Codex.
- Monitors results, retries within limits, or escalates.
- Updates planning/gate state between tasks and reports handoff readiness.
- May transcribe explicit Director decisions with their source, build/revision, and timestamp; never invents approval.
- Does NOT write game code directly.

### Codex (Unity Engineer)
- Implements C#, approved Editor settings, ScriptableObjects, Prefabs, and Scenes.
- Runs compile, Console checks, tests, builds, and required technical validation.
- Saves evidence and updates task results in PROJECT_STATUS.md before committing.
- Follows AGENTS.md and DEVELOPMENT_RULES.md; never approves Human Gates.

### Meta XR Operator (VR QA)
- Operates the running XR app, supplies controller/head input, and captures evidence.
- Reports objective results per TEST_PLAN.md.
- Flags subjective evaluations as Human QA Required, not automated PASS.

Only one agent may write a task branch/worktree at a time. Dots does not edit PROJECT_STATUS.md concurrently with Codex's task transaction.

## Validation Profiles

Before implementation, record the profile, required test IDs/checks, execution environment, and any justified N/A in PROJECT_STATUS.md.

| Profile | Required validation |
|---|---|
| DOCS_ONLY | No executable/project/asset changes; check document consistency, references, scope, secrets, and final diff. Runtime checks may be N/A with an explicit no-runtime-change reason. |
| FOUNDATION | Verify the changed setup/artifact and compile once the project exists. Run the relevant available foundation tests. A not-yet-created runtime feature may justify task-level N/A, but required M0 Phase tests must still pass before handoff. |
| RUNTIME | Compile, runtime Console, relevant unit/PlayMode tests, and required XR/device/performance tests. New gameplay behavior needs test coverage. |

A missing required Editor, SDK, test, device, or measurement is BLOCKED, not N/A. Do not use DOCS_ONLY to claim an untested implementation is complete.
Before project creation, confirm the exact Editor in ADR-001 and the required approval for ProjectSettings changes. This document does not grant blanket setup-tool or package-upgrade permission.

## Autonomous Development Loop

```text
1. Read the committed PROJECT_STATUS.md and inspect branch/worktree state.
   If BLOCKED or WAITING_HUMAN, handle the recorded condition; do not pick another Phase.
2. Select the next eligible AUTO task in the current Phase using dependencies.
   Human Gate checklist entries are never AUTO tasks.
3. Confirm scope, approvals, validation profile, and required checks.
   Delegate one implementation unit to Codex.
4. Implement, then run the required checks:
   compile -> runtime Console -> unit tests -> PlayMode tests
   -> applicable XR/operator checks -> required Quest build/performance checks.
5. On FAIL or missing required validation:
   save evidence -> update task/blocker/root-cause history
   -> review only diagnostic/status changes -> diagnostic/state-only commit.
   Retry only while permitted; STOP on a hard breach or retry limit.
6. On successful technical checks:
   a. Save sanitized evidence.
   b. Prepare PROJECT_STATUS.md with task result, evidence, and remaining blockers.
   c. Stage intended changes and review the final staged diff, INCLUDING status/evidence.
   d. Re-run affected checks if implementation or requirements changed after testing.
   e. Commit implementation + evidence + status together only if AGENTS.md PASS holds.
   f. Advance only when commit succeeds. Uncommitted PASS text is provisional.
7. When all current-Phase AUTO tasks and required technical tests pass:
   prepare Technical Gate = PASS, Human Gates = WAITING,
   Phase State = WAITING_HUMAN, and the handoff evidence/report.
   Review and commit this state (it may be included in the final AUTO task commit).
   Report to Director and STOP; do not claim Phase COMPLETE.
8. After explicit Director decisions for the same candidate:
   if both Human Gates are approved, record their source and Phase COMPLETE,
   review/commit the state record, and await explicit next-Phase authorization.
   otherwise record rejection/blockers and wait for authorized corrective work.
```

If final diff review fails, correct the status/result before using the failure path.
If commit fails, do not advance or claim durable completion; preserve the working tree and report the failure. Never reset/clean away another agent's or the Director's changes.
A commit's own SHA is read from Git history; do not require a self-referential SHA edit after every commit.

## Human Gate

```text
AUTO tasks and required technical tests complete
 -> Technical Gate PASS
 -> Director Gate WAITING + Device Gate WAITING
 -> Human Gate WAITING / Phase WAITING_HUMAN
 -> state saved in Git and candidate reported
 -> Director evaluates candidate and supplies both decisions
 -> both approved: Human Gate PASS / Phase COMPLETE
 -> explicit transition authorization: next Phase may begin
```

Comfort, fun, perceived haptics, and full human playtests are not prerequisites for Technical Gate PASS. They are prerequisites for Human Gate PASS.
Automated device telemetry is technical evidence; it does not approve a worn-headset Device Gate.

The handoff contains: completed AUTO tasks, test outcomes/evidence, candidate code revision and APK checksum/location, remaining issues, and required human checks.
Human decisions must identify the candidate and include reviewer, timestamp, outcome, and feedback in HUMAN_FEEDBACK.md. Dots may record the Director's explicit decision, but silence, elapsed time, or a prior build's approval never counts.
A generic "next phase" does not waive an unperformed Device Gate or failed technical test.

If a Human Gate is rejected, set Human Gate = FAIL and Phase State = BLOCKED, record feedback, and wait for authorized fixes.
Changed implementation/assets/settings or relevant requirements invalidate previous technical/human approvals. After revalidation, request new decisions for the new candidate. Documentation that only records evidence/decisions does not by itself change the tested build.

### What Director checks at each gate
- M0: Does it run on Quest 3, with correct display, head tracking, and controllers?
- M1: Does it feel like sitting in a mech cockpit? Is leaning/turning comfortable?
- M2: Is shooting fun? (Most critical gate.) Is the combat session comfortable?
- M3: Does combat feel satisfying? Are haptics, sound, and optional effects acceptable?
- M4: Is a full stage engaging and comfortable on the identified candidate?
- M5: Does it look good enough?
- M6: Is performance stable at 90Hz?
- M7: Is it ready for release?

The current task/checklist definitions cover M0-M4. Define later-Phase tasks and tests before scheduling them.

## When to STOP and Ask Director

- A required game-design decision is missing, except explicitly permitted temporary balance values.
- Multiple approaches affect gameplay feel or a subjective judgment is needed.
- Any unapproved ProjectSettings change or Unity/SDK/package version change is needed.
- Security/authentication/payment settings require action.
- The same root cause reaches three recorded failures.
- A required environment is unavailable, an emergency performance breach occurs, or a Human Gate is waiting/rejected.

## When to Continue Autonomously

- The current-Phase task is defined, approved, and not blocked.
- The implementation choice does not change gameplay feel or reverse an ADR.
- A scoped bug fix, test, documentation update, or cleanup has the required checks.
- A permitted retry remains for the same task and no STOP condition applies.

Do not choose unrelated tasks to bypass a blocker or a Human Gate.

## Git Rules

- Use task/Phase branches such as `phase/m0-foundation` and `phase/m1-cockpit`.
- Implementation commit: all applicable AGENTS.md PASS conditions satisfied; evidence and PROJECT_STATUS.md included before final diff review and commit.
- Implementation message: `Phase MX: <short description>`.
- Failure-time commits are restricted to the diagnostic/state-only exception below.
- No force push; no direct push to main without PR; no unapproved ProjectSettings changes.
- Keep commits focused. Preserve prior work; do not use blanket staging or destructive cleanup.
- A diagnostic commit does not make a branch merge-ready or a Phase complete.

## Diagnostic / State-Only Commits

This exception preserves truthful failure state in Git without committing failed feature code.
It is not an exception to gameplay tests, budgets, or Human Gate authority.

### Allowed paths
- `docs/PROJECT_STATUS.md`: task/gate/blocker state, retry history, and evidence references only.
- `docs/KNOWN_ISSUES.md`: failure diagnosis, reproduction, and follow-up only.
- `docs/validation/**/*.md`: sanitized textual evidence and failure reports only.

Do not change gameplay values, task acceptance criteria, budgets, approvals, or unrelated roadmap content under this exception.
No source code, tests, Scenes, Prefabs, materials, assets, ProjectSettings, packages, binaries, executable scripts, or secrets are allowed in a diagnostic commit.

### Required failure record
- Task/Phase, branch, base commit, last known passing implementation, and tested working-tree fingerprint.
- Failed check IDs/commands, actual results, timestamps, and sanitized evidence references.
- Stable root-cause ID, failure_count, attempted fixes, and outcomes.
- Current task state, compile/test condition, blocker, and next action.
- Location/status of uncommitted implementation work. State explicitly when it is only local and not recoverable from Git on another machine.

### Procedure
1. Save the report and update status to FAIL/BLOCKED; do not mark the feature complete or reset retry counts.
2. Inspect the Git index. If it contains unrelated/feature changes, stop this commit attempt rather than including or discarding them.
3. Explicitly stage only allowed diagnostic files/hunks. Never use `git add .` or `git add -A` for this path.
4. Review the staged paths and full staged diff. Verify Markdown/references, truthful states/counters, and absence of secrets, binary payloads, hidden code patches, or scope/approval changes.
5. Commit with `State MX: <task-id> <root-cause-id> <FAIL|BLOCKED> - <summary>` only after those checks pass.
6. Leave failed implementation unstaged and intact. Re-run the full required implementation validation before any later feature commit.

If dirty implementation prevents safe docs-only validation, use an isolated worktree based on the current branch HEAD, copy only allowed sanitized records, validate, and commit there with exclusive branch ownership. Never bypass hooks or include failed code to force the commit through.
If Git itself is unavailable or commit fails, retain a local report and tell the Director that Git persistence failed. Do not claim the state is safely recorded remotely.

Human Gate request/approval bookkeeping after a technically passing candidate uses a normal DOCS_ONLY state update with the actual decision evidence; the failure exception cannot manufacture an approval.

## Error Recovery

The failure counter is per stable ROOT CAUSE and persists across sessions/agents, not per tool call or session.
The initial observed failure counts as 1. Each failed retest of the same cause increments it. At 3 failures, STOP: there are at most two repair/retest attempts after the initial failure.
This follows DEVELOPMENT_RULES.md's "same fundamental error occurs 3 times" rule.
Do not reset the count by renaming a test, starting a new session, or making a diagnostic commit.
Different verified causes have separate records; record the evidence when splitting causes.

```text
Failure -> identify/reuse root-cause ID -> increment failure_count
 -> save evidence/status -> diagnostic/state-only commit
 -> hard STOP condition or failure_count >= 3: report and wait
 -> otherwise apply one scoped fix and re-test
 -> PASS: complete normal implementation validation/commit
 -> same failure: repeat with the persisted counter
```

Report the cause, each attempted fix and result, current project condition, local-only work, recommended action, and whether rollback is proposed. Do not perform destructive rollback without approval.
A Director-authorized new attempt after STOP must be recorded explicitly; do not silently erase the previous failure history.

## Performance Acceptance and Emergency Stop

TEST_PLAN.md Performance Acceptance owns measurement and classification.

- PASS requires sustained application rendering at 90Hz and application CPU/GPU frame times each < 11.1ms, plus other applicable budgets.
- Missing an acceptance target is FAIL even when application FPS remains >= 72. Fix within the retry policy; no feature commit or Phase Technical PASS.
- Application FPS < 72 or CPU/GPU frame time > 13.9ms is an immediate STOP/BLOCKED condition.
- Draw calls > 300/eye, gameplay GC allocation > 0 B/frame, texture memory > 512MB, or another hard-limit breach also triggers STOP/BLOCKED.
- Stop the test safely, save measurements/status through the diagnostic path, and report to Director.
- Never relax budgets, lower the target refresh rate, substitute compositor/reprojected FPS, or run endless optimization loops.

## Cloud vs Local

### Cloud (PC can sleep only for checks actually available there)
- Code generation, static analysis, Git operations, documentation.
- Unit tests only when their required runtime/dependencies are installed in that environment.
- Unity-dependent tests require a configured Unity runner; do not assume cloud availability.

### Local (required workstation/tools must be available)
- Unity Editor, XR Simulator, Meta VR CLI, ADB/Quest deployment, Meta XR Operator.
- Real-device tests and the Director's worn-headset checks.
- The workstation must be on; required desktop applications/connections must be active.

A missing local connection is a recorded BLOCKED condition, not permission to invent a test result or claim unattended execution.

## Performance Monitoring

After every significant runtime implementation, run the applicable Quest measurements in TEST_PLAN.md.
Save the evidence, update PROJECT_STATUS.md, review the complete staged diff, and only then commit a passing implementation.
Normal budget failures use bounded repair; emergency breaches stop immediately.
