using FluentValidation;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Service.Validators.BaseValidator;

namespace Labour.MS.Establishment.Service.Validators
{
    public class EstablishmentRequestDetailsValidator : BaseValidator<EstablishmentDetailsRequest>
    {
        public EstablishmentRequestDetailsValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x.EstablishmentDetails!.EstablishmentName)
                    .NotNull().NotEmpty()
                    .WithMessage("Establishment Name is required");
            RuleFor(x => x.EstablishmentDetails!.EmailId)
                    .NotNull().NotEmpty()
                    .WithMessage("Email is required")
                        .Matches("^(?:[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})?$")
                        .WithMessage("Invalid email format");
        }
    }
}
