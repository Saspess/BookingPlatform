using BP.IdentityMS.Business.Queries.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BP.IdentityMS.IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableRolesAsync()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);
        }
    }
}
