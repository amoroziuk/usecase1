using Microsoft.AspNetCore.Mvc;
using UseCase1.Services;

namespace UseCase1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("fetchCountries")]
    public async Task<IActionResult> FetchCountries()
    {
        try
        {
            var countries = await _countryService.GetAllCountriesAsync();

            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name/common")]
    public async Task<IActionResult> SearchCountries(string name)
    {
        try
        {
            var countries = await _countryService.SearchCountriesAsync(name);

            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
