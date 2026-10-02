# M0-004 local analysis of the existing dump

- Parent authorized local read-only analysis of the existing Unity.exe.20568.dmp. Used already-installed Unity Android NDK LLDB 18.0.3, with --no-lldbinit; no installation, SDK/OS change, Unity run, dump upload or requested symbol-server access. No memory contents, arguments, local variables or credentials displayed.
- Faulting thread: LLDB thread 1, Windows TID 15940 (0x3e44), process 20568. Exception-stream metadata independently parsed from the existing minidump header/directory: code 0xC0000409, one parameter, parameter[0]=7. This matches WER exception data; no assertion that the code alone means stack overflow.
- Faulting-thread unwind, limited to function/module names and offsets:
  1. ucrtbase.dll!abort + 78.
  2. mrutilitykitshared.dll RVA 0xEC5E.
  3. mrutilitykitshared.dll RVA 0xEA7B.
  4. mrutilitykitshared.dll!SetTrackingSpacePoseGetter + 94 (RVA 0x120E).
- LLDB labels the two intermediate MRUK frames as GetHeadsetPoseAtTime + 19454 and + 18971. These are nearest available exported-symbol labels with large offsets, not verified private function names. Do not interpret them as evidence that headset pose acquisition itself caused the failure. Unwind ends after frame 4; no complete managed/native stack is claimed.
- This establishes MRUK native code on the immediate abort call path. Together with the existing final native log asserting a missing gGlobalContext when setting the tracking-space getter, it supports an inference that a missing-context native fatal check terminates the process. It does not establish why CreateGlobalContext failed earlier, nor prove that the caught managed log-decode exception caused that failure.
- Remaining blocker: the first native context-creation failure message could not be decoded, and no private MRUK symbols/source or relevant original message bytes were recovered by this limited analysis. Path-encoding failure and a separately localized native error remain hypotheses. No further SDK retry is justified merely by the abort stack; define an approved diagnostic that recovers the initial native failure evidence first.
- Dump remains local at its existing location. Only this sanitized summary is published. MRUK failure_count remains 2; one retry unspent. Phase/Technical Gate remain BLOCKED; no failed implementation is committed.
