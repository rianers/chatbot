using ChatBot.API.Inputs;
using ChatBot.Application.UseCases.Commands;
using ChatBot.Application.UseCases.Queries;
using ChatBot.Domain.Constants;
using ChatBot.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
    public class MessageController : ControllerBase
    {
        [HttpPost]
        [Route("addmessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] MessageInput messageInput, [FromServices] AddMessageUseCase addMessage)
        {
            Message message;
            try
            {
                message = await addMessage.Execute(messageInput.UserId, messageInput.Text, messageInput.ChatRoomId);
            }
            catch (Exception)
            {
                return BadRequest(ResultConstants.ERROR_PROCESSING_REQUEST);
            }
            return Ok(message);
        }

        [HttpGet]
        [Route("getmessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string chatroomId, [FromServices] GetMessagesUseCase getMessages)
        {
            IEnumerable<object> message;
            try
            {
                message = await getMessages.Execute(chatroomId);
            }
            catch (Exception)
            {
                return BadRequest(ResultConstants.NO_MESSAGE_FOUND);
            }
            return Ok(message);
        }
    }
}
