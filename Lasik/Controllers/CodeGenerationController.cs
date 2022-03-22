using System;
using System.Threading;
using System.Threading.Tasks;
using Generation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lasik.Controllers
{
    [ApiController]
    [Route("/parse")]
    public class CodeGenerationController : ControllerBase
    {
        private readonly ILogger<CodeGenerationController> _logger;
        private readonly Parser _parser;
        private readonly Generator _generator;

        public CodeGenerationController(ILogger<CodeGenerationController> logger, Parser parser, Generator generator)
        {
            _logger = logger;
            _parser = parser;
            _generator = generator;
        }

        [HttpPost]
        [Consumes("text/plain")]
        [Produces("text/plain")]
        [EnableCors]
        public async Task<string> Generate([FromBody] string javaCode, CancellationToken cancellationToken = default)
        {
            try
            {
                var javaAst = await _parser.Parse(javaCode, cancellationToken);

                if (javaAst is null) return ""; // TODO(Michael): Make this an error message
                return _generator.Generate(javaAst);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}