using System.Net.Http.Json;
using Furijat.Data.Data.DTOs.RequestDTO;
using Furijat.Data.Data.Models;
using Microsoft.AspNetCore.Http;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Furijat.Test;

public class ControllersTest
{
    
    private HttpClient _httpClient = new HttpClient();
    private ITestOutputHelper _outputHelper = new TestOutputHelper();
    
    [Fact]
    public async void GetProjectsTest()
    {
        var response = await _httpClient.GetAsync("http://localhost:5116/Projects/GetProjects").Result.Content.ReadFromJsonAsync<List<Project>>();
        _outputHelper.WriteLine("projects: " + response);
    }
    
    [Theory]
    [InlineData("7e4788cd-77a9-4b03-9412-385a482cf489")]
    public async void GetProjectTest(string projectid)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5116/Projects/GetProject/{projectid}").Result.Content.ReadFromJsonAsync<Project>();
        _outputHelper.WriteLine("response: ", response);
    }
    
    [Fact]
    public async void AddProject()
    {
        string url = "http://localhost:5116/Projects/AddProject";
        ProjectRequestDTO newproject = new ProjectRequestDTO
        {
            Id = default,
            Title = "test unit project",
            Subtitle = "test subtitle",
            Description = "test description",
            CategoryId = Guid.Parse("4a858ba2-cc64-4752-973a-2b1acba5d78d"),
            UserId = Guid.Parse("c0c343f3-a9d0-4ae6-93e4-0d1923b04e60"),
            Totalfundrequired = 5000,
            Facebook = "",
            X = "n",
            Instagram = "",
            ImagesFiles = new IFormFile[]
            {
            },
            IsAccepted = false
        };
        var response = await _httpClient.PostAsJsonAsync(url, newproject ).Result.Content.ReadFromJsonAsync<bool>();
        _outputHelper.WriteLine("Add Project result: ",response);
        Assert.True(response);
    }
    
    
    
}