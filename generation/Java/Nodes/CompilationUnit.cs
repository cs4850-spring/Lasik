using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class CompilationUnit : Node
    {
        [JsonPropertyName("imports")] public List<Import> Imports { get; set; }

        [JsonPropertyName("types")] public List<Type> Types { get; set; }
    }
}