namespace GitTracker.Models.EventStrategies;

public class PullRequestEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "PullRequestEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var prAction =  e.Payload.Action;
        var prName = e.Payload.PullRequest?.Title;
        return $"    -Pull Request Action: {prAction} - {prName}";
    }
}