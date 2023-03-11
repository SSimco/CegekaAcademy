using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DomainModel;
using PetShelter.Domain.Exceptions;
using System.Collections.Immutable;

namespace PetShelter.Domain.Services;

public class FundraiserService : IFundraiserService
{
    private readonly IFundraiserRepository _fundraiserRepository;
    private readonly IPersonRepository _personRepository;

    public FundraiserService(IFundraiserRepository fundraiserRepository, IPersonRepository personRepository)
    {
        _fundraiserRepository = fundraiserRepository;
        _personRepository = personRepository;
    }

    public async Task<int> CreateFundraiser(Fundraiser fundraiser)
    {
        fundraiser.CurrentRaisedAmount = 0m;
        fundraiser.CreationDate = DateTime.Now;
        if (fundraiser.CreationDate >= fundraiser.DueDate)
        {
            fundraiser.Status = FundraiserStatus.Closed;
        }
        else
        {
            fundraiser.Status = FundraiserStatus.Active;
        }
        var fundraiserFromDomain = fundraiser.FromDomainModel();
        await _fundraiserRepository.Add(fundraiserFromDomain);
        return fundraiserFromDomain.Id;
    }

    public async Task DeleteFundraiser(int id)
    {
        var fundraiser = await _fundraiserRepository.GetById(id);
        if (fundraiser == null)
        {
            throw new NotFoundException($"No fundraiser with id {id} found!");
        }
        fundraiser.Status = FundraiserStatus.Closed.ToString();
        await _fundraiserRepository.Update(fundraiser);
    }

    public async Task DonateToFundraiser(int id, Person donor, decimal donationAmount)
    {
        var fundraiser = await _fundraiserRepository.GetByIdWithDonors(id);
        if (fundraiser == null)
        {
            throw new NotFoundException($"No fundraiser with id {id} found!");
        }
        fundraiser.Donors.Add(donor.FromDomainModel());
        fundraiser.CurrentRaisedAmount += donationAmount;
        if (fundraiser.CurrentRaisedAmount >= fundraiser.GoalValue)
        {
            fundraiser.Status = FundraiserStatus.Closed.ToString();
        }
        await _fundraiserRepository.Update(fundraiser);
    }

    public async Task<Fundraiser?> GetFundraiserWithDonors(int id)
    {
        var fundraiser = await _fundraiserRepository.GetByIdWithDonors(id);
        return fundraiser?.ToDomainModel();
    }

    public async Task<IReadOnlyCollection<Fundraiser>> GetAllFundraisers()
    {
        var fundraisers = await _fundraiserRepository.GetAll();
        return fundraisers.Select(p => p.ToDomainModel()).ToImmutableArray();
    }

    public async Task<Fundraiser?> GetFundraiser(int id)
    {
        var fundraiser = await _fundraiserRepository.GetById(id);
        return fundraiser?.ToDomainModel();
    }
}