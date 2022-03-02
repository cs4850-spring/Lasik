using System;
using System.Linq;
using Generation.Generators.Body;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class MethodCallExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<MethodCallExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, MethodCallExpression node)
        {
            var arguments = node?.Arguments?.Select(expression =>
                new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, expression));
            
            if (node.Scope == null)
            {
                var name = new NameSyntaxNodeGenerator().Generate(syntaxGenerator, node.Name);

                return syntaxGenerator.InvocationExpression(name, arguments);
            }
            
            //NOTE(MICHAEL): This is pretty hacky, but it gets around the differences in syntax trees here.
            var scope = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Scope);
            node.Name.Qualifier = new Qualifier {Identifier = CollapseScope((scope as ExpressionSyntax)!)};
            node.Name.Identifier = node.Name.Identifier.Replace("@", "");
            node.Name.Qualifier.Identifier = node.Name.Qualifier.Identifier.Replace("@", "");
            var scopedName = new NameSyntaxNodeGenerator().Generate(syntaxGenerator, node.Name);
            
            return syntaxGenerator.InvocationExpression(scopedName, arguments);
        }
        private static String CollapseScope(ExpressionSyntax scope)
        {
            return scope switch
            {
                MemberAccessExpressionSyntax memberAccess => $"{CollapseScope(memberAccess.Expression)}.{memberAccess.Name}",
                ThisExpressionSyntax thisExpression => "this",
                IdentifierNameSyntax identifierName => $"{identifierName.Identifier.ValueText}",
                null => "",
                _ => throw new ArgumentOutOfRangeException(nameof(scope), scope, null)
            };
        }
    }
}