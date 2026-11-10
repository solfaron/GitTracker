using System.Text.Json.Serialization;

namespace GitTracker;

public class PullRequest
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("number")]
    public int? Number { get; set; }
}