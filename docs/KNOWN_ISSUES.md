# KNOWN ISSUES



## Format

### ISSUE-XXX: Title
- **Status**: Open / Fixed / Won't Fix
- **Phase**: M0 / M1 / M2 / ...
- **Description**: What happens
- **Steps to Reproduce**: How to trigger it
- **Expected**: What should happen
- **Actual**: What actually happens
- **Workaround**: Temporary fix if any
- **Fix**: How it was resolved (when fixed)

## ISSUE-M0-004: MRUK global-context initialization blocks SDK validation

- **Status**: Open; M0-004 BLOCKED, runtime failure_count=2.
- **Phase**: M0.
- **Actual**: Caught native-log decode exception, context creation failure, then native abort in prior runs; initial failure cause is unknown.
- **Diagnostic**: The single approved LLDB launch did not reach the intended project/runtime. No test result or callback bytes; no counter increment or relaunch.
- **Evidence**: [Bounded live diagnostic](validation/m0/2026-10-02-m0-004-live-diagnostic.md), [existing dump analysis](validation/m0/2026-10-02-m0-004-local-dump-analysis.md).
- **Workaround**: None validated. Failed implementation remains unstaged; further execution requires a new concrete authorized scope.
