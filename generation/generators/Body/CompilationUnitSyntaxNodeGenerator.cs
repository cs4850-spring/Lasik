using System.Linq;
using Generation.Generators.Types;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
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