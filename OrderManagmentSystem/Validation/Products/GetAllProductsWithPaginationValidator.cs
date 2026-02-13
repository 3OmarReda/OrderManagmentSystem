using FluentValidation;
using ApplicationLayer.Dtos.Products;

public class GetAllProductsWithPaginationValidator : AbstractValidator<GetAllProductsWithPaginationDto>
{
    public GetAllProductsWithPaginationValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(50).WithMessage("PageSize cannot exceed 50.");
    }
}
