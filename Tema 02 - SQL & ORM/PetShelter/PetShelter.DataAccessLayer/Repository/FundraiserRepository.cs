using PetShelter.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>
{
    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }
    public async Task<decimal> GetCurrentRaisedAmount(int fundraiserId)
    {
        return await _context.Donations.Where(p => p.FundraiserId == fundraiserId).SumAsync(p => p.Amount);
    }
}