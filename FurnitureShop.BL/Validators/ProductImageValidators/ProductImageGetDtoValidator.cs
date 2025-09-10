using FluentValidation;
using FurnitureShop.BL.Dtos.ProductImageDto;

namespace FurnitureShop.BL.Validators.ProductImageValidators;

public class ProductImageGetDtoValidator : AbstractValidator<ProductImageGetDto>
{
    public ProductImageGetDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("ProductId must be greater than 0");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required")
            .MaximumLength(500).WithMessage("ImageUrl cannot exceed 500 characters");

        RuleFor(x => x.AltText)
            .MaximumLength(200).WithMessage("AltText cannot exceed 200 characters");

        RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("CreatedDate is required");

        RuleFor(x => x.UpdatedTime)
            .NotEmpty().WithMessage("UpdatedTime is required");
    }
}