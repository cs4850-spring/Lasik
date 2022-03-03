using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Types
{
    public class ArrayJavaType : JavaType
    {
        [JsonPropertyName("componentType")] public JavaType ComponentType { get; set; }
        
        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }

        public override string Identifier()
        {
            throw new System.NotImplementedException();
        }
    }
}