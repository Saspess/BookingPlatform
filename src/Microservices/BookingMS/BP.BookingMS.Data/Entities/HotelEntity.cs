using BP.BookingMS.Data.Entities.Contracts;

namespace BP.BookingMS.Data.Entities
{
    public class HotelEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber {  get; set; } 
        public int? Stars {  get; set; }

        public Guid LandlordId { get; set; }
        public virtual LandlordEntity Landlord { get; set; }
    }
}
