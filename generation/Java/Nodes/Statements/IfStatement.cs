using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;
using Generation.Java.Nodes.Statements;

namespace Generation.Java.Nodes.Statements
{
    public class IfStatement : Statement
    {
        [JsonPropertyName("condition")] public Expression Condition { get; set; }
        [JsonPropertyName("elseStmt")] public Statement Else { get; set; }
        [JsonPropertyName("thenStmt")] public Statement Then { get; set; }
    }
}