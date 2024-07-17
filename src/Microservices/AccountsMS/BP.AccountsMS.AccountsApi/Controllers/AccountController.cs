using BP.AccountsMS.Business.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.AccountsMS.AccountsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountAsync()
        {
            var result = await _accountService.GetAccountAsync();
            return Ok(result);
        }

        [HttpGet("email-verification-code")]
        public async Task<IActionResult> GetEmailVerificationCodeAsync()
        {
            await _emailService.RequestVerificationCodeAsync();
            return Ok();
        }

        [HttpPatch("email-verification-status")]
        public async Task<IActionResult> VerifyEmailAsync([FromQuery] string verificationCode)
        {
            await _emailService.VerifyEmailAsync(verificationCode);
            return Ok();
        }
    }
}
