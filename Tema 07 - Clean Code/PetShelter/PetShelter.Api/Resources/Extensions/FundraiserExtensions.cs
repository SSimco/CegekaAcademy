namespace PetShelter.Api.Resources.Extensions;

public static class FundraiserExtensions
{
    public static Domain.FundraiserDonation AsDomainModel(this FundraiserDonation fundraiserDonation)
    {
        return new Domain.FundraiserDonation
        {
            Donor = fundraiserDonation.Donor.AsDomainModel(),
            Amount = fundraiserDonation.Amount
        };
    }

    public static Domain.Fundraiser AsDomainModel(this FundraiserCreateRequest fundraiserCreateRequest)
    {
        return new Domain.Fundraiser
        {
            GoalValue = fundraiserCreateRequest.GoalValue,
            Name = fundraiserCreateRequest.Name,
            DueDate = fundraiserCreateRequest.DueDate,
            Owner = fundraiserCreateRequest.Owner.AsDomainModel()
        };
    }

    public static FundraiserPartialInfo AsPartialInfo(this Domain.Fundraiser fundraiser)
    {
        return new FundraiserPartialInfo
        {
            Name = fundraiser.Name,
            Status = fundraiser.Status.ToString()
        };
    }

    public static FundraiserInfo AsResource(this Domain.Fundraiser fundraiser)
    {
        return new FundraiserInfo
        {
            GoalValue = fundraiser.GoalValue,
            Name = fundraiser.Name,
            DueDate = fundraiser.DueDate,
            CurrentRaisedAmount = fundraiser.CurrentRaisedAmount,
            Donors = fundraiser.Donors.Select(p => p.AsResource()).ToList(),
            Status = fundraiser.Status.ToString()
        };
    }
}