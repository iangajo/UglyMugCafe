using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRHub.Domains;
using SignalRHub.Model.DataTransferObjects;

namespace SignalR.Hub.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<NotifyHub, IHubClient> _hubContext;
        public MessageController(IHubContext<NotifyHub, IHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Send([FromBody] MessageRequest message)
        {
            _hubContext.Clients.All.BroadcastMessage(message.Type, message.Payload);

            return Ok();
            
        }
    }
}