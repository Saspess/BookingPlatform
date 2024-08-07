using BP.BookingMS.Business.Models.Hotel;
using FluentValidation;

namespace BP.BookingMS.Business.Validators.Hotel
{
    internal class HotelCreateModelValidator : AbstractValidator<HotelCreateModel>
    {
        public HotelCreateModelValidator()
        {
            RuleFor(m => m.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.City)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.Street)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(m => m.BuildingNumber)
                .GreaterThan(0);

            RuleFor(m => m.Stars)
                .GreaterThan(0)
                .LessThanOrEqualTo(5);
        }
    }
}
