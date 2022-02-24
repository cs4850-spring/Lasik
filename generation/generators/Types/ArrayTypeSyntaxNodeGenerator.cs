using Generation.Generators.Types;
using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Types
{
    public class ArrayTypeSyntaxNodeGenerator : ISyntaxNodeGenerator<ArrayJavaType>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ArrayJavaType node)
        {
            var component = new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, node.ComponentType);

            return syntaxGenerator.ArrayTypeExpression(component);
        }
    }
}