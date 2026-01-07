

using FluentValidation;
using ChineseAuctionAPI.DTO;

namespace ChineseAuctionAPI.Validations
{

    public class DonorValidator:AbstractValidator<DonorCreateDTO>
    {

        public DonorValidator()
        {
            RuleFor(donor => donor.FirstName)
                .NotEmpty().WithMessage("Donor Name is required.");
                
            RuleFor(donor => donor.LastName)
                .NotEmpty().WithMessage("Donor Last Name is required.");

            RuleFor(donor => donor.Email)
                .EmailAddress().WithMessage("Invalid Email Address")
                .NotEmpty().WithMessage("Donor Email is required.");

            RuleFor(donor => donor.Address)
                .NotEmpty().WithMessage("Donor Address is required.");

            RuleFor(donor => donor.PhoneNumber)
                .NotEmpty().WithMessage("Donor Phone Number is required.");
        }
    }

    public class DonorUpdateValidator:AbstractValidator<DonorUpdateDTO>
    {

        public DonorUpdateValidator()
        {
            RuleFor(donor => donor.Id)
                .NotEmpty().WithMessage("Donor Id is required.");
                
            RuleFor(donor => donor.FirstName)
                .NotEmpty().WithMessage("Donor Name is required.");
                
            RuleFor(donor => donor.LastName)
                .NotEmpty().WithMessage("Donor Last Name is required.");

            RuleFor(donor => donor.Email)
                .EmailAddress().WithMessage("Invalid Email Address")
                .NotEmpty().WithMessage("Donor Email is required.");

            RuleFor(donor => donor.Address)
                .NotEmpty().WithMessage("Donor Address is required.");

            RuleFor(donor => donor.PhoneNumber)
                .NotEmpty().WithMessage("Donor Phone Number is required.");
        }
    }






}