using Generation.Generators.Body;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class NameExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<NameExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, NameExpression node)
        {
            return new NameSyntaxNodeGenerator().Generate(syntaxGenerator, node.Name);
        }
    }
}