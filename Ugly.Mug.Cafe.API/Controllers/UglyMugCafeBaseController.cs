using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ugly.Mug.Cafe.API.Service;

namespace Ugly.Mug.Cafe.API.Controllers
{
    [ServiceFilter(typeof(ModelValidationServiceFilter))]
    [Route("api"), ApiController, EnableCors("AllowAllOrigins")]
    public class UglyMugCafeBaseController : ControllerBase
    {
    }
}