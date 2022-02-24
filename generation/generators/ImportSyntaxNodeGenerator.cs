using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class ImportSyntaxNodeGenerator : ISyntaxNodeGenerator<Import>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Import node)
        {
            var name = new NameSyntaxNodeGenerator().Generate(syntaxGenerator, node.Name);
            return syntaxGenerator.NamespaceImportDeclaration(name);
        }
    }
}