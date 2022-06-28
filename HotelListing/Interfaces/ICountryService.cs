using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.DTO.Country;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface ICountryService
    {
        Task<IList<CountryDTO>> GetCountriesAsync();

        Task<IList<CountryDTO>> GetCountriesAsync(RequestParams requestParams);

        Task<CountryDTO> GetCountryByIdAsync(int id);

        Task<Country> CreateCountryAsync(CreateCountryDTO countryDTO);

        Task<Country> UpdateCountryAsync(int id, UpdateCountryDTO countryDTO);

        Task DeleteCountryAsync(int id);
    }
}
