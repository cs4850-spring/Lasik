using System.Collections.Generic;

namespace generation.Java.Nodes
{
    public class Type : Node
    {
        public List<object> ExtendedTypes { get; set; }
        public List<object> ImplementedTypes { get; set; }
        public string IsInterface { get; set; }
        public List<object> TypeParameters { get; set; }
        public List<Member> Members { get; set; }
        public List<object> Modifiers { get; set; }
        public Name name { get; set; }
        public List<object> annotations { get; set; }
    }
}