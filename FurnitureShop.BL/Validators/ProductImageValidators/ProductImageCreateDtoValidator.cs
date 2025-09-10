using FluentValidation;
using FurnitureShop.BL.Dtos.ProductImageDto;

namespace FurnitureShop.BL.Validators.ProductImageValidators;

public class ProductImageCreateDtoValidator : AbstractValidator<ProductImageCreateDto>
{
    public ProductImageCreateDtoValidator()
    {
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image file is required");

        RuleFor(x => x.AltText)
            .NotEmpty().WithMessage("AltText is required")
            .MaximumLength(200).WithMessage("AltText cannot exceed 200 characters");

        RuleFor(x => x)
            .Must(x => !(x.IsPrimary && x.IsSecondary))
            .WithMessage("An image cannot be both primary and secondary at the same time.");
    }
}