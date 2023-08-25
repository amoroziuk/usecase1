using Microsoft.AspNetCore.Mvc;
using Moq;
using UseCase1.Controllers;
using UseCase1.Models;
using UseCase1.Services;
using Xunit;

namespace UserCase1.Tests.Controllers;

public class CountriesControllerTests
{
    private readonly Mock<ICountryService> _mockCountryService;
    private readonly CountriesController _controller;

    public CountriesControllerTests()
    {
        _mockCountryService = new Mock<ICountryService>();
        _controller = new CountriesController(_mockCountryService.Object);
    }

    [Fact]
    public async Task FetchCountries_ReturnsOk_WhenCalledWithValidRequestModel()
    {
        // Arrange
        var requestModel = new RequestModel();
        var countries = new List<Country> { new Country { Name = new Name { Common = "USA" } } };
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(requestModel)).ReturnsAsync(countries);

        // Act
        var result = await _controller.FetchCountries(requestModel);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Country>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal("USA", returnValue[0].Name.Common);
    }

    [Fact]
    public async Task FetchCountries_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        var requestModel = new RequestModel();
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(requestModel)).ThrowsAsync(new Exception("Some error"));

        // Act
        var result = await _controller.FetchCountries(requestModel);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Some error", badRequestResult.Value);
    }

    [Fact]
    public async Task FetchByName_ReturnsOk_WhenCalledWithValidRequest()
    {
        // Arrange
        var countries = new List<Country> { new Country { Name = new Name { Common = "India" } } };
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(It.IsAny<RequestModel>())).ReturnsAsync(countries);

        // Act
        var result = await _controller.FetchByName("India");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Country>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal("India", returnValue[0].Name.Common);
    }

    [Fact]
    public async Task FetchByPopulation_ReturnsOk_WhenCalledWithValidRequest()
    {
        // Arrange
        var countries = new List<Country> { new Country { Name = new Name { Common = "India" }, Population = 1000000 } };
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(It.IsAny<RequestModel>())).ReturnsAsync(countries);

        // Act
        var result = await _controller.FetchByPopulation(1000000);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Country>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal(1000000, returnValue[0].Population);
    }

    [Fact]
    public async Task FetchByName_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(It.IsAny<RequestModel>()))
                           .ThrowsAsync(new Exception("Invalid name"));

        // Act
        var result = await _controller.FetchByName("InvalidName");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid name", badRequestResult.Value);
    }

    [Fact]
    public async Task FetchByPopulation_ReturnsBadRequest_WhenExceptionIsThrown()
    {
        // Arrange
        _mockCountryService.Setup(x => x.GetFilteredCountriesAsync(It.IsAny<RequestModel>()))
                           .ThrowsAsync(new Exception("Invalid population"));

        // Act
        var result = await _controller.FetchByPopulation(-1); // Invalid population

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid population", badRequestResult.Value);
    }
}
