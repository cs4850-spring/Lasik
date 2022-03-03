using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class NameExpression : Expression
    {
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
    }
}