namespace GitTracker.Models.EventStrategies;

public class CommitCommentEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "CommitCommentEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var commitComment = e.Payload.Comment?.Body;
        var commentAction =  e.Payload.Action;
        return $"    -Commented commit : {commitComment} - {commentAction}";
    }
}