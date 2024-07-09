using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Business.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BP.IdentityMS.IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("token")]
        public async Task<IActionResult> LoginAsync([FromQuery] UserLoginQuery userLoginQuery)
        {
            var result = await _mediator.Send(userLoginQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterCommand userRegisterCommand)
        {
            var result = await _mediator.Send(userRegisterCommand);
            return Ok(result);
        }
    }
}
