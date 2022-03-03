using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class Initializer : Node
    {
        [JsonPropertyName("value")] public string Value { get; set; }
    }
}