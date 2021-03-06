using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes
{
    public class Variable : Node
    {
        [JsonPropertyName("initializer")] public Expression Initializer { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("type")] public JavaType JavaType { get; set; }
    }
}