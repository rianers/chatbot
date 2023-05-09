using ChatBot.Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using StockBot.Service;

namespace ChatBot.API.Controllers
{
    public class CommandController : ControllerBase
    {
        [HttpGet]
        [Route("getbystock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string stock, [FromQuery] string chatRoomId, [FromServices] StockBotService stockBotService)
        {
            try
            {
                stockBotService.GetStock(stock, chatRoomId);
            }
            catch (Exception)
            {
                return BadRequest(ResultConstants.NO_MESSAGE_FOUND);
            }
            return Ok();
        }
    }
}
