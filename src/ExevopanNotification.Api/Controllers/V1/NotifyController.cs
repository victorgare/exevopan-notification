using ExevopanNotification.Api.Controllers.Base;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.V1
{
    public class NotifyController : BaseController
    {
        private readonly INotifyService _notifyService;

        public NotifyController(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        [HttpGet("telegram/ping")]
        public IActionResult Ping()
        {
            _notifyService.NotifyTelegram($"Ping - {DateTime.Now}");
            return Ok("Pong");
        }
    }
}
