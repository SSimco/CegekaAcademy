using FluentValidation;
using PetShelter.BusinessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer;

public class DonationService
{
    private readonly IDonationRepository _donationRepository;
    private readonly IValidator<AddDonationRequest> _donationValidator;

    public DonationService(IDonationRepository donationRepository, IValidator<AddDonationRequest> validator)
    {
        _donationValidator = validator;
        _donationRepository = donationRepository;
    }

    public async Task AddDonation(AddDonationRequest addDonationRequest)
    {
        var validationResult = _donationValidator.Validate(addDonationRequest);
        if (!validationResult.IsValid) { throw new ArgumentException(); }

        await _donationRepository.Add(new DataAccessLayer.Models.Donation
        {
            Amount = addDonationRequest.Amount,
            Donor = new DataAccessLayer.Models.Person
            {
                Name = addDonationRequest.Donor.Name,
                IdNumber = addDonationRequest.Donor.IdNumber,
                DateOfBirth = addDonationRequest.Donor.DateOfBirth
            }
        });
    }
}