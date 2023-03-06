﻿using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class PetRepository : BaseRepository<Pet>, IPetRepository
{
    public PetRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<Pet>> GetAllPetsWithRescuersAndAdopters()
    {
        return await _context.Pets.Include(p => p.Rescuer).Include(p => p.Adopter).ToListAsync();
    }

    public async Task<Pet?> GetPetByName(string name)
    {
        return await _context.Pets.FirstOrDefaultAsync(p => p.Name.Equals(name));
    }
}