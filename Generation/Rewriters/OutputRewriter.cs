using System;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;

namespace Generation.Rewriters
{
    public class InvocationRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
        {

            if (node.Expression is not IdentifierNameSyntax identifier) return node;
            
            var newText = identifier.ToString() switch
            {
                "System.out.print" => "Console.Write",
                "System.out.println" => "Console.WriteLine",
                _ => identifier.ToString()
            };
            
                
            var newIdentifier = identifier.Update(SyntaxFactory.Identifier(newText));

            node = node.WithExpression(newIdentifier);
            return base.VisitInvocationExpression(node);
        }
        
    }
}