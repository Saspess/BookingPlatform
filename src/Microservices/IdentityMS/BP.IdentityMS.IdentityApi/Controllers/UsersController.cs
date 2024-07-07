using BP.IdentityMS.Business.Commands.User;
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

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterCommand userRegisterCommand)
        {
            var result = await _mediator.Send(userRegisterCommand);
            return Ok(result);
        }
    }
}
