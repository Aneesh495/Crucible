namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Cli;
using Crucible.Concurrency;
using Crucible.Protocols;
using Crucible.Runtime;
using Xunit;

public class CliIntegrationTests
{
    [Fact]
    public void Cli_list_command_succeeds()
    {
        var exitCode = Program.Main(new[] { "list" });
        Assert.Equal(0, exitCode);
    }

    [Fact]
    public void Cli_run_command_succeeds_with_max_steps()
    {
        var exitCode = Program.Main(new[] { "run", "--workload", "raft", "--seed", "42", "--nodes", "3", "--max-steps", "100" });
        Assert.Equal(0, exitCode);
    }

    [Fact]
    public void Cli_run_command_succeeds_with_steps_alias()
    {
        var exitCode = Program.Main(new[] { "run", "--workload", "or-set", "--seed", "42", "--nodes", "3", "--steps", "100" });
        Assert.Equal(0, exitCode);
    }

    [Fact]
    public void Cli_run_unknown_workload_returns_error_exit_code()
    {
        var exitCode = Program.Main(new[] { "run", "--workload", "unknown-workload-xyz" });
        Assert.Equal(1, exitCode);
    }

    [Fact]
    public void Cli_run_invalid_parameters_returns_error_exit_code()
    {
        var exitCode = Program.Main(new[] { "run", "--workload", "raft", "--nodes", "-5" });
        Assert.Equal(1, exitCode);
    }

    [Fact]
    public void Cli_run_require_quiescence_returns_inconclusive_when_step_bound_exhausted()
    {
        // 5 steps is too short for 5-node Raft to reach quiescence
        var exitCode = Program.Main(new[] { "run", "--workload", "raft", "--seed", "42", "--nodes", "5", "--steps", "5", "--require-quiescence" });
        Assert.Equal(4, exitCode);
    }

    [Fact]
    public void Cli_explore_pct_succeeds()
    {
        var exitCode = Program.Main(new[] { "explore", "--workload", "two-phase-commit", "--strategy", "pct", "--schedules", "2", "--steps", "50" });
        Assert.Equal(0, exitCode);
    }

    [Fact]
    public void Cli_explore_dfs_succeeds()
    {
        var exitCode = Program.Main(new[] { "explore", "--workload", "two-phase-commit", "--strategy", "dfs", "--schedules", "2", "--steps", "50" });
        Assert.Equal(0, exitCode);
    }

    [Fact]
    public void Cli_replay_and_report_commands_handle_valid_and_divergent_schedules()
    {
        var entry = ScenarioHost.RegistryView.Get("two-phase-commit");
        var options = new WorkloadOptions { NodeCount = 3 };
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);

        var explorerOpts = new ExplorerOptions { MaxSteps = 50, MaxSchedules = 1 };
        var normalResult = new PctExplorer(explorerOpts).Run("two-phase-commit", 42, builder, options, entry.Workload.GlobalInvariants);
        Assert.NotNull(normalResult.Trace);

        var tempDir = Path.Combine(Path.GetTempPath(), "crucible_cli_test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        var validPath = Path.Combine(tempDir, "valid.schedule.json");
        var corruptPath = Path.Combine(tempDir, "corrupted.schedule.json");

        try
        {
            ScheduleSerializer.WriteToFile(normalResult.Trace, validPath);

            // 1. Replay valid schedule -> exit code 0
            var replayExit = Program.Main(new[] { "replay", "--schedule", validPath });
            Assert.Equal(0, replayExit);

            // 2. Report command on valid schedule -> exit code 0
            var reportExit = Program.Main(new[] { "report", "--schedule", validPath });
            Assert.Equal(0, reportExit);

            // 3. Corrupted schedule with mismatched transition key -> exit code 3 (Replay divergence)
            var corruptedChoices = normalResult.Trace.Choices.Select((c, idx) =>
                idx == 0
                    ? new ScheduleChoice
                    {
                        Step = c.Step,
                        Kind = c.Kind,
                        ChosenIndex = c.ChosenIndex,
                        CandidateCount = c.CandidateCount,
                        TransitionKey = "non_existent_divergent_key",
                        CandidateKeys = c.CandidateKeys
                    }
                    : c).ToArray();

            var corruptedTrace = new ScheduleTrace
            {
                Seed = normalResult.Trace.Seed,
                Workload = normalResult.Trace.Workload,
                Options = normalResult.Trace.Options,
                Choices = corruptedChoices,
                Faults = normalResult.Trace.Faults,
                StepsExecuted = normalResult.Trace.StepsExecuted
            };
            ScheduleSerializer.WriteToFile(corruptedTrace, corruptPath);

            var corruptReplayExit = Program.Main(new[] { "replay", "--schedule", corruptPath });
            Assert.Equal(3, corruptReplayExit);
        }
        finally
        {
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
    }
}
