using System.Text;
using Furijat.Data.Data.DTOs.RequestDTO;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furijat.Test;

public class DonationTest
{
    private HttpClient _httpClient = new HttpClient();
    private ITestOutputHelper _outputHelper = new TestOutputHelper();

    [Theory]
    [InlineData("a5379337-e6a4-4222-aa88-233358bda6e9", "694d6683-d3e6-4bc1-ab5d-f2f67f887332", "visa", 5000)]
    public async void DonateTest(string userId, string projectId, string paymenttype, int donationAmount)
    {
        DonationRequestDTO donationRequest = new DonationRequestDTO() {ProjectId = Guid.Parse(projectId), UserId = Guid.Parse(userId), PaymentType = paymenttype, DonationAmount = donationAmount};
        var jsonRequest = new StringContent(donationRequest.ToString(), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5116/donations/donate", jsonRequest ).Result.Content.ReadAsStringAsync();
        _outputHelper.WriteLine("donation done: ", response);
    }
}