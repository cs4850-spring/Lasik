using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class ReturnStatement : Statement
    {
        [JsonPropertyName("expression")] public Expression Expression { get; set; }
    }
}