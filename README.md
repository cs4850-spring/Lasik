## Adding new Syntax to Lasik  

For the purposes of this document we are going to be adding support for the `for each` loop.

### Identify the systax

When adding new syntax we need to understand what the syntax is:

1. Is this an expression or a statement?

    The `for each` loop is a statement

2. Is this composed of other expressions or statements?

    Yes, it has the declaration inside `(String s : myStrings)` and the body between the `{ }`

3. Are there any pieces of this syntax that are not already implemented

    No, the generator can handle types `String`, variables `s`, and identifiers `myStrings`

4. If yes to 3, implement those first

### What is the syntax parsed as?

1. Write a simple test program utilizing the syntax, this doesnt need to be runnable just syntactially correct

```java
public class Test {

    public void Test() {
        String[] myStrings;

        for (String s : myStrings) {
            System.out.println(s)
        }
    }
}

```

2. Use https://www.postman.com/ and make a POST request to http://api.lasik.michaelepps.me:8080/parse.

This will give you the java ast you will need. Use https://jsonformatter.org/json-pretty-print if needed to pretty print it.

3. Find the syntax in the ast. Use an actual text editor to ctrl+f. Use the editor to hide objects to clearly idenfity what your looking for.

### Create a node class for the syntax

1. Create a new class in Generation/Java/Nodes. Utilize appropriate folder for the syntax.

    Since this is a statement we will create it under statements

2. Look at similar classes to see how to write

    For our `ForEachStatement` we end up with 

    ```csharp
    public class ForEachStatement : Statement
    {
        [JsonPropertyName("body")] public BlockStatement Body { get; set; }
        [JsonPropertyName("iterable")] public Expression Iterable { get; set; }
        [JsonPropertyName("variable")] public VariableDeclarationExpression VariableDeclaration { get; set; }
    }
    ```

3. Add node to appropriate converter

    For the `ForEachStatement` we add 

    ```csharp
                "com.github.javaparser.ast.stmt.ForEachStmt" =>
                    JsonSerializer.Deserialize<ForEachStatement>(ref reader, options),
    ```

    to `Generators/Java/Converters/StatementConverter.cs`

### Create the Generator for the syntax

1. Create a new class in Generation/Generation. Utilize appropriate folder for the syntax.

        Since this is a statement we will create it under statements

2. Look at similar classes to see how to write

    For our `ForEachStatementSyntaxNodeGenerator` we end up with
    
    ```csharp
    public class ForEachStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<ForEachStatement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ForEachStatement node)
        {
            throw new System.NotImplementedException();
        }
    }
    ```
3. Add class to base generator.

    For the `ForEachStatementSyntaxNodeGenerator` we add

    ```csharp
        ForEachStatement forEachStatement =>
        new ForEachStatementSyntaxNodeGenerator().Generate(syntaxGenerator, forEachStatement),
    ```

    to `Generators/generators/stmt/StatementSyntaxNodeGenerator.cs`


### Implement the generator

1. Idenfify the syntax in C# we need

    In C# this would be a `foreach` statement

2. Identify how to create this syntax. Either through SyntaxFactory or syntaxGenerator

3. Find simplest override to use

4. Create the needed parameters

This is hard to put into simple words, look at current implementations of other syntax features to see examples.

### Test the generator

1. Run app

2. Create a POST request to wherever app is being ran (https://localhost:80 usually) using postman.com and the java code from before.
