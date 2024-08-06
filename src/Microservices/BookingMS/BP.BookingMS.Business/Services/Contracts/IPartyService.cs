using GrpcContracts;

namespace BP.BookingMS.Business.Services.Contracts
{
    public interface IPartyService
    {
        Task<CreatePartyResponse> CreatePartyAsync(CreatePartyRequest createPartyRequest);
    }
}
