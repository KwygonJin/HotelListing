using HotelListing.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IHotelService
    {
        Task<IList<HotelDTO>> GetHotels();

        Task<HotelDTO> GetHotelById(int id);
    }
}
