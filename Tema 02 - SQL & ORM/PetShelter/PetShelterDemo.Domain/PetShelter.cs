using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.DataAccessLayer.Models;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IPetRepository petRepository;
    private readonly IDonationRepository donationRepository;
    private readonly IFundraiserRepository fundraiserRepository;
    private readonly IPersonRepository personRepository;
    public PetShelter()
    {
        PetShelterContext context = new PetShelterContext();
        petRepository = new PetRepository(context);
        donationRepository = new DonationRepository(context);
        personRepository = new PersonRepository(context);
        fundraiserRepository = new FundraiserRepository(context);
    }

    public void RegisterPet(Pet pet)
    {
        petRepository.Add(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRepository.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRepository.GetPetByName(name).Result;
    }

    public void Donate(Person donor, Donation donation)
    {
        donation.Donor = donor;
        donationRepository.Add(donation);
    }
    public void Donate(Person donor, Donation donation, Fundraiser fundraiser)
    {
        donation.Donor = donor;
        donation.Fundraiser = fundraiser;
        donationRepository.Add(donation);
    }
    public decimal GetTotalDonations()
    {
        return donationRepository.GetTotalNonFundraiserDonations().Result;
    }
    public IReadOnlyList<Person> GetDonorsForFundraiser(Fundraiser fundraiser)
    {
        return (IReadOnlyList<Person>)fundraiserRepository.GetDonorsForFundraiser(fundraiser.Id).Result;
    }
    public IReadOnlyList<Person> GetAllDonors()
    {
        return (IReadOnlyList<Person>)personRepository.GetDonors().Result;
    }

    public void RegisterFundraiser(Fundraiser fundraiser)
    {
        fundraiserRepository.Add(fundraiser);
    }

    public IReadOnlyList<Fundraiser> GetAllFundraisers()
    {
        return fundraiserRepository.GetAll().Result;
    }

    public decimal GetCurrentRaisedAmountForFundraiser(Fundraiser fundraiser)
    {
        return fundraiserRepository.GetCurrentRaisedAmount(fundraiser.Id).Result;
    }
}