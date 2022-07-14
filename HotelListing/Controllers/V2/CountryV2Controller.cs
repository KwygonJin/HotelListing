using System.Threading.Tasks;
using HotelListing.BLL.DTO.Country;
using HotelListing.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListing.WEB.Controllers.V2;
//Hardcoded for testing api verisoning

[ApiVersion("2.0")]
//[ApiVersion("2.0", Deprecated = true)]
//1. Case, when youn change route
[Route("api/{v:apiversion}/country")]
//2. Case. when yo do not change rote, and add parametr api-version on request in client-side(api-version = 2.0)
//[Route("api/country")]
//3. Case, when you add opt.ApiVersionReader = new HeaderApiVersionReader("api-version"); in services.AddApiVersioning
//[Route("api/country")]
//
[ApiController]
public class CountryV2Controller : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly ILogger<CountryController> _logger;

    public CountryV2Controller(ICountryService countryService, ILogger<CountryController> logger)
    {
        _countryService = countryService;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountries()
    {
        var results = await _countryService.GetCountriesAsync();
        results.Add(new CountryDTO
        {
            Id = 12,
            Name = "Test country",
            ShortName = "TC"
        });
        return Ok(results);
    }
}