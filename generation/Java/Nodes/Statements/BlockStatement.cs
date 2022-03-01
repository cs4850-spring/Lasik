using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Statements
{
    public class BlockStatement : Statement
    {
        [JsonPropertyName("statements")] public List<Statement> Statements { get; set; }
    }
}