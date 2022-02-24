using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public interface ISyntaxNodeGenerator<in TNode> where TNode : Node
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, TNode node);
    }
}