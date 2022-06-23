using HotelListing.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface ICountryService
    {
        Task<IList<CountryDTO>> GetCountries();

        Task<CountryDTO> GetCountryById(int id);
    }
}
