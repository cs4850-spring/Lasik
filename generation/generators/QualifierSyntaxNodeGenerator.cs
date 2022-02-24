using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class QualifierSyntaxNodeGenerator : ISyntaxNodeGenerator<Qualifier>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Qualifier node)
        {
            return syntaxGenerator.IdentifierName(node.Identifier);
        }
    }
}