namespace GitTracker.Models.EventStrategies;

public class PushEventStrategy : IEventStrategy
{
    public string TargetEventType { get; } = "PushEvent";

    public string Process(GithubEvent e, TrackerContext context)
    {
        string result = string.Empty;
        string repo = e.Repo.Name;
        string branch = e.Payload.Ref?.Replace("refs/heads/", "") ?? "";
        
        if (repo == context.CurrentRepo && branch == context.CurrentBranch)
        {
            context.PushCounter++;
        }
        else 
        {
            if (context.CurrentRepo != "")
            {   
                result = $"    -Made {context.PushCounter} push(-es) in repo {context.CurrentRepo} -> {context.CurrentBranch}";
            }
            context.CurrentRepo = repo;
            context.CurrentBranch = branch;
            context.PushCounter = 1;
        }

        return result;
    }
}