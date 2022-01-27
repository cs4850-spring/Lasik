using generation.Java.Nodes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace generation.Java.Visitors.Roslyn
{
    public class CompilationUnitGenerator : NodeVisitor<ComplilationUnit, CompilationUnitSyntax>
    {
        public override CompilationUnitSyntax Apply(ComplilationUnit node)
        {

            return null;
        }
    }
}