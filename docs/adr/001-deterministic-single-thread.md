# ADR 001: Deterministic single-threaded simulation

## Status

Accepted

## Context

Real concurrent tests flake. Distributed tests need clusters. We want replayable
counterexamples without maintaining cloud infrastructure.

## Decision

Run all workloads on one thread. Nondeterminism appears only at explicit
schedule points (message delivery order, timer interleaving, oracle choices,
shared-memory ops).

## Consequences

- Positive: perfect replay, easy CI, small hardware footprint
- Negative: cannot catch low-level memory model bugs that require real CPU reordering
- Mitigation: shared-memory ops still explore interleavings explicitly
