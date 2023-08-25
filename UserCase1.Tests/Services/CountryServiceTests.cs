using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using UseCase1.Models;
using UseCase1.Services;
using Xunit;

namespace UserCase1.Tests.Services;

public class CountryServiceTests
{
    private readonly Mock<ILogger<CountryService>> _mockLogger;
    private readonly Mock<IConfiguration> _mockConfiguration; 
    private readonly Mock<MockHttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly CountryService _countryService;

    public CountryServiceTests()
    {
        _mockLogger = new Mock<ILogger<CountryService>>();
        _mockConfiguration = new Mock<IConfiguration>();
        var inMemorySettings = new Dictionary<string, string> {
            {"CountryApiBaseUrl", "https://example.com"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _mockHttpMessageHandler = new Mock<MockHttpMessageHandler> { CallBase = true };
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        _countryService = new CountryService(configuration, _mockLogger.Object, _httpClient);
    }

    [Fact]
    public async Task GetAllCountriesAsync_ShouldReturnCountries_WhenApiCallSucceeds()
    {
        // Arrange
        var countries = new List<Country> { new Country { Name = new Name { Common = "USA" } } };
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(countries)),
        };
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(httpResponse);

        // Act
        var result = await _countryService.GetAllCountriesAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("USA", result.First().Name.Common);
    }

    [Fact]
    public async Task GetAllCountriesAsync_ShouldThrowException_WhenApiCallFails()
    {
        // Arrange
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Content = new StringContent(string.Empty),
        };
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(httpResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _countryService.GetAllCountriesAsync());
    }

    [Fact]
    public async Task GetFilteredCountriesAsync_ShouldReturnFilteredCountries()
    {
        // Arrange
        var countries = new List<Country>
        {
            new Country { Name = new Name { Common = "USA" }, Population = 1000000 },
            new Country { Name = new Name { Common = "Canada" }, Population = 500000 },
        };
        var requestModel = new RequestModel { Name = "USA", Population = 1000, Sort = "ascend", Limit = 1 };

        // Mock GetAllCountriesAsync to return the predefined list of countries
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(countries)),
            });

        // Act
        var result = await _countryService.GetFilteredCountriesAsync(requestModel);

        // Assert
        Assert.Single(result);
        Assert.Equal("USA", result.First().Name.Common);
    }

    [Fact]
    public async Task GetFilteredCountriesAsync_ShouldReturnFilteredCountries_WithoutPopulatio()
    {
        // Arrange
        var countries = new List<Country>
        {
            new Country { Name = new Name { Common = "USA" }, Population = 1000000 },
            new Country { Name = new Name { Common = "Canada" }, Population = 500000 },
        };
        var requestModel = new RequestModel { Name = "USA", Population = null, Sort = "ascend", Limit = 1 };

        // Mock GetAllCountriesAsync to return the predefined list of countries
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(countries)),
            });

        // Act
        var result = await _countryService.GetFilteredCountriesAsync(requestModel);

        // Assert
        Assert.Single(result);
        Assert.Equal("USA", result.First().Name.Common);
    }

    [Fact]
    public async Task GetFilteredCountriesAsync_ShouldReturnFilteredAsnSortedCountries()
    {
        // Arrange
        var countries = new List<Country>
        {
            new Country { Name = new Name { Common = "USA" }, Population = 1000000 },
            new Country { Name = new Name { Common = "Canada" }, Population = 500000 },
        };
        var requestModel = new RequestModel { Name = "USA", Population = 1000, Sort = "descend", Limit = 1 };

        // Mock GetAllCountriesAsync to return the predefined list of countries
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(countries)),
            });

        // Act
        var result = await _countryService.GetFilteredCountriesAsync(requestModel);

        // Assert
        Assert.Single(result);
        Assert.Equal("USA", result.First().Name.Common);
    }

    [Fact]
    public async Task GetAllCountriesAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Throws(new HttpRequestException());

        // Act
        await Assert.ThrowsAsync<HttpRequestException>(() => _countryService.GetAllCountriesAsync());

        // Assert
        _mockLogger.Verify(
            x => x.LogError(
                It.IsAny<string>(),
                It.IsAny<string>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task GetFilteredCountriesAsync_ShouldLogError_WhenExceptionThrown()
    {
        // Arrange
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Throws(new HttpRequestException());

        // Act
        await Assert.ThrowsAsync<HttpRequestException>(() => _countryService.GetFilteredCountriesAsync(new RequestModel()));

        // Assert
        _mockLogger.Verify(
            x => x.LogError(
                It.IsAny<string>(),
                It.IsAny<string>()
            ),
            Times.Once
        );
    }

    [Fact]
    public async Task GetFilteredCountriesAsync_ShouldReturnEmptyList_WhenNoCountriesMatchFilters()
    {
        // Arrange
        var countries = new List<Country>
    {
        new Country { Name = new Name { Common = "USA" }, Population = 1000000 },
        new Country { Name = new Name { Common = "Canada" }, Population = 500000 }
    };

        var requestModel = new RequestModel { Name = "Germany", Population = 2000 };

        // Mock GetAllCountriesAsync to return the predefined list of countries
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(countries)),
            });

        // Act
        var result = await _countryService.GetFilteredCountriesAsync(requestModel);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task DeserializeCountries_ShouldReturnEmptyList_WhenApiResponseIsEmpty()
    {
        // Arrange
        _mockHttpMessageHandler.Setup(x => x.Send(It.IsAny<HttpRequestMessage>()))
            .Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new List<Country>()))
            });

        // Act
        var result = await _countryService.GetAllCountriesAsync();

        // Assert
        Assert.Empty(result);
    }
}
