﻿using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class CompilationUnitSyntaxNodeGenerator : ISyntaxNodeGenerator<CompilationUnit>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, CompilationUnit node)
        {
     
            var imports =
                node.Imports.Select(import => new ImportSyntaxNodeGenerator().Generate(syntaxGenerator, import));

            var types = node.Types
                .Select(type => new ClassOrInterfaceSyntaxNodeGenerator().Generate(syntaxGenerator, type));

            var declarations = imports.Concat(types);
            return syntaxGenerator.CompilationUnit(declarations);
        }
    }

    
}