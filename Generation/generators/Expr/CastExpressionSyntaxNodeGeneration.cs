using Generation.Generators.Stmt;
using Generation.Generators.Types;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class CastExpressionSyntaxNodeGeneration : ISyntaxNodeGenerator<CastExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, CastExpression node)
        {
            var expression = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Expression);
            var type = new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, node.Type);
            return syntaxGenerator.CastExpression(type, expression);
        }
    }
}