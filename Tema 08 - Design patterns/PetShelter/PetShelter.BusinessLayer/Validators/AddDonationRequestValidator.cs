using FluentValidation;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Validators;

public class AddDonationRequestValidator : AbstractValidator<AddDonationRequest>
{
    public AddDonationRequestValidator()
    {
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0m);
        RuleFor(x => x.Donor).NotEmpty().SetValidator(new PersonValidator());

        RuleFor(x => x.Donor.DateOfBirth).LessThanOrEqualTo(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
          .WithMessage("Donor should be an adult.");
    }
}
