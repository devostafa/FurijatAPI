using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.DTOs.ResponseDTO;

namespace Furijat.Services.Services.Donate;

public interface IDonate
{
    public Task<List<DonationResponseDTO>> GetDonations();
    public Task<bool> DecideDonation(string donationid, bool decision);
    public Task<bool> DonateToProject(DonationRequestDTO donationtolog);
}