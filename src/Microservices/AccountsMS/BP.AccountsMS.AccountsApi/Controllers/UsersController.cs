using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BP.AccountsMS.AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserEntity userEntity)
        {
            var result = await _unitOfWork.UserRepository.CreateUserAsync(userEntity);
            _unitOfWork.Commit();

            return Ok(result);
        }
    }
}
