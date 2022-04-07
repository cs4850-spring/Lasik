using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class EnclosedExpression : Expression
    {
        [JsonPropertyName("inner")] public Expression Inner { get; set; }
    }
}