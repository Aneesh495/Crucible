namespace Crucible.Cli;

using Crucible.Abstractions;
using Crucible.Checking;
using Crucible.Concurrency;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Runtime;

public static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return 1;
            }

            return args[0].ToLowerInvariant() switch
            {
                "run" => RunCommand(args),
                "explore" => ExploreCommand(args),
                "replay" => ReplayCommand(args),
                "report" => ReportCommand(args),
                "list" => ListCommand(),
                "--help" or "-h" or "help" => PrintHelpReturn(),
                _ => Unknown(args[0])
            };
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
        catch (KeyNotFoundException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Fatal Error: {ex.Message}");
            return 1;
        }
    }

    private static int PrintHelpReturn()
    {
        PrintHelp();
        return 0;
    }

    private static int Unknown(string cmd)
    {
        Console.Error.WriteLine($"Unknown command '{cmd}'.");
        PrintHelp();
        return 1;
    }

    private static void PrintHelp()
    {
        Console.WriteLine("""
            crucible — deterministic simulation forge

            crucible list
            crucible run     --workload <name> --seed <n> [--nodes <n>] [--steps <n> | --max-steps <n>] [--chaos mild|adversarial] [--require-quiescence]
            crucible explore --workload <name> --seed <n> [--mode dfs|pct|random] [--schedules <n>] [--steps <n> | --max-steps <n>]
            crucible replay  --schedule <path.json>
            crucible report  --schedule <path.json>
            """);
    }

    private static int ListCommand()
    {
        foreach (var name in ScenarioHost.RegistryView.Names)
            Console.WriteLine(name);
        return 0;
    }

    private static int RunCommand(string[] args)
    {
        var opts = ParseOptions(args);
        var workload = opts.GetRequired("workload");
        var seed = opts.GetInt("seed", 42);
        var nodes = opts.GetInt("nodes", 5);
        var steps = opts.GetSteps(2_000);
        var requireQuiescence = opts.GetBool("require-quiescence", false);

        if (nodes <= 0) throw new ArgumentException("--nodes must be positive");
        if (steps <= 0) throw new ArgumentException("--steps or --max-steps must be positive");

        var chaos = opts.GetString("chaos", "mild").ToLowerInvariant() switch
        {
            "adversarial" => ChaosProfile.Adversarial,
            _ => ChaosProfile.Mild
        };

        var options = new WorkloadOptions { NodeCount = nodes };
        var (runtime, invariants) = ScenarioHost.Build(workload, seed, options, chaos: chaos);
        var classification = runtime.Run(steps);
        var violations = runtime.CheckAll(invariants);
        Console.WriteLine(runtime.DumpState());

        if (violations.Count > 0 || runtime.Violations.Count > 0)
        {
            var all = violations.Concat(runtime.Violations).Distinct().ToList();
            var report = new CounterexampleReport
            {
                Workload = workload,
                Seed = seed,
                Steps = runtime.StepsExecuted,
                Violations = all,
                RuntimeDump = runtime.DumpState()
            };
            Console.WriteLine(report.Render());
            return 2;
        }

        if (classification == RunClassification.StepBoundExhausted && requireQuiescence)
        {
            Console.WriteLine($"INCONCLUSIVE workload={workload} seed={seed} steps={runtime.StepsExecuted} (step bound exhausted before quiescence)");
            return 4;
        }

        Console.WriteLine($"OK workload={workload} seed={seed} steps={runtime.StepsExecuted} (classification={classification})");
        return 0;
    }

    private static int ExploreCommand(string[] args)
    {
        var opts = ParseOptions(args);
        var workload = opts.GetRequired("workload");
        var seed = opts.GetInt("seed", 42);
        var nodes = opts.GetInt("nodes", 5);
        var steps = opts.GetSteps(5_000);
        var schedules = opts.GetInt("schedules", 20);
        var mode = opts.GetString("mode", opts.GetString("strategy", "pct")).ToLowerInvariant();
        var outDir = opts.GetString("out", "out");

        if (nodes <= 0) throw new ArgumentException("--nodes must be positive");
        if (steps <= 0) throw new ArgumentException("--steps or --max-steps must be positive");
        if (schedules <= 0) throw new ArgumentException("--schedules must be positive");

        var workloadOptions = new WorkloadOptions { NodeCount = nodes };
        var entry = ScenarioHost.RegistryView.Get(workload);
        var explorerOpts = new ExplorerOptions
        {
            MaxSteps = steps,
            MaxSchedules = schedules,
            ScheduleOutputDirectory = outDir
        };

        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);
        IReadOnlyList<ExplorationResult> results = mode switch
        {
            "dfs" => new DfsExplorer(explorerOpts).Explore(workload, seed, builder, workloadOptions, entry.Workload.GlobalInvariants),
            "random" => new RandomExplorer(explorerOpts).Run(workload, seed, schedules, builder, workloadOptions, entry.Workload.GlobalInvariants),
            _ => new PctExplorer(explorerOpts).RunMany(workload, seed, schedules, builder, workloadOptions, entry.Workload.GlobalInvariants)
        };

        foreach (var r in results)
            Console.WriteLine(r);

        if (results.Any(r => r.Classification == RunClassification.SafetyViolation || r.Violations.Count > 0))
            return 2;
        if (results.Any(r => r.Classification == RunClassification.ReplayDivergence))
            return 3;
        if (results.All(r => r.Classification == RunClassification.StepBoundExhausted))
            return 4;
        return 0;
    }

    private static int ReplayCommand(string[] args)
    {
        var opts = ParseOptions(args);
        var path = opts.GetRequired("schedule");
        var trace = ScheduleSerializer.FromFile(path);
        var entry = ScenarioHost.RegistryView.Get(trace.Workload);
        var options = trace.Options ?? new WorkloadOptions { NodeCount = 5 };
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);

        try
        {
            var result = new ReplayExplorer().Replay(trace, builder, options, entry.Workload.GlobalInvariants);
            Console.WriteLine(result);
            if (result.Trace is not null && !result.Success)
            {
                var report = new CounterexampleReport
                {
                    Workload = trace.Workload,
                    Seed = trace.Seed,
                    Steps = result.Steps,
                    Violations = result.Violations,
                    Trace = trace
                };
                Console.WriteLine(report.Render());
            }
            return result.Success ? 0 : 2;
        }
        catch (ReplayDivergenceException ex)
        {
            Console.Error.WriteLine($"REPLAY DIVERGENCE: {ex.Message}");
            return 3;
        }
    }

    private static int ReportCommand(string[] args)
    {
        var opts = ParseOptions(args);
        var path = opts.GetRequired("schedule");
        var trace = ScheduleSerializer.FromFile(path);
        Console.WriteLine(ScheduleSerializer.ToJson(trace));
        return 0;
    }

    private static OptionBag ParseOptions(string[] args)
    {
        var bag = new OptionBag();
        for (var i = 1; i < args.Length; i++)
        {
            if (!args[i].StartsWith("--", StringComparison.Ordinal))
                continue;
            var key = args[i][2..];
            var val = (i + 1 < args.Length && !args[i + 1].StartsWith("--", StringComparison.Ordinal))
                ? args[++i]
                : "true";
            bag.Set(key, val);
        }
        return bag;
    }

    private sealed class OptionBag
    {
        private readonly Dictionary<string, string> _values = new(StringComparer.OrdinalIgnoreCase);
        public void Set(string key, string value) => _values[key] = value;
        public string GetRequired(string key) =>
            _values.TryGetValue(key, out var v) ? v : throw new ArgumentException($"Missing --{key}");
        public string GetString(string key, string def) =>
            _values.TryGetValue(key, out var v) ? v : def;
        public int GetInt(string key, int def) =>
            _values.TryGetValue(key, out var v) && int.TryParse(v, out var n) ? n : def;
        public int GetSteps(int def)
        {
            if (_values.TryGetValue("max-steps", out var ms) && int.TryParse(ms, out var n1))
                return n1;
            if (_values.TryGetValue("steps", out var s) && int.TryParse(s, out var n2))
                return n2;
            return def;
        }
        public bool GetBool(string key, bool def = false) =>
            _values.TryGetValue(key, out var v)
                ? string.Equals(v, "true", StringComparison.OrdinalIgnoreCase) || v == "1"
                : def;
    }
}
