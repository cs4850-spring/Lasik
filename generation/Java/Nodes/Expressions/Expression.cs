using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Expressions
{
    [JsonConverter(typeof(ExpressionConverter))]
    public abstract class Expression : Node
    {
    }
}