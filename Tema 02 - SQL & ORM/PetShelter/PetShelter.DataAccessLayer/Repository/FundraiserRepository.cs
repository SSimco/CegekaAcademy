using PetShelter.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<decimal> GetCurrentRaisedAmount(int fundraiserId)
    {
        return await _context.Donations.Where(p => p.FundraiserId == fundraiserId).SumAsync(p => p.Amount);
    }

    public async Task<IEnumerable<Person>> GetDonorsForFundraiser(int fundraiserId)
    {
        return await _context.Persons
            .Join(_context.Donations,
                  donor => donor.Id,
                  donation => donation.DonorId,
                  (donor, donation) => new { Donor = donor, Donation = donation })
            .Where(p => p.Donation.FundraiserId == fundraiserId)
            .Select(p => p.Donor)
            .Distinct()
            .ToListAsync();
    }
}