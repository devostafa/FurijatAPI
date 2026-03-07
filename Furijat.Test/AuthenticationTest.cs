using System.Net.Http.Json;
using Furijat.Data.DTOs.RequestDTO;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furijat.Test;

public class AuthenticationTest
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly ITestOutputHelper _outputHelper = new TestOutputHelper();

    [Fact]
    public async void LoginTest()
    {
        var loginDto = new LoginRequestDTO
        {
            Username = "testuser", Password = "1234"
        };
        var jsonloginrequest = JsonConvert.SerializeObject(loginDto);

        var url = "http://localhost:5116/auth/login";
        //receive token
        var response = await _httpClient.PostAsJsonAsync(url, loginDto).Result.Content.ReadFromJsonAsync<string>();

        if (!string.IsNullOrEmpty(response))
        {
            _outputHelper.WriteLine(response);
        }
        else
        {
            Assert.Fail($"error to get token {response}");
        }
    }
}