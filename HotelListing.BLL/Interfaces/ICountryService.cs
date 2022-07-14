using HotelListing.BLL.DTO.Country;
using HotelListing.DAL.Entities;

namespace HotelListing.BLL.Interfaces;

public interface ICountryService
{
    Task<IList<CountryDTO>> GetCountriesAsync();

    Task<CountryDTO> GetCountryByIdAsync(int id);

    Task<Country> CreateCountryAsync(CreateCountryDTO countryDTO);

    Task<Country> UpdateCountryAsync(int id, UpdateCountryDTO countryDTO);

    Task DeleteCountryAsync(int id);
}