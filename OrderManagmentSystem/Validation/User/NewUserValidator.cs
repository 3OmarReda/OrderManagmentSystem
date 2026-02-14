using ApplicationLayer.Dtos.User;
using FluentValidation;

public class NewUserValidator : AbstractValidator<RegisterDto>
{
    public NewUserValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
    }
}

