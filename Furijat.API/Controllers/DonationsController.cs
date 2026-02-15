using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;
using Furijat.Services.Services.Donate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furijat.API.Controllers;

[Route("donations")]
public class DonationsController : BaseController
{
    private readonly IDonate _donationservice;

    public DonationsController(IDonate donationservice)
    {
        _donationservice = donationservice;
    }

    [HttpGet("getdonations")]
    public async Task<List<DonationResponseDTO>> GetDonations()
    {
        return await _donationservice.GetDonations();
    }

    [Authorize]
    [HttpPost("decidedonation")]
    public async Task<bool> DecideDonation(string donationid, bool decision)
    {
        return await _donationservice.DecideDonation(donationid, decision);
    }

    [Authorize]
    [HttpPost("donate")]
    public async Task<bool> Donate(DonationRequestDTO donationrequest)
    {
        return await _donationservice.DonateToProject(donationrequest);
    }
    
}