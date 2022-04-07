using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;

namespace Generation.Java.Nodes.Statements
{
    public class ThrowStatement : Statement
    {
        [JsonPropertyName("expression")] public Expression Expression { get; set; }
    }
}