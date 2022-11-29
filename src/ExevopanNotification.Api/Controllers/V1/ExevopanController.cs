using ExevopanNotification.Api.Controllers.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExevopanNotification.Api.Controllers.V1
{
    public class ExevopanController : BaseController
    {
        private readonly IExevoPanService _exevopanService;
        private readonly QueryConfig _queryConfig;

        public ExevopanController(IExevoPanService exevopanService, IOptions<ApplicationConfig> appConfig)
        {
            _exevopanService = exevopanService;
            _queryConfig = appConfig.Value.QueryConfig;
        }

        [HttpPost]
        public async Task<IActionResult> FindAndNotify()
        {
            await _exevopanService.FindAndNotify();
            return Accepted();
        }

        [HttpGet]
        public IActionResult GetConfig()
        {
            return Ok(_queryConfig);
        }
    }
}
