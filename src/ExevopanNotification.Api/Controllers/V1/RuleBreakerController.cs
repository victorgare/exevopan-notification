using ExevopanNotification.Api.Controllers.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.V1
{
    public class RuleBreakerController : BaseController
    {
        private readonly IRuleBreakerService _ruleBreakerService;

        public RuleBreakerController(IRuleBreakerService ruleBreakerService)
        {
            _ruleBreakerService = ruleBreakerService;
        }

        [HttpPost]
        public async Task<IActionResult> FindAndNotify()
        {
            await _ruleBreakerService.FindAndNotify();
            return Accepted();
        }
    }
}
