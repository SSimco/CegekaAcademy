using PetShelter.BusinessLayer.Models;

namespace PetShelter.BusinessLayer.Tests;

class AddDonationRequestBuilder
{
    private AddDonationRequest _request;
    private Person _donor;
    public AddDonationRequestBuilder()
    {
        _request = new AddDonationRequest();
        _donor = new Person();
    }
    public AddDonationRequestBuilder SetDonorName(string name)
    {
        _donor.Name = name;
        return this;
    }
    public AddDonationRequestBuilder SetDonorIdNumber(string idNumber)
    {
        _donor.IdNumber = idNumber;
        return this;
    }
    public AddDonationRequestBuilder SetDonorDateOfBirth(DateTime dateOfBirth)
    {
        _donor.DateOfBirth = dateOfBirth;
        return this;
    }
    public AddDonationRequestBuilder SetDonationAmount(decimal amount)
    {
        _request.Amount = amount;
        return this;
    }
    public AddDonationRequest GetRequest()
    {
        _request.Donor = _donor;
        return _request;
    }
}