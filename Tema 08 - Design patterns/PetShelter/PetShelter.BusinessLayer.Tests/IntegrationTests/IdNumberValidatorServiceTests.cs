using PetShelter.BusinessLayer.ExternalServices;

namespace PetShelter.BusinessLayer.Tests.IntegrationTests;

public class IdNumberValidatorServiceTests
{
    private readonly IdNumberValidator _idNumberValidatior;
    public IdNumberValidatorServiceTests()
    {
        _idNumberValidatior = new IdNumberValidator(new HttpClient());
    }

    [Fact]
    public async void GivenIdNumberIsValid_WhenValidate_ReturnsTrue()
    {
        var validCnp = "1234567890123";

        var isValid = await _idNumberValidatior.Validate(validCnp);

        Assert.Equal(isValid, true);
    }
}
