using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Java.Nodes.Statements
{
    public class ForStatement : Statement
    {
        [JsonPropertyName("body")] public BlockStatement Body { get; set; }
        [JsonPropertyName("compare")] public Expression Comparison { get; set; }
        [JsonPropertyName("update")] public List<Expression> Updates { get; set; }
        [JsonPropertyName("initialization")] public List<VariableDeclarationExpression> Initialization { get; set; }
    }
}