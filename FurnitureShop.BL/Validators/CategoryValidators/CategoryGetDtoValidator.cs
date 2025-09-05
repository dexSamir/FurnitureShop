using FluentValidation;
using FurnitureShop.BL.Dtos.CategoryDtos;

namespace FurnitureShop.BL.Validators.CategoryValidators;

public class CategoryGetDtoValidator :  AbstractValidator<CategoryGetDto>
{
    public CategoryGetDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.CreatedTime)
            .NotEmpty()
            .WithMessage("CreatedTime is required");

        RuleFor(x => x.UpdatedTime)
            .GreaterThanOrEqualTo(x => x.CreatedTime)
            .When(x => x.UpdatedTime.HasValue)
            .WithMessage("UpdatedTime cannot be earlier than CreatedTime");

        RuleForEach(x => x.Subcategories)
            .SetValidator(new CategoryGetDtoValidator());
    }
}