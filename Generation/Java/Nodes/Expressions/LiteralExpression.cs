using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class LiteralExpression : Expression
    {
        [JsonPropertyName("value")] public string Value { get; set; }
        
    }
}