namespace BP.BookingMS.Business.Models.Hotel
{
    public class HotelCreateModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int? Stars { get; set; }
    }
}
