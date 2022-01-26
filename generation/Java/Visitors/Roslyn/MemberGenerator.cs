using System;
using generation.Java.Nodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace generation.Java.Visitors.Roslyn
{
    public class MemberGenerator : NodeVisitor<Member, MemberDeclarationSyntax>
    {
        public override MemberDeclarationSyntax Apply(Member node)
        {
            throw new NotImplementedException();
        }
    }
}