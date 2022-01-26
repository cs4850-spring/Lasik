using System.Collections.Generic;

namespace generation.Java.Nodes
{
    public class ComplilationUnit : Node
    {
        public List<object> Imports { get; set; }
        public List<Type> Types { get; set; }

        public override IEnumerable<Node> Children()
        {
            return Types;
        }
    }
}