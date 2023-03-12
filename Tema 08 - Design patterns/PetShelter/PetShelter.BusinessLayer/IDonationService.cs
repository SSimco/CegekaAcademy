using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer;

public interface IDonationService
{
    Task AddDonation(AddDonationRequest addDonationRequest);
}
