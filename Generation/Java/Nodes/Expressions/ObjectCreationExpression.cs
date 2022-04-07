using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes.Expressions
{
    public class ObjectCreationExpression : Expression
    {
        [JsonPropertyName("arguments")] public List<Expression> Arguments { get; set; }
        [JsonPropertyName("type")] public JavaType Type { get; set; }
    }
}