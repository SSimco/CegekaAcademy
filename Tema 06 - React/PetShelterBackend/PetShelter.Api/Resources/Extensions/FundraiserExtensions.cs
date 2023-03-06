namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtensions
{
    public static Domain.Fundraiser AsDomainModel(this FundraiserCreationInfo fundraiserInfo)
    {
        return new Domain.Fundraiser
        {
            GoalValue = fundraiserInfo.GoalValue,
            Name = fundraiserInfo.Name,
            DueDate = fundraiserInfo.DueDate,
            Owner = fundraiserInfo.Owner.AsDomainModel()
        };
    }

    public static FundraiserInfo AsResource(this Domain.Fundraiser fundraiser)
    {
        return new FundraiserInfo
        {
            Id = fundraiser.Id,
            GoalValue = fundraiser.GoalValue,
            Name = fundraiser.Name,
            DueDate = fundraiser.DueDate,
            CurrentRaisedAmount = fundraiser.CurrentRaisedAmount,
            Donors = fundraiser.Donors?.Select(p => p.AsResource()).ToList(),
            Status = fundraiser.Status.ToString()
        };
    }
}