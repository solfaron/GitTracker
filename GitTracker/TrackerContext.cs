namespace GitTracker;

public class TrackerContext
{
    public int PushCounter { get; set; } = 1;
    public string CurrentRepo { get; set; } = "";
    public string CurrentBranch { get; set; } = "";
}