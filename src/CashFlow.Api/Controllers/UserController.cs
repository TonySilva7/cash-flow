using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        [Route("register")]
        [ProducesResponseType<ResponseRegisteredUser>(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RequestRegisterUser user)
        {
            return Created("", new ResponseRegisteredUser());
        }
    }
}
