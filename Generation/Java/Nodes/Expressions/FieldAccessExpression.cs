using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class FieldAccessExpression : Expression
    {
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
        [JsonPropertyName("scope")] public Expression Scope { get; set; }
    }
}