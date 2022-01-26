using generation.Java.Nodes;

namespace generation.Java.Visitors
{
    /**
     * A NodeVisitor is an object that can operate on a specific Node type in the AST
     */
    public abstract class NodeVisitor<TNode, TResult> where TNode : Node
    {
        public abstract TResult Apply(TNode node);
    }
}