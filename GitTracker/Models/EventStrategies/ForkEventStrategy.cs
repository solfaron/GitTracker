namespace GitTracker.Models.EventStrategies;

public class ForkEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "ForkEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var source = e.Payload.Forkee?.FullName;
        return $"    -Forked {source} repo";
    }
}