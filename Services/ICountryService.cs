using UseCase1.Models;

namespace UseCase1.Services;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllCountriesAsync(int? limit = null);
    Task<IEnumerable<Country>> GetFilteredCountriesAsync(RequestModel model);
}
