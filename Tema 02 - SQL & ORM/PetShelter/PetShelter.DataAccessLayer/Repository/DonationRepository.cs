using PetShelter.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShelter.DataAccessLayer.Repository;

public class DonationRepository : BaseRepository<Donation>, IDonationRepository
{
    public DonationRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<decimal> GetTotalNonFundraiserDonations()
    {
        return await _context.Donations.Where(p => p.FundraiserId == null).SumAsync(p => p.Amount);
    }
}