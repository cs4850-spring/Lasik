using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class NameSyntaxNodeGenerator : ISyntaxNodeGenerator<SimpleName>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, SimpleName node)
        {
            var identifier = syntaxGenerator.IdentifierName(node.Identifier);
            if (node.Qualifier == null) return identifier;
            
            var qualifier = new QualifierSyntaxNodeGenerator().Generate(syntaxGenerator, node.Qualifier);
            return syntaxGenerator.QualifiedName(qualifier, identifier);
        }
    }
}