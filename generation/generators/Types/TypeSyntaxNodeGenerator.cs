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
            //Note(Michael): Most nodes dont know what type they actually have, so we need to do a bit of
            //dynamic dispatch here.

            switch (node)
            {
                case ArrayJavaType arrayType:
                    return new ArrayTypeSyntaxNodeGenerator().Generate(syntaxGenerator, arrayType);
                case ClassOrInterfaceJavaType classOrInterfaceType:
                    return new ClassOrInterfaceSyntaxNodeGenerator().Generate(syntaxGenerator, classOrInterfaceType);
                case PrimitiveJavaType primativeType:
                    return new PrimativeTypeSyntaxNodeGenerator().Generate(syntaxGenerator, primativeType);
                case VarJavaType varType:
                    return new VarTypeSyntaxNodeGenerator().Generate(syntaxGenerator, varType);
                case VoidJavaType voidType:
                    return new VoidTypeSyntaxNodeGenerator().Generate(syntaxGenerator, voidType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(node));
            }
        }
    }
}