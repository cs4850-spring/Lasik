using System.Globalization;
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
            identifier = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(identifier);
            return node.WithIdentifier(SyntaxFactory.Identifier(identifier));
        }
    }
}