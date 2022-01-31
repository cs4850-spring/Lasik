using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Variable : Node
    {
        [JsonPropertyName("initializer")] public Initializer Initializer { get; set; }

        [JsonPropertyName("name")] public Name Name { get; set; }

        [JsonPropertyName("type")] public Type Type { get; set; }
    }
}