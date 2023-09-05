using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Shopping.Client.Data;
using Shopping.Client.Models;

namespace Shopping.Client.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClient)
    {
        _logger = logger;
        _httpClient = httpClient.CreateClient("ShoppingApiClient");
    }

    public async Task<IActionResult> Index()
    {

        // Configure JsonSerializerOptions to handle null values
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Optional: Ignore case when deserializing property names
            ReadCommentHandling = JsonCommentHandling.Skip, // Optional: Skip comments in JSON input
            AllowTrailingCommas = true, // Optional: Allow trailing commas in JSON input
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull // Handle null values when serializing
        };
        var response = await _httpClient.GetAsync("/Product");
        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
