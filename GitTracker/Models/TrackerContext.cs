namespace GitTracker;

public class TrackerContext
{
    public int PushCounter { get; set; } = 1;
    public string CurrentRepo { get; set; } = "";
    public string CurrentBranch { get; set; } = "";
    
    public string? FlushPushes()
    {
        if (string.IsNullOrEmpty(CurrentRepo))
        {
            return null;
        }

        string result = $"    -Made {PushCounter} push(-es) in repo {CurrentRepo} -> {CurrentBranch}";
        CurrentRepo = string.Empty;
        CurrentBranch = string.Empty;
        PushCounter = 0;
        return result;
    }
}