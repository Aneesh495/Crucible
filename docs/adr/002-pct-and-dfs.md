# ADR 002: PCT plus bounded DFS

## Status

Accepted

## Context

Exhaustive interleaving explodes quickly. We need practical exploration with
occasionally deep counterexamples.

## Decision

Default CLI exploration uses PCT. Optional DFS explores first branch alternates
to a bounded depth. Random exploration provides cheap regression coverage.

## Consequences

- Not complete verification
- Good engineering tradeoff for finding real bugs in protocol code paths
