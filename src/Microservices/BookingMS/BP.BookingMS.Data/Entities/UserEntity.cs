using BP.BookingMS.Data.Entities.Contracts;

namespace BP.BookingMS.Data.Entities
{
    public abstract class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Email {  get; set; }
    }
}
