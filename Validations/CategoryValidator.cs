


using FluentValidation;
using static ChineseAuctionAPI.DTO.CategoryDTO;

namespace ChineseAuctionAPI.Validations
{
    public class CategoryValidator:AbstractValidator<CategoriesDTO>
    {

        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Category Name is required.");
        }
    }

    public class CategoryUpdateValidator:AbstractValidator<UpdateCategory>
    {

        public CategoryUpdateValidator()
        {
            RuleFor(category => category.Id)
                .NotEmpty().WithMessage("Category Id is required.");

            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Category Name is required.");
        }
    }
}