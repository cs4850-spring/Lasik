using generation.Java.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace generation.Java.Visitors.Roslyn
{
    public class VariableGenerator : NodeVisitor<Variable, VariableDeclarationSyntax>
    {
        public override VariableDeclarationSyntax Apply(Variable node)
        {
            var typeSyntax = new TypeGenerator().Apply(node.Type);

            
            return SyntaxFactory.VariableDeclaration(typeSyntax);
        }
    }
}