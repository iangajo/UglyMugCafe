using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Ugly.Mug.Cafe.API.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api")]
    [ApiController]
    public class UglyMugCafeBaseController : ControllerBase
    {
    }
}