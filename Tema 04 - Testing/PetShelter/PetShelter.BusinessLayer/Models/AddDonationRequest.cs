namespace PetShelter.BusinessLayer.Models;

public class AddDonationRequest
{
    public decimal Amount { get; set; }
    public Person Donor { get; set; }
}