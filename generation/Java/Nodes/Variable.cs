using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class Variable : Node
    {
        [JsonPropertyName("initializer")] public Initializer Initializer { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("type")] public ClassOrInterface Type { get; set; }
    }
}