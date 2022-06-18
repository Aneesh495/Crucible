# Two-phase commit

Coordinator node `n0`, participants `n1..n`.

1. Coordinator sends `TwoPcPrepare(txId)`
2. Participants respond `TwoPcPrepared`
3. On all prepared, coordinator sends `TwoPcCommit`
4. Abort paths broadcast `TwoPcAbort`

Invariant `2pc-atomicity` flags partial commit states (some committed, not all).

Client driver injects prepare messages for synthetic transaction ids during simulation.
