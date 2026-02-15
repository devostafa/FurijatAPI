using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using Furijat.Data.Data.DTOs.RequestDTO;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furijat.Test;

public class AuthenticationTest
{
    private HttpClient _httpClient = new HttpClient();
    private ITestOutputHelper _outputHelper = new TestOutputHelper();

    [Fact]
    public async void LoginTest()
    {
        LoginRequestDTO loginDto = new LoginRequestDTO {Username = "testuser", Password = "1234"} ;
        var jsonloginrequest = JsonConvert.SerializeObject(loginDto);
        
        string url = "http://localhost:5116/Authentication/Login";
        //receive token
        var response = await _httpClient.PostAsJsonAsync(url, loginDto ).Result.Content.ReadFromJsonAsync<string>();
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