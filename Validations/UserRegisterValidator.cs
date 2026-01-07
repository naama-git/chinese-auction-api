
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static ChineseAuctionAPI.DTO.UserDTO;
using ChineseAuctionAPI.Data;

namespace ChineseAuctionAPI.Validations
{
    public class UserRegisterValidator : AbstractValidator<SignInDTO>
    {
        private readonly ChineseAuctionDBcontext _context;

        public UserRegisterValidator(ChineseAuctionDBcontext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync(BeUniqueEmail).WithMessage("Registration could not be completed with the details provided. Please try again.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Length(2, 50).WithMessage("First Name  Length must be between 2 and 50");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .Length(2, 50).WithMessage("Last Name Length must be between 2 and 50");


            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required")
                .MinimumLength(9).WithMessage("Phone Number must contain 9 digits");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }

        
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var exists = await _context.users.AnyAsync(u => u.Email == email, cancellationToken);
            return !exists;
        }

    }
}
