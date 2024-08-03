using AutoMapper;
using BP.AccountsMS.Business.Constants;
using BP.AccountsMS.Business.Services.Contracts;
using BP.AccountsMS.Business.Settings;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.Business.Common.Services.Contracts;
using BP.EmailSender.Models;
using BP.EmailSender.Services.Contracts;
using BP.Utils.Helpers;
using Microsoft.Extensions.Options;

namespace BP.AccountsMS.Business.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmailSendingService _emailSendingService;
        private readonly EmailVerificationSettings _emailVerificationSettings;

        public EmailService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IEmailSendingService emailSendingService,
            IOptions<EmailVerificationSettings> emailVerificationSettings)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _emailSendingService = emailSendingService ?? throw new ArgumentNullException(nameof(emailSendingService));
            _emailVerificationSettings = emailVerificationSettings?.Value ?? throw new ArgumentNullException(nameof(emailVerificationSettings));
        }

        public async Task RequestVerificationCodeAsync()
        {
            using (_unitOfWork)
            {
                var currentUserAccount = await _unitOfWork.AccountRepository.GetByEmailAsync(_currentUserService.GetEmail())
                    ?? throw new NotFoundException(ExceptionMessages.UserNotFound);

                await _unitOfWork.OneTimePasswordRepository.DeactivateAllAsync(currentUserAccount.Id);

                var oneTimePassword = PasswordHelper.GeneratePassword(_emailVerificationSettings.OneTimePasswordLength);

                var oneTimePasswordEntity = new OneTimePasswordEntity()
                {
                    Id = Guid.NewGuid(),
                    AccountId = currentUserAccount.Id,
                    Password = PasswordHelper.HashPassword(oneTimePassword, PasswordHelper.GenerateSalt()),
                    CreatedAtUtc = DateTime.UtcNow,
                    ExpiredAtUtc = DateTime.UtcNow.AddMinutes(_emailVerificationSettings.OneTimePasswordLifetimeMinutes),
                    IsActive = true
                };

                await _unitOfWork.OneTimePasswordRepository.CreateAsync(oneTimePasswordEntity);

                var message = new MessageModel()
                {
                    ReceiverEmail = currentUserAccount.Email,
                    Subject = MessageContent.VerifyEmailSubject,
                    Body = $"{MessageContent.VerifEmailBody}: {oneTimePassword}"
                };

                await _emailSendingService.SendMessageAsync(message);

                _unitOfWork.Commit();
            }
        }

        public async Task VerifyEmailAsync(string emailVerificationCode)
        {
            using (_unitOfWork)
            {
                var currentUserAccount = await _unitOfWork.AccountRepository.GetByEmailAsync(_currentUserService.GetEmail())
                    ?? throw new NotFoundException(ExceptionMessages.UserNotFound);

                var activeOneTimePassword = await _unitOfWork.OneTimePasswordRepository.GetActiveAsync(currentUserAccount.Id)
                    ?? throw new NotFoundException(ExceptionMessages.ActivePasswordNotFound);

                if (activeOneTimePassword.ExpiredAtUtc < DateTime.UtcNow)
                {
                    await _unitOfWork.OneTimePasswordRepository.DeactivateOneAsync(activeOneTimePassword.Id);
                    _unitOfWork.Commit();

                    throw new InvalidCredentialsException(ExceptionMessages.PasswordExpired);
                }

                var isPasswordValid = PasswordHelper.VerifyPassword(emailVerificationCode, activeOneTimePassword.Password);

                if (!isPasswordValid)
                {
                    throw new InvalidCredentialsException(ExceptionMessages.InvalidVerificationCode);
                }

                await _unitOfWork.OneTimePasswordRepository.DeactivateOneAsync(activeOneTimePassword.Id);

                currentUserAccount.IsEmailConfirmed = true;
                await _unitOfWork.AccountRepository.UpdateAsync(currentUserAccount);
                _unitOfWork.Commit();
            }
        }
    }
}
