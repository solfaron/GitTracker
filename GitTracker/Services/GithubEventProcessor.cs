using System.Text.Json;

namespace GitTracker;

public class GithubEventProcessor
{
    private readonly Dictionary<string, IEventStrategy> _strategies;
    public GithubEventProcessor()
    {
        var types = System.Reflection.Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => typeof(IEventStrategy).IsAssignableFrom(type) && !type.IsInterface);
        var insertEvents = new Dictionary<string, IEventStrategy>();
        foreach (var type in types)
        {
            if (Activator.CreateInstance(type) is IEventStrategy strategy)
            {
                insertEvents.Add(strategy.TargetEventType, strategy);
            }
        }

        _strategies = insertEvents;
    }
    
    public void ProcessAndDisplay(string output)
    {
        var events = JsonSerializer.Deserialize<List<GithubEvent>>(output);
        Console.WriteLine("output:\n");
        
        TrackerContext context =  new TrackerContext();
        
        foreach (var e in events)
        {
            if (e.Type != "PushEvent")
            {
                var pendingPushes = context.FlushPushes();
                if (pendingPushes is not null)
                {
                    Console.WriteLine(pendingPushes);
                }
            }

            if (_strategies.TryGetValue(e.Type, out var strategy))
            {
                string message = strategy.Process(e, context);
                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine(message);
                }
            }
        }
        var finalPushes = context.FlushPushes();
        if (finalPushes is not null)
        {
            Console.WriteLine(finalPushes);
        }
    }
}