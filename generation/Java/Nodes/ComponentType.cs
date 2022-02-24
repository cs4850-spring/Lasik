sing System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class ComponentType : Node
    {

        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("annotations")]
        public List<object> Annotations { get; set; }

         [JsonProperty("componentType")]
        public ComponentType ComponentType { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; 
}