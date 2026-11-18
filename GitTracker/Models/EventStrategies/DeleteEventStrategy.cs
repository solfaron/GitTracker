namespace GitTracker.Models.EventStrategies;

public class DeleteEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "DeleteEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var deleteRefType = e.Payload.RefType;
        var deleteRefName = e.Payload.Ref;
        return $"    -Deleted:  {deleteRefType} - {deleteRefName}";
    }
}