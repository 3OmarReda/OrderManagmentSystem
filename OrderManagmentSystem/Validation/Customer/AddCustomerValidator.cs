using FluentValidation;
using ApplicationLayer.Dtos.Customers;

public class AddCustomerValidator : AbstractValidator<AddCustomerDto>
{
    public AddCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
