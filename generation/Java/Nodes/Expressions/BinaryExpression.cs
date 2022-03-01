using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class BinaryExpression : Expression
    {
        [JsonPropertyName("operator")] public string Operator { get; set; }
        [JsonPropertyName("left")] public Expression Left { get; set; }
        [JsonPropertyName("right")] public Expression Right { get; set; }
    }
}