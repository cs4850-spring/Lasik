using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class ExpressionStatement : Statement
    {
        [JsonPropertyName("expression")] public Expression Expression { get; set; }
        
    }
}