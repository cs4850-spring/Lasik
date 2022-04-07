using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace Generation.Java.Nodes.Types
{
    public class ArrayJavaType : JavaType
    {
        [JsonPropertyName("componentType")] public JavaType ComponentType { get; set; }
        
        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }

        public override string Identifier()
        {

            var depth = 0;

            var componentType = ComponentType;
            
            while (componentType is ArrayJavaType arrayJavaType)
            {
                componentType = arrayJavaType.ComponentType;
                depth++;
            }

            var commas = new String(',', depth).TrimEnd();
            
            return $"{componentType.Identifier()}[{commas}]";
            
        }

        private int Depth()
        {
            if (ComponentType is ArrayJavaType componentType)
            {
                return 1 + componentType.Depth();
            }

            return 1;
        }
    }
}