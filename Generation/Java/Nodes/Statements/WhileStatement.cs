using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class WhileStatement : Statement
    {
        [JsonPropertyName("body")] public Statement Body { get; set; }
        [JsonPropertyName("condition")] public Expression Conditional { get; set; }
    }
}