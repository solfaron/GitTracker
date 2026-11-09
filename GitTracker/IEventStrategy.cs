namespace GitTracker;

public interface IEventStrategy
{
    public string TargetEventType { get; }
    public void Process(GithubEvent e, TrackerContext context);
}