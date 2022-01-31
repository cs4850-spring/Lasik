using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Expressions
{
    [JsonConverter(typeof(ExpressionConverter))]
    public abstract class Expression : Node
    {
    }
}