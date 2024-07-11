using AutoMapper;
using BP.AccountsMS.Business.Services.Contracts;
using BP.AccountsMS.Data.Entities;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using BP.Business.Common.Constants;
using GrpcContracts;

namespace BP.AccountsMS.Business.Services
{
    internal class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreateUserAccountResponse> CreateUserAccountAsync(CreateUserAccountRequest createUserAccountRequest)
        {
            ArgumentNullException.ThrowIfNull(createUserAccountRequest, nameof(createUserAccountRequest));

            var existingUser = await _unitOfWork.UserRepository.GetByEmailAsync(createUserAccountRequest.Email);

            if (existingUser != null)
            {
                return new CreateUserAccountResponse()
                {
                    IsSuccessfullyCreated = false,
                    ExceptionMessage = ExceptionMessages.UserAlreadyExists
                };
            }

            var userEntity = _mapper.Map<UserEntity>(createUserAccountRequest);
            await _unitOfWork.UserRepository.CreateAsync(userEntity);
            _unitOfWork.Commit();

            return new CreateUserAccountResponse()
            {
                IsSuccessfullyCreated = true
            };
        }
    }
}
