using FluentValidation;
using PetShelter.Api.Resources;

namespace PetShelter.Api.Validators;

public class FundraiserCreationInfoValidator : AbstractValidator<FundraiserCreationInfo>
{
    public FundraiserCreationInfoValidator()
    {
        RuleFor(_ => _.GoalValue).GreaterThan(100m);
        RuleFor(_ => _.Name).MinimumLength(10);
        RuleFor(_ => _.Owner).SetValidator(new PersonValidator());
    }
}
