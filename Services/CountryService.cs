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

    public async Task<IEnumerable<Country>> SearchCountriesAsync(string name)
    {
        var allCountries = await GetAllCountriesAsync();

        return allCountries?.Where(x => x.Name.Common.ToLower().Contains(name.ToLower()));
    }
}
