using ApplicationLayer.Dtos.User;
using FluentValidation;

namespace PresentationLayer.Validation.User
{
    public class LoginValidator : AbstractValidator<DtoLogin>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }

}
