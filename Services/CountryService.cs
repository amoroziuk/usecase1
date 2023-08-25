using Newtonsoft.Json;
using UseCase1.Models;

namespace UseCase1.Services;

public class CountryService : ICountryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CountryService> _logger;
    private readonly string _apiBaseUrl;

    public CountryService(IConfiguration configuration, ILogger<CountryService> logger)
    {
        _httpClient = new HttpClient();
        _apiBaseUrl = configuration.GetValue<string>("CountryApiBaseUrl")
                     ?? throw new ArgumentNullException("CountryApiBaseUrl is not defined in configuration.");
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<Country>> GetAllCountriesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return DeserializeCountries(responseData);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching all countries: {ex.Message}");
            throw; // Re-throw the exception or handle it as required
        }
    }

    public async Task<IEnumerable<Country>> GetFilteredCountriesAsync(RequestModel model)
    {
        try
        {
            var allCountries = await GetAllCountriesAsync() ?? Enumerable.Empty<Country>();

            allCountries = FilterCountriesByName(allCountries, model.Name);
            allCountries = FilterCountriesByPopulation(allCountries, model.Population);
            allCountries = SortCountries(allCountries, model.Sort);

            return model.Limit.HasValue ? allCountries.Take(model.Limit.Value) : allCountries;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while fetching filtered countries: {ex.Message}");
            throw; // Re-throw the exception or handle it as required
        }
    }

    private IEnumerable<Country> DeserializeCountries(string responseData)
    {
        return JsonConvert.DeserializeObject<IEnumerable<Country>>(responseData);
    }

    private IEnumerable<Country> FilterCountriesByName(IEnumerable<Country> countries, string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? countries
            : countries.Where(x => x.Name.Common.ToLower().Contains(name.ToLower()));
    }

    private IEnumerable<Country> FilterCountriesByPopulation(IEnumerable<Country> countries, long? populationInMillions)
    {
        if (!populationInMillions.HasValue)
        {
            return countries;
        }

        var populationExact = populationInMillions.Value * 1000000;
        return countries.Where(x => x.Population < populationExact);
    }

    private IEnumerable<Country> SortCountries(IEnumerable<Country> countries, string sort)
    {
        return sort?.ToLower() switch
        {
            "ascend" => countries.OrderBy(item => item.Name.Common),
            "descend" => countries.OrderByDescending(item => item.Name.Common),
            _ => countries
        };
    }
}
