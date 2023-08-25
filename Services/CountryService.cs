using Newtonsoft.Json;
using UseCase1.Models;

namespace UseCase1.Services;

public class CountryService : ICountryService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public CountryService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiBaseUrl = configuration.GetValue<string>("CountryApiBaseUrl");
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync(int? limit = null)
    {
        var response = await _httpClient.GetAsync(_apiBaseUrl);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseData);

        return limit.HasValue ? countries.Take(limit.Value) : countries;
    }

    public async Task<IEnumerable<Country>> GetFilteredCountriesAsync(RequestModel model)
    {
        var allCountries = await GetAllCountriesAsync();
        var result = new List<Country>();

        if (!string.IsNullOrWhiteSpace(model.Name))
            allCountries = allCountries?.Where(x => x.Name.Common.ToLower().Contains(model.Name.ToLower()));

        if (model.Population.HasValue)
        {
            var populationExact = model.Population.Value * 1000000; // Convert input from millions to the exact number
            allCountries = allCountries?.Where(x => x.Population < populationExact);
        }

        if (!string.IsNullOrWhiteSpace(model.Sort))
        {
            allCountries = model.Sort?.ToLower() switch
            {
                "ascend" => allCountries.OrderBy(item => item.Name.Common).ToList(),
                "descend" => allCountries.OrderByDescending(item => item.Name.Common).ToList(),
                _ => allCountries // If sort parameter is not provided or invalid, return items as is.
            };
        }

        return allCountries;
    }
}
