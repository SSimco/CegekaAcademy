using Microsoft.AspNetCore.Mvc;
using PetShelter.Domain.Services;
using PetShelter.Api.Resources;
using PetShelter.Api.Resources.Extensions;
using System.Collections.Immutable;
using PetShelter.Domain.Exceptions;

namespace FundraiserShelter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FundraisersController : ControllerBase
    {
        private readonly IFundraiserService _fundraiserService;

        public FundraisersController(IFundraiserService FundraiserService)
        {
            _fundraiserService = FundraiserService;
        }


        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFundraiser([FromBody] FundraiserCreationInfo fundraiserCreationInfo)
        {
            var id = await _fundraiserService.CreateFundraiser(fundraiserCreationInfo.AsDomainModel());
            return CreatedAtRoute("GetFundraiser", new { id = id }, null);
        }

        [HttpPost("{id}/donate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DonateToFundraiser(int id, [FromBody] FundraiserDonation fundraiserDonation)
        {
            try
            {
                await _fundraiserService.DonateToFundraiser(id, fundraiserDonation.Donor.AsDomainModel(), fundraiserDonation.DonationAmount);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<FundraiserInfo>>> GetFundraisers()
        {
            var data = await _fundraiserService.GetAllFundraisers();
            return Ok(data.Select(p => p.AsResource()).ToImmutableArray());
        }

        [HttpGet("{id}", Name = "GetFundraiser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FundraiserInfo>> Get(int id)
        {
            var fundraiser = await _fundraiserService.GetFundraiserWithDonors(id);
            if (fundraiser is null)
            {
                return NotFound();
            }
            return Ok(fundraiser.AsResource());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _fundraiserService.DeleteFundraiser(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
