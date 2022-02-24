using System;
using Generation.Generators;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation
{
    public class Generator
    {
        private readonly SyntaxGenerator _syntaxGenerator;

        public Generator()
        {
            _syntaxGenerator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
        }
        
        public string Generate(CompilationUnit javaAst)
        {
            
            // Java AST -> C# (Rosyln) AST
            // CompilationUnit -> CompilationUnitSyntax
            CleanupAST(javaAst);
            var cSharpAst = new CompilationUnitSyntaxNodeGenerator().Generate(_syntaxGenerator, javaAst);

            return cSharpAst.NormalizeWhitespace().ToFullString();
        }

        private void CleanupAST(CompilationUnit javaAst)
        {
            
        }
    }
}