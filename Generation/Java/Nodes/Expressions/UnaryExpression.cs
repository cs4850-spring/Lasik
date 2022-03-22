using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class UnaryExpression : Expression
    {
        [JsonPropertyName("operator")] public string Operator { get; set; }
        [JsonPropertyName("expression")] public Expression Expression { get; set; }
    }
}