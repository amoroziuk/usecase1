using UseCase1.Models;

namespace UseCase1.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();
    Task<IEnumerable<Country>> SearchCountriesAsync(string name);
}
