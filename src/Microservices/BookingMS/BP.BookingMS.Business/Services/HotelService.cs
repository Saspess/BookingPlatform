using AutoMapper;
using BP.BookingMS.Business.Models.Hotel;
using BP.BookingMS.Business.Services.Contracts;
using BP.BookingMS.Data.Entities;
using BP.BookingMS.Data.Repositories.Contracts;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.Business.Common.Services.Contracts;

namespace BP.BookingMS.Business.Services
{
    internal class HotelService : IHotelService
    {
        private readonly IGenericRepository<HotelEntity> _hotelRepository;
        private readonly IGenericRepository<LandlordEntity> _landlordRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public HotelService(
            IGenericRepository<HotelEntity> hotelRepository,
            IGenericRepository<LandlordEntity> landlordRepository,
            IMapper mapper, 
            ICurrentUserService currentUserService)
        {
            _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
            _landlordRepository = landlordRepository ?? throw new ArgumentNullException(nameof(landlordRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
        }

        public async Task<Guid> CreateHotelAsync(HotelCreateModel hotelCreateModel)
        {
            ArgumentNullException.ThrowIfNull(hotelCreateModel, nameof(hotelCreateModel));

            var existingLandlordEntity = await _landlordRepository.GetFirstOrDefaultAsync(e => e.Email == _currentUserService.GetEmail())
                ?? throw new NotFoundException(ExceptionMessages.LandlordNotFound);

            var hotelEntiy = _mapper.Map<HotelEntity>(hotelCreateModel);
            hotelEntiy.LandlordId = existingLandlordEntity.Id;
            await _hotelRepository.CreateAsync(hotelEntiy);

            return hotelEntiy.Id;
        }
    }
}
