using FluentValidation;
using FurnitureShop.BL.Dtos.ProductImageDto;

namespace FurnitureShop.BL.Validators.ProductImageValidators;

public class ProductImageUpdateDtoValidator :  AbstractValidator<ProductImageUpdateDto>
{
    public ProductImageUpdateDtoValidator()
    {
        RuleFor(x => x.AltText)
            .MaximumLength(200).WithMessage("AltText cannot exceed 200 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.AltText));

        RuleFor(x => x.ExistingImageUrl)
            .NotEmpty().WithMessage("ExistingImageUrl is required if provided")
            .When(x => x.ExistingImageUrl != null);

        RuleFor(x => x)
            .Must(x => !(x.IsPrimary == true && x.IsSecondary == true))
            .WithMessage("An image cannot be both primary and secondary at the same time.");
    }
}