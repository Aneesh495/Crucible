namespace Crucible.Concurrency;

public sealed class ExplorerStatistics
{
    public int SchedulesAttempted { get; set; }
    public int FailuresFound { get; set; }
    public long TotalSteps { get; set; }
    public double FailureRate => SchedulesAttempted == 0 ? 0 : (double)FailuresFound / SchedulesAttempted;

    public void Record(ExplorationResult result)
    {
        SchedulesAttempted++;
        TotalSteps += result.Steps;
        if (!result.Success) FailuresFound++;
    }

    public override string ToString() =>
        $"schedules={SchedulesAttempted} failures={FailuresFound} rate={FailureRate:P2} steps={TotalSteps}";
}

public static class ExplorerBatch
{
    public static ExplorerStatistics RunBatch(
        Func<int, ExplorationResult> run,
        int baseSeed,
        int count)
    {
        var stats = new ExplorerStatistics();
        for (var i = 0; i < count; i++)
            stats.Record(run(baseSeed + i));
        return stats;
    }
}
