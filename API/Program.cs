using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



//Generate([FromBody] javaCode) {
    //create http client
    //create request (javaCode as body)
    //send request to spring server
    //read response
    
    //call ast deserializer
    //call C# generator
    //return C# text
//}


// Create endpoints
// Read body from body (http) -> create request to spring server (return json ast)
// Deserialize the json ast -> Java ast nodes
// Pass ast object to generator
// return C# text as response

app.Run();