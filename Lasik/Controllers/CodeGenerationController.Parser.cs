using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Generation;
using Generation.Java.Nodes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lasik.Controllers
{
    public class Parser
    {
        private readonly ILogger<Parser> _logger;
        private readonly HttpClient _httpClient;

        private readonly Uri _parserUri;
        
        public Parser(ILogger<Parser> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _parserUri = new Uri(configuration["ParserURI"]);
        }

        public async Task<CompilationUnit?> Parse(string javaCode, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync(_parserUri, new StringContent(javaCode), cancellationToken);
            
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception(content);
            }
            return await response.Content.ReadFromJsonAsync<CompilationUnit>(cancellationToken: cancellationToken);
        }
    }
}