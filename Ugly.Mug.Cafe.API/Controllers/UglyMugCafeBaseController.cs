using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ugly.Mug.Cafe.API.Service;
using Ugly.Mug.Cafe.Domain.Enum;
using Ugly.Mug.Cafe.Domain.Response;

namespace Ugly.Mug.Cafe.API.Controllers
{
    [ServiceFilter(typeof(ModelValidationServiceFilter))]
    [Route("api"), ApiController, EnableCors("AllowAllOrigins")]
    public class UglyMugCafeBaseController : ControllerBase
    {

        protected IActionResult StatusCodeReturn<T>(BaseResponse<T> response)
        {
            switch (response.StatusCode)
            {
                case ResultType.Error:
                    return BadRequest(response);
                case ResultType.Created:
                    return Created(string.Empty, response);
                case ResultType.Success:
                    return Ok(response);
                default:
                    return BadRequest(response);
            }
        }
    }
}