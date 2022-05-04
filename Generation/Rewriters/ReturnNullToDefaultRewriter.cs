using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    public class TypeParamReturnNullRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var body = node.Body;

            IEnumerable<StatementSyntax> statements = Array.Empty<StatementSyntax>();
            foreach (var statement in body.Statements)
            {
                if (statement is ReturnStatementSyntax returnStatement 
                    && returnStatement.Expression is LiteralExpressionSyntax literalExpression 
                    && literalExpression.ToString().Equals("null"))
                {
                    var defaultExpression = SyntaxFactory.DefaultExpression(node.ReturnType);
                    statements = statements.Append(returnStatement.WithExpression(defaultExpression));
                }
                else
                {
                    statements = statements.Append(statement);
                }
            }
            
            body = body.WithStatements(SyntaxFactory.List(statements));
            return base.VisitMethodDeclaration(node.WithBody(body));
        }
    }
}