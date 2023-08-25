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

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        var response = await _httpClient.GetAsync(_apiBaseUrl);
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        var countries = JsonConvert.DeserializeObject<IEnumerable<Country>>(responseData);

        return countries;
    }

    public async Task<IEnumerable<Country>> GetFilteredCountriesAsync(RequestModel model)
    {
        var allCountries = await GetAllCountriesAsync();

        if (!string.IsNullOrWhiteSpace(model.Name))
            return allCountries?.Where(x => x.Name.Common.ToLower().Contains(model.Name.ToLower()));

        if (model.Population.HasValue)
        {
            var populationExact = model.Population.Value * 1000000; // Convert input from millions to the exact number
            return allCountries?.Where(x => x.Population < populationExact);
        }

        return allCountries;
    }
}
