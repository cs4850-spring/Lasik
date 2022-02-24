using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Types
{
    public class PrimitiveJavaType : JavaType
    {
        [JsonPropertyName("type")] public string Type { get; set; }
        
        public override string Identifier()
        {
            // Primitives are always uppercase (INT, FLOAT, STRING, etc)
            return Type.ToLower();
        }
    }
}