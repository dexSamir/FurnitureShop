using FluentValidation;
using FurnitureShop.BL.Dtos.CategoryDtos;

namespace FurnitureShop.BL.Validators.CategoryValidators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.ParentCategoryId)
            .GreaterThanOrEqualTo(0)
            .When(x => x.ParentCategoryId.HasValue)
            .WithMessage("ParentId cannot be negative");
    }
}