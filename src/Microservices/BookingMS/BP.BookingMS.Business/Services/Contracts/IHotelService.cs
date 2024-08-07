using BP.BookingMS.Business.Models.Hotel;

namespace BP.BookingMS.Business.Services.Contracts
{
    public interface IHotelService
    {
        Task<Guid> CreateHotelAsync(HotelCreateModel hotelCreateModel);
    }
}
