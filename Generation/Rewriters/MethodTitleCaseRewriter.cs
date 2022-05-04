using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    /**
     * Rewrites all <see cref="MethodDeclarationSyntax"></see> nodes to be Title-Cased. 
     */
    public class MethodTitleCaseRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var identifier = node.Identifier.Text;
            var upper = Char.ToUpper(identifier[0]);

            identifier = identifier.Remove(0, 1);
            identifier = identifier.Insert(0, upper.ToString());
            node = node.WithIdentifier(SyntaxFactory.Identifier(identifier));
            return base.VisitMethodDeclaration(node);
        }

        

        public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
        { 
            if (node.Expression is not IdentifierNameSyntax identifierSyntax) return node;


            var identifier = identifierSyntax.Identifier.ToString();
            var dotIndicies = IndexOfAll(identifier,".");

            if (!dotIndicies.Any())
            {
                var index = 0;
                var upper = Char.ToUpper(identifier[index]);

                identifier = identifier.Remove(index, 1);
                identifier = identifier.Insert(index, upper.ToString());
                var newIdentifier = identifierSyntax.Update(SyntaxFactory.Identifier(identifier));
                node = node.WithExpression(newIdentifier);
                return base.VisitInvocationExpression(node);
            }

            foreach (var dotIndex in dotIndicies)
            {
                var index = dotIndex + 1;
                var upper = Char.ToUpper(identifier[index]);

                identifier = identifier.Remove(index, 1);
                identifier = identifier.Insert(index, upper.ToString());
                var newIdentifier = identifierSyntax.Update(SyntaxFactory.Identifier(identifier));

                node = node.WithExpression(newIdentifier);
            }

            return base.VisitInvocationExpression(node);
        }
        
        public static IEnumerable<int> IndexOfAll(string sourceString, string subString)
        {
            subString = Regex.Escape(subString);
            foreach (Match match in Regex.Matches(sourceString, subString))
            {
                yield return match.Index;
            }        
        }
    }
}