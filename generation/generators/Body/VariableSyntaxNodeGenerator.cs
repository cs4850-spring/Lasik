using Generation.Generators.Expr;
using Generation.Generators.Types;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
{
    public class VariableSyntaxNodeGenerator : ISyntaxNodeGenerator<Variable>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Variable node)
        {
            var type = new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, node.JavaType);
            var initializer = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Initializer);
            return syntaxGenerator.LocalDeclarationStatement(type, node.SimpleName.Identifier, initializer);
        }
    }
}