namespace BP.BookingMS.Data.Entities
{
    public abstract class UserEntity
    {
        public Guid Id { get; set; }
        public string Email {  get; set; }
    }
}
