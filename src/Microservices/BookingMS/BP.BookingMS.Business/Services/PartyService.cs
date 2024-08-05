using AutoMapper;
using BP.BookingMS.Business.Services.Contracts;
using BP.BookingMS.Data.Entities;
using BP.BookingMS.Data.Repositories.Contracts;
using BP.Business.Common.Constants;
using GrpcContracts;

namespace BP.BookingMS.Business.Services
{
    internal class PartyService : IPartyService
    {
        private readonly IGenericRepository<LandlordEntity> _landlordRepository;
        private readonly IGenericRepository<TenantEntity> _tenantRepository;
        private readonly IMapper _mapper;

        public PartyService(
            IGenericRepository<LandlordEntity> landlordRepository,
            IGenericRepository<TenantEntity> tenantRepository,
            IMapper mapper)
        {
            _landlordRepository = landlordRepository ?? throw new ArgumentNullException(nameof(landlordRepository));
            _tenantRepository = tenantRepository ?? throw new ArgumentNullException(nameof(tenantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CreatePartyResponse> CreatePartyAsync(CreatePartyRequest createPartyRequest)
        {
            ArgumentNullException.ThrowIfNull(createPartyRequest, nameof(createPartyRequest));

            return createPartyRequest.Role switch
            {
                AvailableRoles.Landlord => await CreateLandlordAsync(createPartyRequest),
                AvailableRoles.Tenant => await CreateTenantAsync(createPartyRequest),
                _ => new CreatePartyResponse()
                {
                    IsSuccessfullyCreated = false,
                    ExceptionMessage = ExceptionMessages.UnsupportedRole
                }
            };
        }

        private async Task<CreatePartyResponse> CreateLandlordAsync(CreatePartyRequest createPartyRequest)
        {
            var existingLandlordEntity = await _landlordRepository.GetFirstOrDefaultAsync(e => e.Email == createPartyRequest.Email);

            if (existingLandlordEntity != null)
            {
                return new CreatePartyResponse()
                {
                    IsSuccessfullyCreated = false,
                    ExceptionMessage = ExceptionMessages.UserAlreadyExists
                };
            }

            var landlordEntity = _mapper.Map<LandlordEntity>(createPartyRequest);
            await _landlordRepository.CreateAsync(landlordEntity);

            return new CreatePartyResponse()
            {
                IsSuccessfullyCreated = true
            };
        }

        private async Task<CreatePartyResponse> CreateTenantAsync(CreatePartyRequest createPartyRequest)
        {
            var existingTenantEntity = await _tenantRepository.GetFirstOrDefaultAsync(e => e.Email == createPartyRequest.Email);

            if (existingTenantEntity != null)
            {
                return new CreatePartyResponse()
                {
                    IsSuccessfullyCreated = false,
                    ExceptionMessage = ExceptionMessages.UserAlreadyExists
                };
            }

            var tenantEntity = _mapper.Map<TenantEntity>(createPartyRequest);
            await _tenantRepository.CreateAsync(tenantEntity);

            return new CreatePartyResponse()
            {
                IsSuccessfullyCreated = true
            };
        }
    }
}
