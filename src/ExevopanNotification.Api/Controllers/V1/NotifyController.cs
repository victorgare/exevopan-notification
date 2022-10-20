using ExevopanNotification.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.V1
{
    [ApiVersion("1")]
    public class NotifyController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Index()
        {
            return Accepted();
        }
    }
}
