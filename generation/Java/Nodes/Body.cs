using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Statements;

namespace Generation.Java.Nodes
{
    public class Body : Node
    {
        [JsonPropertyName("statements")] public List<Statement> Statements { get; set; }
    }
}