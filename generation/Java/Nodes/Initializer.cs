using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Initializer : Node
    {
        [JsonPropertyName("value")] public string Value { get; set; }
    }
}