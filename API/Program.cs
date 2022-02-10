using generation;
using Microsoft.AspNetCore.Builder;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/generate", ([FromBody] string javaCode) => {

    string _url = "https://lasik.michaelepps.me:8080/parse ";
    // create http client
    HttpClient httpClient = new HttpClient();
    // create request(javaCode as body)
    var request = new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri(_url),
        Content = new StringContent(javaCode),
    };
    // send request to spring server
    var response = httpClient.SendAsync(request).Result;
    // read response
    var responseBody = response.Content.ReadAsStringAsync().Result;
    // call ast deserializer
    var cSharpObj = JsonSerializer.Deserialize<Object>(responseBody);
    // call C# generator
    string cSharpStr = new Generator().generator(cSharpObj);
    // return C# text
    return cSharpStr;

});



app.Run();



