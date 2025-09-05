using FluentValidation;
using FurnitureShop.BL.Dtos.CategoryDtos;

namespace FurnitureShop.BL.Validators.CategoryValidators;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.ParentCategoryId)
            .GreaterThanOrEqualTo(0)
            .When(x => x.ParentCategoryId.HasValue)
            .WithMessage("ParentId cannot be negative");
    }
}