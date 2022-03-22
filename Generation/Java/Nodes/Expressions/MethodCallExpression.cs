using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class MethodCallExpression : Expression
    {
        [JsonPropertyName("arguments")] public List<Expression> Arguments { get; set; }
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
        [JsonPropertyName("scope")] public Expression Scope { get; set; }
    }
}