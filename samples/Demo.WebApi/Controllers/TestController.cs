using Demo.WebApi.Config;
using WWB.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IStorageProviderFactory _storageProviderFactory;
        private readonly IOptions<ConfigModel> _options;

        public TestController(ILogger<TestController> logger, IStorageProviderFactory storageProviderFactory, IOptions<ConfigModel> options)
        {
            _logger = logger;
            _storageProviderFactory = storageProviderFactory;
            _options = options;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var provider = _storageProviderFactory.Create(_options.Value.GetCfg());

            return Ok();
        }
    }
}
