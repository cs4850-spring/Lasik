using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class AssignExpression : Expression
    {
        [JsonPropertyName("operator")] public string Operator { get; set; }
        [JsonPropertyName("target")] public Expression Target { get; set; }
        [JsonPropertyName("value")] public Expression Value { get; set; }
    }
}