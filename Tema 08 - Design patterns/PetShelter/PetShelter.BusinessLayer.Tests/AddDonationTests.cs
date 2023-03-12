using Moq;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Models;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    static AddDonationRequest GetValidDonationRequest()
    {
        return new AddDonationRequest
        {
            Amount = 10m,
            Donor = new Models.Person
            {
                Name = "Donor",
                IdNumber = new string('1', PersonConstants.IdNumberLength),
                DateOfBirth = DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge)
            }
        };
    }

    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = GetValidDonationRequest();
        await donationServiceSut.AddDonation(request);

        mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == request.Amount)), Times.Once);
    }

    [Fact]
    public async Task GivenRequestWithMissingAmount_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequest();
        request.Donor = new Models.Person
        {
            Name = "Donor",
            IdNumber = new string('1', PersonConstants.IdNumberLength),
            DateOfBirth = DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge)
        };
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GivenDonatinAmountLEQ0_WhenAddDonation_ThenThrowsArgumenExceptionAsync(decimal donationAmount)
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = GetValidDonationRequest();
        request.Amount = donationAmount;
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenDonorNameWithNotEnoughCharacters_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = GetValidDonationRequest();
        request.Donor.Name = new string('c', PersonConstants.NameMinLength - 1);

        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenIdNumberIsNotRequiredLength_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = GetValidDonationRequest();
        request.Donor.IdNumber = new string('1', PersonConstants.IdNumberLength - 1);

        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenDonorIsNotAnAdult_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = GetValidDonationRequest();
        request.Donor.DateOfBirth = DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge + 1);

        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }
}
