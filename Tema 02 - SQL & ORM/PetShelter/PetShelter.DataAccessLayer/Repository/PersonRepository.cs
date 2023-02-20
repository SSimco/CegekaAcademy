using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{

    public PersonRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<Person?> GetPersonByIdNumber(string idNumber)
    {
        return await _context.Persons.SingleOrDefaultAsync(p => p.IdNumber == idNumber);
    }
    public async Task<IEnumerable<Person>> GetDonors()
    {
        return await _context.Persons.Include(p => p.Donations).Where(p => p.Donations.Count() > 0).ToListAsync();
    }
}