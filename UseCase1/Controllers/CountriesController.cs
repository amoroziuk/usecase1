using Microsoft.AspNetCore.Mvc;
using UseCase1.Models;
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
    public async Task<IActionResult> FetchCountries([FromQuery] RequestModel model)
    {
        try
        {
            var countries = await _countryService.GetFilteredCountriesAsync(model);

            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("byName")]
    public async Task<IActionResult> FetchByName([FromQuery] string name, [FromQuery] string sort = "ascend",
        [FromQuery] int limit = 15)
    {
        try
        {
            var countries = await _countryService.GetFilteredCountriesAsync(new RequestModel()
            {
                Name = name,
                Sort = sort,
                Limit = limit
            });

            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("byPopulation")]
    public async Task<IActionResult> FetchByPopulation([FromQuery] int population, [FromQuery] string sort = "ascend",
        [FromQuery] int limit = 15)
    {
        try
        {
            var countries = await _countryService.GetFilteredCountriesAsync(new RequestModel()
            {
                Population = population,
                Sort = sort,
                Limit = limit
            });

            return Ok(countries);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
