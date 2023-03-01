using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace PetShelter.BusinessLayer.Tests.IntegrationTests;

public class RepositoryTests
{
    private readonly PersonRepository _personRepository;

    public RepositoryTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<PetShelterContext>();
        string connectionString = "Server=localhost;Database=PetShelter;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
        PetShelterContext context = new PetShelterContext(optionsBuilder.Options);
        _personRepository = new PersonRepository(context);
    }

    [Fact]
    public async Task GivenValidPerson_WhenAdd_ThenPersonIsAddedAsync()
    {

        var personToAdd = new DataAccessLayer.Models.Person
        {
            Name = "Person",
            DateOfBirth = DateTime.Now.Date,
            IdNumber = "1234567890123"
        };

        await _personRepository.Add(personToAdd);

        var addedPerson = await _personRepository.GetById(personToAdd.Id);
        Assert.True(addedPerson != null && personToAdd.Name.Equals(addedPerson.Name) && personToAdd.IdNumber.Equals(addedPerson.IdNumber));

    }
}
