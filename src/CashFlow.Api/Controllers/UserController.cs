using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("register")]
        [ProducesResponseType<ResponseRegisteredUser>(StatusCodes.Status201Created)]
        [SwaggerOperation(
            Summary = "Register a new user",
            Description = "Register a new user in the system",
            OperationId = "Register",
            Tags = new[] { "Users" }
        )]
        public async Task<IActionResult> Register([FromBody] RequestRegisterUser user, [FromServices] IRegisterUserUseCase useCase)
        {
            var response = await useCase.Execute(user);

            return Created(nameof(Register), response);
        }
    }
}
