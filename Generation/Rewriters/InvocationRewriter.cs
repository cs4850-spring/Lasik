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
            base.VisitInvocationExpression(node);

            if (node.Expression is not IdentifierNameSyntax identifier) return node;
            
            var newText = identifier.ToString() switch
            {
                "System.out.print" => "Console.Write",
                "System.out.println" => "Console.WriteLine",
                _ => identifier.ToString()
            };
            
            newText = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(newText);
                
            var newIdentifier = identifier.Update(SyntaxFactory.Identifier(newText));

            return node.WithExpression(newIdentifier);
        }
        
    }
}