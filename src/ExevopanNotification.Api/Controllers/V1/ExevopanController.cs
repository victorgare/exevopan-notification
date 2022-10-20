using ExevopanNotification.Api.Controllers.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.V1
{
    public class ExevopanController : BaseController
    {
        private readonly IExevoPanService _exevopanService;

        public ExevopanController(IExevoPanService exevopanService)
        {
            _exevopanService = exevopanService;
        }

        [HttpPost]
        public async Task<IActionResult> FindAndNotify()
        {
            await _exevopanService.FindAndNotify();
            return Accepted();
        }
    }
}
