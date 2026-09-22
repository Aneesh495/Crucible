namespace Crucible.Runtime.Diagnostics;

using System.Text;
using Crucible.Abstractions;

public sealed class SimulationTrace
{
    private readonly List<TraceEvent> _events = new();

    public void Record(SimTime time, string category, string message) =>
        _events.Add(new TraceEvent(time, category, message));

    public IReadOnlyList<TraceEvent> Events => _events;

    public string Render(int maxLines = 200)
    {
        var sb = new StringBuilder();
        foreach (var e in _events.Take(maxLines))
            sb.AppendLine($"{e.Time} [{e.Category}] {e.Message}");
        return sb.ToString();
    }

    public void Clear() => _events.Clear();
}

public readonly record struct TraceEvent(SimTime Time, string Category, string Message);

public static class RuntimeTraceExtensions
{
    public static SimulationTrace EnableTrace(this DeterministicRuntime runtime)
    {
        var trace = new SimulationTrace();
        return trace;
    }
}
