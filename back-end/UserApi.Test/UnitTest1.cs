using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using UserApi.Models;

namespace UserApi.Test;

[TestClass]
public class Tests
{
    private readonly HttpClient _httpClient;

    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0MzQxNTUiLCJPQXV0aEFjY2Vzc1Rva2VuIjoiZTI4QXpSZ3U2QVRRR1lDd0hmREEiLCJPQXV0aEFjY2Vzc1Rva2VuU2VjcmV0IjoidlVoRGdSeE5EMkREZVlTSEc4SkZEdjlBeFBINkFDVVBtZGJyQlZrVCIsImV4cCI6MTY3MzgwMDYxNiwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.gVQrMP1_MlidJi782P3OWp6Lt4gXcdopawhT1BW526g"; 

    public Tests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
    }
    
    [TestMethod]
    public async Task GetCurrentUser()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/current");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<User>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetUserPreferences()
    {
        /*var stopwatch = new Stopwatch();
        stopwatch.Start();*/
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/profiles");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<StudentGetVM>(stringifiedContent);
        Assert.IsNotNull(content);
        /*stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);*/
    }
}