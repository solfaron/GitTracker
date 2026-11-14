namespace GitTracker;

public interface IEventStrategy
{
    public string TargetEventType {get;}
    public string Process(GithubEvent e, TrackerContext context);
}