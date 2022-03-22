using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes.Expressions
{
    public class CastExpression : Expression
    {
        [JsonPropertyName("expression")] public Expression Expression { get; set; }
        [JsonPropertyName("type")] public JavaType Type { get; set; }
    }
}