using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
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

    [HttpGet]
    public async Task<IActionResult> GetWithLimit([FromQuery] int limit = 15)
    {
        var countries = await _countryService.GetAllCountriesAsync(limit);
        return Ok(countries);
    }
}
