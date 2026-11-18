namespace GitTracker.Models.EventStrategies;

public class IssueCommentEvent : IEventStrategy
{
    public string TargetEventType { get; } = "IssueCommentEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var issue = e.Payload.Issue?.Title;
        var comment = e.Payload.Comment?.Body;
        return $"    -Commented Issue: {issue} - {comment}";
    }
}