using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UseCase1.Models;

namespace UseCase1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CountriesController()
    {
        _httpClient = new HttpClient();
    }

    [HttpGet("fetchCountries")]
    public async Task<IActionResult> FetchCountries()
    {
        try
        {
            // Make a request to the public REST Countries API
            var response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");
            response.EnsureSuccessStatusCode();

            // Parse the retrieved API response data into a variable/object
            var responseData = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseData);

            return Ok(jsonObject.Take(1));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
