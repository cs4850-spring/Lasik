using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Expressions
{
    [JsonConverter(typeof(ExpressionConverter))]
    public abstract class Expression : Node
    {
        
        [JsonProperty("index")]
        public Index Index { get; set; }

        [JsonProperty("inner")]
        public Inner Inner { get; set; }

        [JsonProperty("annotations")]
        public List<object> Annotations { get; set; }

        [JsonProperty("modifiers")]
        public List<object> Modifiers { get; set; }

        [JsonProperty("variables")]
        public List<Variable> Variables { get; set; }

        [JsonProperty("arguments")]
        public List<Argument> Arguments { get; set; }

        [JsonProperty("scope")]
        public Scope Scope { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("target")]
        public Target Target { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }
    }
}