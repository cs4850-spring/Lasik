using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Converters;
using Generation.Java.Nodes.Members;

namespace Generation.Java.Nodes.Types
{
    public class ClassOrInterfaceJavaType : JavaType
    {
        [JsonPropertyName("extendedTypes")] public List<JavaType> ExtendedTypes { get; set; }

        [JsonPropertyName("implementedTypes")] public List<JavaType> ImplementedTypes { get; set; }

        [JsonPropertyName("isInterface")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsInterface { get; set; }

        [JsonPropertyName("typeParameters")] public List<TypeParameter> TypeParameters { get; set; }
        
        [JsonPropertyName("typeArguments")] public List<JavaType> TypeArguments { get; set; }

        [JsonPropertyName("members")] public List<Member> Members { get; set; }

        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("scope")] public JavaType Scope { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }
        
        public override string Identifier()
        {
            return SimpleName.Identifier;
        }
    }
}