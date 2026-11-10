namespace GitTracker.Models.EventStrategies;

public class PushEventStrategy: IEventStrategy
{
    public string TargetEventType { get; set; }=  "PushEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        
    }
}