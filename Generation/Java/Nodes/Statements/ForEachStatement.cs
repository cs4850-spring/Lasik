using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class ForEachStatement : Statement
    {
        [JsonPropertyName("body")] public Statement Body { get; set; }
        [JsonPropertyName("iterable")] public Expression Iterable { get; set; }
        [JsonPropertyName("variable")] public VariableDeclarationExpression VariableDeclaration { get; set; }
    }
}