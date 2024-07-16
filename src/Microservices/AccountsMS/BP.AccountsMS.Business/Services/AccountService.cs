using AutoMapper;
using BP.AccountsMS.Business.Models.Account;
using BP.AccountsMS.Business.Services.Contracts;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.Business.Common.Services.Contracts;
using GrpcContracts;

namespace BP.AccountsMS.Business.Services
{
    internal class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<AccountViewModel> GetAccountAsync()
        {
            var currentUserAccount = await _unitOfWork.AccountRepository.GetByEmailAsync(_currentUserService.GetEmail())
                ?? throw new NotFoundException(ExceptionMessages.UserNotFound);

            var accountViewModel = _mapper.Map<AccountViewModel>(currentUserAccount);
            return accountViewModel;
        }

        public async Task<CreateUserAccountResponse> CreateAccountAsync(CreateUserAccountRequest createUserAccountRequest)
        {
            ArgumentNullException.ThrowIfNull(createUserAccountRequest, nameof(createUserAccountRequest));

            var existingUser = await _unitOfWork.AccountRepository.GetByEmailAsync(createUserAccountRequest.Email);

            if (existingUser != null)
            {
                return new CreateUserAccountResponse()
                {
                    IsSuccessfullyCreated = false,
                    ExceptionMessage = ExceptionMessages.UserAlreadyExists
                };
            }

            var accountEntity = _mapper.Map<AccountEntity>(createUserAccountRequest);
            await _unitOfWork.AccountRepository.CreateAsync(accountEntity);
            _unitOfWork.Commit();

            return new CreateUserAccountResponse()
            {
                IsSuccessfullyCreated = true
            };
        }
    }
}
