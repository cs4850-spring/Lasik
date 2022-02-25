using System;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Types
{
    public class TypeSyntaxNodeGenerator : ISyntaxNodeGenerator<JavaType>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, JavaType node)
        {
            return node switch
            {
                ArrayJavaType arrayType => new ArrayTypeSyntaxNodeGenerator().Generate(syntaxGenerator, arrayType),
                ClassOrInterfaceJavaType classOrInterfaceType => 
                    new ClassOrInterfaceSyntaxNodeGenerator().Generate(syntaxGenerator, classOrInterfaceType),
                PrimitiveJavaType primativeType => 
                    new PrimativeTypeSyntaxNodeGenerator().Generate(syntaxGenerator, primativeType),
                VarJavaType varType => new VarTypeSyntaxNodeGenerator().Generate(syntaxGenerator, varType),
                VoidJavaType voidType => new VoidTypeSyntaxNodeGenerator().Generate(syntaxGenerator, voidType),
                _ => throw new ArgumentOutOfRangeException(nameof(node))
            };
        }
    }
}