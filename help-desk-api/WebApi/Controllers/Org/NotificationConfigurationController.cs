using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Org
{
    [ApiController]
    [Route("company")]
    [AllowAnonymous]
    public class NotificationConfigurationController : ControllerBase
    {
    }
}
