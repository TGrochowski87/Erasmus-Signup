using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserApi.Models;

namespace UserApi.Test;

[TestClass]
public class Tests
{
    private readonly HttpClient _httpClient;

    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0Mzg5OTMiLCJPQXV0aEFjY2Vzc1Rva2VuIjoiZXk4SkJzbno5OW5VUzlCTXk2VmsiLCJPQXV0aEFjY2Vzc1Rva2VuU2VjcmV0IjoiTHJ0d0JwRjRzSEJnZG53UUs0WjR1QlZZdmszZ0M1ek1OZmFtUk1YZSIsImV4cCI6MTY3MzgwODA5NSwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.hfzwE0D5yZhHqC2HaGKueruUW42ef8s7wfAIsVg6KBg";

    public Tests()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();

        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        //var response = _httpClient.GetAsync("user/profiles");
        //var stringifiedContent = response.Content.ReadAsStringAsync();
        //var content = JsonConvert.DeserializeObject<StudentGetVM>(stringifiedContent);

    }

    [TestMethod]
    public async Task GetCurrentUserSecond()
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
        Console.WriteLine("cur", stopwatch.Elapsed);
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
        Console.WriteLine("cur", stopwatch.Elapsed);
    }

    [TestMethod]
    public async Task GetUserPreferencesSecond()
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
        Console.WriteLine("pref", stopwatch.Elapsed);
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
        Console.WriteLine("pref", stopwatch.Elapsed);
    }
}

[TestClass]
public class TestsAll
{
    private readonly HttpClient _httpClient;

    private const string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0Mzg5OTMiLCJPQXV0aEFjY2Vzc1Rva2VuIjoiZXk4SkJzbno5OW5VUzlCTXk2VmsiLCJPQXV0aEFjY2Vzc1Rva2VuU2VjcmV0IjoiTHJ0d0JwRjRzSEJnZG53UUs0WjR1QlZZdmszZ0M1ek1OZmFtUk1YZSIsImV4cCI6MTY3MzgwODA5NSwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlUG9zdG1hbkNsaWVudCJ9.hfzwE0D5yZhHqC2HaGKueruUW42ef8s7wfAIsVg6KBg";

    public TestsAll()
    {
        var webAppFactory = new WebApplicationFactory<Program>();
        _httpClient = webAppFactory.CreateDefaultClient();

        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        //var response = _httpClient.GetAsync("user/profiles");
        //var stringifiedContent = response.Content.ReadAsStringAsync();
        //var content = JsonConvert.DeserializeObject<StudentGetVM>(stringifiedContent);

    }

    [TestMethod]
    public async Task All()
    {
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
            Console.WriteLine("cur", stopwatch.Elapsed);
        }

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
            Console.WriteLine("cur", stopwatch.Elapsed);
        }

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
            Console.WriteLine("pref", stopwatch.Elapsed);
        }

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
            Console.WriteLine("pref", stopwatch.Elapsed);
        }
    }

    [TestMethod]
    public async Task AllSecond()
    {
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
            Console.WriteLine("cur", stopwatch.Elapsed);
        }

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
            Console.WriteLine("cur", stopwatch.Elapsed);
        }

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
            Console.WriteLine("pref", stopwatch.Elapsed);
        }

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
            Console.WriteLine("pref", stopwatch.Elapsed);
        }
    }
}