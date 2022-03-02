using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class ThisExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<ThisExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ThisExpression node)
        {
            return syntaxGenerator.ThisExpression();
        }
    }
}