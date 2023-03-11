namespace PetShelter.Domain.Services;
public interface IFundraiserService
{
    Task<int> CreateFundraiser(Fundraiser fundraiser);
    Task<Fundraiser> GetFundraiser(int id);
    Task<Fundraiser> GetFundraiserWithDonors(int id);
    Task DonateToFundraiser(int id, FundraiserDonation fundraiserDonation);
    Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers();
    Task DeleteFundraiser(int id);
}
