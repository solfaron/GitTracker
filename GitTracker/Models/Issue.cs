using System.Text.Json.Serialization;

namespace GitTracker;

public class Issue
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
}