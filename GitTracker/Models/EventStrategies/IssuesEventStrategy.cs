namespace GitTracker.Models.EventStrategies;

public class IssuesEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "IssuesEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var issueAction = e.Payload.Action;
        var title = e.Payload.Issue?.Title;
        return $"    -Issue Action: {issueAction} - {title}";
    }
}