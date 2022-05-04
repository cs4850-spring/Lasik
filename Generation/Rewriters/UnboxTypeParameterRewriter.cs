using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    public class UnboxTypeParameterRewriter : CSharpSyntaxRewriter
    {
        private SemanticModel _semanticModel;

        public UnboxTypeParameterRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }
        
        public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
        {
            var integers = _semanticModel.Compilation.GetTypesByMetadataName("MyCompilation.LinkedList");

            return base.VisitCompilationUnit(node);
        }
        

        private SyntaxToken Unbox(SyntaxToken identifier)
        {
            var str = identifier.ToString() switch
            {
                "Integer" => "int",
                _ => identifier.ToString()
            };

            return SyntaxFactory.Identifier(str);
        }
    }
}