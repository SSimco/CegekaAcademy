using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
{
    public FundraiserRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<Fundraiser?> GetByIdWithDonors(int id)
    {
        return await _context.Fundraisers.Include(p => p.Donors).SingleOrDefaultAsync(x => x.Id == id);
    }
}