using FluentValidation;
using Labour.MS.Establishment.Models.DTOs.Request;
using Labour.MS.Establishment.Service.Validators.BaseValidator;

namespace Labour.MS.Establishment.Service.Validators
{
    public class EstablishmentRequestValidator : BaseValidator<EstablishmentRequest>
    {
        public EstablishmentRequestValidator()
        {
            RuleFor(x => x).NotNull()
                            .WithMessage("Establishment is not null");
            RuleFor(x => x.EstablishmentId).NotNull()
                                            .NotEmpty()
                                            .WithMessage("Establishment id is required");
        }
    }
}
