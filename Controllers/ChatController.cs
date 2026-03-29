using System.Threading.Tasks;
using doctors.DTO;
using doctors.services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace doctors.Controllers
{
    [Route("api/chat")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.Content) && string.IsNullOrWhiteSpace(model.FileUrl))
                return BadRequest(new { message = "Message content or file required." });

            var success = await _chatService.SendMessageAsync(model);
            if (!success)
                return BadRequest(new { message = "Cannot send message. Active relationship between doctor and patient not found." });

            return Ok(new { message = "Message sent successfully." });
        }

        [HttpGet("{doctorId}/{patientId}")]
        public async Task<IActionResult> GetMessages(int doctorId, int patientId)
        {
            var messages = await _chatService.GetMessagesAsync(doctorId, patientId);
            return Ok(messages);
        }
    }
}