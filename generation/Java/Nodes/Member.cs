using System.Collections.Generic;

namespace generation.Java.Nodes
{
    public class Member : Node
    {
        public List<object> modifiers { get; set; }
        public List<Variable> variables { get; set; }
        public List<object> annotations { get; set; }
    }
}