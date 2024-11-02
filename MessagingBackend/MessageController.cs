using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;
using System;
using Microsoft.AspNetCore.Mvc;

namespace MessagingBackend {
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly WebSocketManager _webSocketManager;

        public MessageController(WebSocketManager webSocketManager)
        {
            _webSocketManager = webSocketManager;
        }

        public class MessageRequest
        {
            public required string Message { get; set; }
        }

        [HttpPost("broadcast")]
        public async Task<IActionResult> BroadcastMessage([FromBody] MessageRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message cannot be empty.");
            }

            await _webSocketManager.SendMessageToAll(request.Message);
            return Ok(new { message = "Message sent to all clients." });
        }
    }
}
