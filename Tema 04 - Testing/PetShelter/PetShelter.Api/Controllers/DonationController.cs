using Microsoft.AspNetCore.Mvc;
using PetShelter.BusinessLayer;
using PetShelter.BusinessLayer.Models;

namespace PetShelter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Donate([FromBody] AddDonationRequest request)
    {
        await _donationService.AddDonation(request);
        return NoContent();
    }
}
