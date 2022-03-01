using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes.Expressions
{
    [JsonConverter(typeof(ExpressionConverter))]
    public abstract class Expression : Node
    {
    }
}