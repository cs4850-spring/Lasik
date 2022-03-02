using System.Linq;
using Generation.Generators.Body;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class VariableDeclarationExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<VariableDeclarationExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, VariableDeclarationExpression node)
        {
            // Modifers on these declarations dont exist in C#, so we ignore them.

            var variable = node?.Variables?.First();
            var type = SyntaxFactory.ParseTypeName(variable.JavaType.Identifier());
            var identifier = SyntaxFactory.Identifier(variable.SimpleName.Identifier);
            var designation = SyntaxFactory.SingleVariableDesignation(identifier);
            var declarationExpression = SyntaxFactory.DeclarationExpression(type, designation);

            var initializer = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, variable.Initializer);
            return SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, declarationExpression, initializer as ExpressionSyntax);
        }

    }
}