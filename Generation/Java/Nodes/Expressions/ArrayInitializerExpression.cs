using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    public class ArrayInitializerExpression : Expression
    {
        [JsonPropertyName("values")] public List<Expression> Values { get; set; }
    }
}