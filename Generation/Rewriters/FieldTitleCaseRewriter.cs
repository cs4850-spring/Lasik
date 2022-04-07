using System;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    public class FieldTitleCaseRewriter : CSharpSyntaxRewriter
    {
        public override SyntaxNode? VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            var identifier = node.Name.Identifier.ToString();
            var upper = Char.ToUpper(identifier[0]);
            identifier = identifier.Remove(0, 1);
            identifier = identifier.Insert(0, upper.ToString());
            node = node.WithName(node.Name.WithIdentifier(SyntaxFactory.Identifier(identifier)));
            return base.VisitMemberAccessExpression(node);
        }
    }
}