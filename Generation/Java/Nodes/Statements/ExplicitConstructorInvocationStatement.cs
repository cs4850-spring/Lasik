using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Converters;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class ExplicitConstructorInvocationStatement : Statement
    {
        [JsonPropertyName("arguments")] public List<Expression> Arguments { get; set; }
        [JsonPropertyName("isThis")] [JsonConverter(typeof(BoolConverter))] public bool IsThis { get; set; }
    }
}