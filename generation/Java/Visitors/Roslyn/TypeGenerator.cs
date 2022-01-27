using generation.Java.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace generation.Java.Visitors.Roslyn
{
    public class TypeGenerator : NodeVisitor<Type, TypeSyntax>
    {
        public override TypeSyntax Apply(Type node)
        {
            return null;
        }
    }
}