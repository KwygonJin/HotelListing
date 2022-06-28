using AutoMapper;
using HotelListing.DTO;
using HotelListing.DTO.Country;
using HotelListing.Interfaces;
using HotelListing.IRepository;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        //[ResponseCache(CacheProfileName = "120SecondsDuration")]       
        //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 90)]
        //[HttpCacheValidation(MustRevalidate = false)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries([FromQuery] RequestParams requestParams)
        {
            var results = await _countryService.GetCountriesAsync(requestParams);
            return Ok(results);
        }

        [Authorize]
        [HttpGet("{id:int}", Name = "GetCountry")]
        [ResponseCache(CacheProfileName = "120SecondsDuration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var result = await _countryService.GetCountryByIdAsync(id);
            return Ok(result);
        }

        //[Authorize(Roles = "Administrator")]
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateCountry)}");
                return BadRequest();
            }

            var country = await _countryService.CreateCountryAsync(countryDTO);
            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO countryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid Put attempt in {nameof(UpdateCountry)}");
                return BadRequest();
            }

            var country = await _countryService.UpdateCountryAsync(id, countryDTO);
            return CreatedAtRoute("GetCountry", new { id = country.Id }, country);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid Delete attempt in {nameof(DeleteCountry)}");
                return BadRequest();
            }

            await _countryService.DeleteCountryAsync(id);
            return Ok();
        }

    }
}
