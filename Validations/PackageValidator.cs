namespace ChineseAuctionAPI.Validations
 {

    public class PackageValidator:AbstractValidator<CreatePackageDTO>
    {

        public PackageValidator()
        {
            RuleFor(package => package.Name)
                .NotEmpty().WithMessage("Package Name is required.");
                
            RuleFor(package => package.NumOfTickets)
                .GreaterThan(0).WithMessage("Package Number of Tickets must be greater than 0.")
                .NotEmpty().WithMessage("Package Number of Tickets is required.");
                
            RuleFor(package => package.Price)
                .NotEmpty().WithMessage("Package Price is required.")
                .GreaterThan(0).WithMessage("Package Price must be greater than 0.");  
        }
    }

    public class PackageUpdateValidator:AbstractValidator<UpdatePackageDTO>
    {

        public PackageUpdateValidator()
        {
            RuleFor(package => package.Id)
                .NotEmpty().WithMessage("Package Id is required.");

            RuleFor(package => package.Name)
                .NotEmpty().WithMessage("Package Name is required.");
                
            RuleFor(package => package.NumOfTickets)
                .GreaterThan(0).WithMessage("Package Number of Tickets must be greater than 0.")
                .NotEmpty().WithMessage("Package Number of Tickets is required.");
                
            RuleFor(package => package.Price)
                .NotEmpty().WithMessage("Package Price is required.")
                .GreaterThan(0).WithMessage("Package Price must be greater than 0.");  
        }
    }
 }