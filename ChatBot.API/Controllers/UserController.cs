using ChatBot.API.Inputs;
using ChatBot.Application.UseCases.Commands;
using ChatBot.Application.UseCases.Queries;
using ChatBot.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("createuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserInput userInput, [FromServices] AddUserUseCase addUser)
        {
            string userId;
            try
            {
                userId = await addUser.Execute(userInput.Email, userInput.Password);
            }
            catch (Exception)
            {
                return BadRequest(ResultConstants.ERROR_PROCESSING_REQUEST);
            }
            return Ok(userId);
        }

        [HttpPost]
        [Route("checkuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserInput userInput, [FromServices] GetUserUseCase checkUser)
        {
            string userId;
            try
            {
                userId = await checkUser.Execute(userInput.Email, userInput.Password);
            }
            catch (Exception)
            {
                return BadRequest(ResultConstants.ERROR_PROCESSING_REQUEST);
            }
            return Ok(userId);
        }
    }
}
