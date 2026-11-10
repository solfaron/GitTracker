using System.Text.Json.Serialization;

namespace GitTracker;

public class Repo
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}