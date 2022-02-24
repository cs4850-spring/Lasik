using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class CompilationUnit : Node
    {
        [JsonPropertyName("imports")] public List<Import> Imports { get; set; }

        [JsonPropertyName("types")] public List<ClassOrInterface> Types { get; set; }
    }
}