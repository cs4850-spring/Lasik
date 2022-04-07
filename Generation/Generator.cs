﻿using System;
using Generation.generators;
using Generation.Java.Nodes;
using Generation.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Options;

namespace Generation
{
    public class Generator
    {
        private readonly Workspace _workspace;
        private readonly SyntaxGenerator _syntaxGenerator;

        public Generator()
        {
            _workspace = MSBuildWorkspace.Create();
            _syntaxGenerator = SyntaxGenerator.GetGenerator(_workspace, LanguageNames.CSharp);
        }
        
        public string Generate(CompilationUnit javaAst)
        {
            
            // Java AST -> C# (Rosyln) AST
            // CompilationUnit -> CompilationUnitSyntax
            var cSharpAst = BodyGenerators.CompilationUnit(_syntaxGenerator, javaAst);
            cSharpAst = CleanupAST(cSharpAst);

            return cSharpAst.NormalizeWhitespace().ToFullString();
        }

        private SyntaxNode CleanupAST(SyntaxNode ast)
        {
            ast = new InvocationRewriter().Visit(ast);
            ast = new MethodTitleCaseRewriter().Visit(ast);
            ast = new FieldTitleCaseRewriter().Visit(ast);
            return Formatter.Format(ast, _workspace);
        }
    }
}