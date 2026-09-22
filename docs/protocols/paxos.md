# Multi-Paxos (simplified)

A minimal acceptor / proposer / learner layout:

- **Acceptors** promise ballots and accept values
- **Proposer** runs two-phase prepare/accept with quorum counting
- **Learner** observes `PaxosLearn`

Agreement invariant: accepted values among acceptors must not conflict once chosen.

This model uses a single proposer timer for demo traffic; it is not full Multi-Paxos
with per-slot leaders.
