namespace GitTracker.Models.EventStrategies;

public class WatchEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "WatchEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        return $"    -Starred {e.Repo.Name}";
    }
}