using Generation.Generators.Types;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
{
    public class ParameterSyntaxNodeGenerator : ISyntaxNodeGenerator<Parameter>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Parameter node)
        {
            var type = new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, node.JavaType);
                
            // Note(Michael): Parameters can never be in/out since these do not exist in java.
            // TODO(Michael): Implement initializers
            return syntaxGenerator.ParameterDeclaration(node.SimpleName.Identifier, type, null, RefKind.None);
        }
    }
}