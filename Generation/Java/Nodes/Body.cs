using System.Collections.Generic;
using System.Text.Json.Serialization;
using generation.Java.Nodes.Statements;

namespace generation.Java.Nodes
{
    public class Body : Node
    {
        [JsonPropertyName("statements")] public List<Statement> Statements { get; set; }
    }
}