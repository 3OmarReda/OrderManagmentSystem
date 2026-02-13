using ApplicationLayer.Dtos.Orders;
using FluentValidation;

namespace PresentationLayer.Validation
{
    public class AddOrderValidator : AbstractValidator<AddOrderDto>
    {
        public AddOrderValidator()
        {
            RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum()
                .WithMessage("Invalid payment method.");

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("Order must have at least one item.");

        }
    }
}
