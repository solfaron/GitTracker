namespace GitTracker.Models.EventStrategies;

public class CreateEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "CreateEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        var reftype = e.Payload.RefType;
        var refname = e.Payload.Ref;
        return $"    -Created a new {reftype} - '{refname}'";
    }
}