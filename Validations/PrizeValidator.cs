using FluentValidation;
using ChineseAuctionAPI.DTO;
namespace ChineseAuctionAPI.Validations
{
    public class PrizeValidator:AbstractValidator<CreatePrizeDTO>
    {

        public PrizeValidator()
        {
            RuleFor(prize => prize.Name)
                .NotEmpty().WithMessage("Prize Name is required.");
                
            RuleFor(prize => prize.Description)
                .NotEmpty().WithMessage("Prize Description is required.");
                
            RuleFor(prize => prize.DonorId)
                .NotEmpty().WithMessage("Donor Id is required.");

            RuleFor(prize=>prize.CategoryId)
                .NotEmpty().WithMessage("Category Id is required");

            RuleFor(prize => prize.Qty)
                .NotEmpty().WithMessage("Quantity is required.")
                .InclusiveBetween(1, 50).WithMessage("Quantity must be between 1 and 50.");  
        }
    }

    public class PrizeUpdateValidator:AbstractValidator<UpdatePrizeDTO>
    {

        public PrizeUpdateValidator()
        {
            RuleFor(prize => prize.Id)
                .NotEmpty().WithMessage("Prize Id is required.");

            RuleFor(prize => prize.Name)
                .NotEmpty().WithMessage("Prize Name is required.");
                
            RuleFor(prize => prize.DonorId)
                .NotEmpty().WithMessage("Donor Id is required.");

            RuleFor(prize=>prize.CategoryId)
                .NotEmpty().WithMessage("Category Id is required");

            RuleFor(prize => prize.Qty)
                .NotEmpty().WithMessage("Quantity is required.")
                .InclusiveBetween(1, 50).WithMessage("Quantity must be between 1 and 50.");  
        }
    }
}

