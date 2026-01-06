


using FluentValidation;
using static ChineseAuctionAPI.DTO.CategotyDTO;

namespace ChineseAuctionAPI.Validations
{
    public class CategoryValidator:AbstractValidator<CategoryCreateDTO>
    {

        public CategoryValidator()
        {
            RuleFor(category => category.Name)
                .NotEmpty().WithMessage("Category Name is required.");
        }
    }

    public class CategoryUpdateValidator:AbstractValidator<CategoryDTOWithId>
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