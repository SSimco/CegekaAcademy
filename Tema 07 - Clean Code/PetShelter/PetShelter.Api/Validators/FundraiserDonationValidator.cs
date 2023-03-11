using FluentValidation;
using PetShelter.Api.Resources;

namespace PetShelter.Api.Validators;

public class FundraiserDonationInfoValidator : AbstractValidator<FundraiserDonation>
{
    public FundraiserDonationInfoValidator()
    {
        RuleFor(_ => _.Amount).GreaterThan(1m);
        RuleFor(_ => _.Donor).SetValidator(new PersonValidator());
    }
}
