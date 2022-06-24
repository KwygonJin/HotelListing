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
        Task<IList<CountryDTO>> GetCountries();

        Task<CountryDTO> GetCountryById(int id);

        Task<Country> CreateCountry(CreateCountryDTO countryDTO);
    }
}
