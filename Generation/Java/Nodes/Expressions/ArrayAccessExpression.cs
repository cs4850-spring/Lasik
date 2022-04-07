using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class ArrayAccessExpression : Expression
    {
        [JsonPropertyName("index")] public Expression Index { get; set; }
        [JsonPropertyName("name")] public Expression Name { get; set; }
    }
}