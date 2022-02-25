using Generation.Java.Nodes.Members;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
{
    public class MemberSyntaxNodeGenerator : ISyntaxNodeGenerator<Member>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Member node)
        {
            return node switch
            {
                FieldDeclaration fieldDeclaration => new FieldDeclarationSyntaxNodeGenerator().Generate(syntaxGenerator, fieldDeclaration),
                MethodDeclaration methodDeclaration => new MethodDeclarationSyntaxNodeGenerator().Generate(syntaxGenerator, methodDeclaration),
                _ => throw new System.NotImplementedException()
            };
        }
    }
}