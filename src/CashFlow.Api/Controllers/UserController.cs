using CashFlow.Application.UseCases.Security;
using CashFlow.Application.UseCases.Users.LoggedUser;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.SignIn;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // create controller to sign in user
    [HttpPost]
    [Route("login")]
    [ProducesResponseType<ResponseLoginUser>(StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Sign in user",
        Description = "Sign in user in the system",
        OperationId = "Login",
        Tags = ["Users"]
    )]
    public async Task<IActionResult> Login([FromBody] RequestLoginUser user, [FromServices] ILoginUserUseCase useCase)
    {
        var response = await useCase.Execute(user);

        return Ok(response);
    }

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

    [HttpGet]
    [Route("me")]
    [ProducesResponseType<ResponseLoggedUser>(StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get logged user",
        Description = "Get logged user in the system",
        OperationId = "LoggedUser",
        Tags = ["Users"]
    )]
    public async Task<IActionResult> Me([FromServices] ILoggedUserUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    // create controller to refresh token
    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType<ResponseLoginUser>(StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Refresh token",
        Description = "Refresh token of user in the system"
    )]
    public async Task<IActionResult> RefreshToken([FromServices] IUserRefreshTokenUseCase useCase, [FromBody] RequestRefreshTokenUser request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
