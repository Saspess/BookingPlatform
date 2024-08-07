using BP.BookingMS.Business.Models.Hotel;
using BP.BookingMS.Business.Services.Contracts;
using BP.Business.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.BookingMS.BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService ?? throw new ArgumentNullException(nameof(hotelService));
        }

        [HttpPost]
        [Authorize(Roles = AvailableRoles.Landlord)]
        public async Task<IActionResult> CreateHotelAsync([FromBody] HotelCreateModel hotelCreateModel)
        {
            var result = await _hotelService.CreateHotelAsync(hotelCreateModel);
            return Ok(result);
        }
    }
}
