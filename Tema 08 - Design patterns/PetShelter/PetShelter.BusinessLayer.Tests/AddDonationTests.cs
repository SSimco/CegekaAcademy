using Moq;
using PetShelter.BusinessLayer.Constants;
using PetShelter.BusinessLayer.Validators;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;

namespace PetShelter.BusinessLayer.Tests;

public class AddDonationTests
{
    [Fact]
    public async Task GivenValidRequest_WhenAddDonation_DonationIsAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());
        var request = new AddDonationRequestBuilder().SetDonationAmount(10m)
            .SetDonorName("Donor")
            .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength))
            .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
            .GetRequest();
        await donationServiceSut.AddDonation(request);

        mockDonationRepository.Verify(x => x.Add(It.Is<Donation>(d => d.Amount == request.Amount)), Times.Once);
    }

    [Fact]
    public async Task GivenRequestWithMissingAmount_WhenAddDonation_DonationIsNotAdded()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequestBuilder().SetDonorName("Donor")
            .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength))
            .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
            .GetRequest();
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

        var request = new AddDonationRequestBuilder().SetDonationAmount(donationAmount)
            .SetDonorName("Donor")
            .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength))
            .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
            .GetRequest();
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenDonorNameWithNotEnoughCharacters_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequestBuilder().SetDonationAmount(10m)
            .SetDonorName(new string('c', PersonConstants.NameMinLength - 1))
            .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength))
            .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
            .GetRequest();

        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenIdNumberIsNotRequiredLength_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequestBuilder().SetDonationAmount(10m)
           .SetDonorName(new string("Donor"))
           .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength - 1))
           .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge))
           .GetRequest();
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }

    [Fact]
    public async Task GivenDonorIsNotAnAdult_WhenAddDonation_ThenThrowsArgumenExceptionAsync()
    {
        var mockDonationRepository = new Mock<IDonationRepository>();
        var donationServiceSut = new DonationService(mockDonationRepository.Object, new AddDonationRequestValidator());

        var request = new AddDonationRequestBuilder().SetDonationAmount(10m)
          .SetDonorName(new string("Donor"))
          .SetDonorIdNumber(new string('1', PersonConstants.IdNumberLength - 1))
          .SetDonorDateOfBirth(DateTime.Now.Date.AddYears(-PersonConstants.AdultMinAge + 1))
          .GetRequest();
        await Assert.ThrowsAsync<ArgumentException>(() => donationServiceSut.AddDonation(request));

        mockDonationRepository.Verify(x => x.Add(It.IsAny<Donation>()), Times.Never);
    }
}
