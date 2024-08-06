using BP.BookingMS.Business.Services.Contracts;
using Grpc.Core;
using GrpcContracts;

namespace BP.BookingMS.BookingApi.Services
{
    public class GrpcPartyService : PartyService.PartyServiceBase
    {
        private readonly IPartyService _partyService;

        public GrpcPartyService(IPartyService partyService)
        {
            _partyService = partyService ?? throw new ArgumentNullException(nameof(partyService));
        }

        public override async Task<CreatePartyResponse> CreateParty(CreatePartyRequest request, ServerCallContext context)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var result = await _partyService.CreatePartyAsync(request);
            return result;
        }
    }
}
