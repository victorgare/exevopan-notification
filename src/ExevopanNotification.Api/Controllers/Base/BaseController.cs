using Microsoft.AspNetCore.Mvc;

namespace ExevopanNotification.Api.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
    }
}
