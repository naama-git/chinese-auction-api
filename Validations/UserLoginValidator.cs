using FluentValidation;

using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.Validations
{
    public class UserLoginValidator : AbstractValidator<LogInDTO>
    {

        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.");
                
        }
    }
}
