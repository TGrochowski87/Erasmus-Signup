using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using UniversityApi.Models;

namespace UniversityApi.Test;

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
    public async Task GetUniversities_NoFilters()
    {
        var response = await _httpClient.GetAsync("universities?PageSize=10&Page=1");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<DestinationResult>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetUniversities_WithFilters()
    {
        var response = await _httpClient.GetAsync("universities?PageSize=10&Page=1&Country=Austria&SubjectAreaId=7&OrderBy=InterestedStudentsAsc");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<DestinationResult>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetUniversitiesRecommended()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("universities-recommended");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<RecommendedDestination>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetUniversitiesRecommendedByStudents()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await _httpClient.GetAsync("universities-recommended-by-students");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<RecommendedDestination>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetCountries()
    {
        var response = await _httpClient.GetAsync("countries");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<string>>(stringifiedContent);
        Assert.IsNotNull(content);
    }
    
    [TestMethod]
    public async Task GetStudyDomains()
    {
        var response = await _httpClient.GetAsync("study-domains");
        Assert.IsTrue(response.IsSuccessStatusCode);
        var stringifiedContent = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<List<StudyDomainVM>>(stringifiedContent);
        Assert.IsNotNull(content);
    }
}