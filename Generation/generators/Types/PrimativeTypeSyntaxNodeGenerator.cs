using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Types
{
    public class PrimativeTypeSyntaxNodeGenerator : ISyntaxNodeGenerator<PrimitiveJavaType>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, PrimitiveJavaType node)
        {
            return SyntaxFactory.ParseTypeName(node.Identifier());
        }
    }
}