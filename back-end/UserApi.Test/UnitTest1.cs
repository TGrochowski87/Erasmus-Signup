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

    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0MzQxNTUiLCJPQXV0aEFjY2Vzc1Rva2VuIjoiVEhKaHVqQkY3NUN6bk51OHVyUzkiLCJPQXV0aEFjY2Vzc1Rva2VuU2VjcmV0IjoiYlZGNTVycXBQTXlxNGg2eGZOMzc0YXBkdnk3anVVZER6ZVFjbndXdCIsImV4cCI6MTY3NDA3NzkxMiwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.t4Q7l-yN3uWK4-UX1r8jFUX5Ot9dKnOvdKauJUX_tWs"; 

    public Tests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();
        FirstCall();
    }

    public async Task FirstCall()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        await _httpClient.GetAsync("user/profiles");
    }
    
    [TestMethod]
    public async Task GetCurrentUser()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/current");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<User>(stringifiedContent);
        Assert.IsNotNull(content);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
    
    [TestMethod]
    public async Task AGetCurrentUser()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/current");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<User>(stringifiedContent);
        Assert.IsNotNull(content);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
    
    [TestMethod]
    public async Task GetUserPreferences()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/profiles");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<StudentGetVM>(stringifiedContent);
        Assert.IsNotNull(content);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
    
    [TestMethod]
    public async Task AGetUserPreferences()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("user/profiles");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<StudentGetVM>(stringifiedContent);
        Assert.IsNotNull(content);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
    }
}