using HotelListing.Data;
using HotelListing.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IHotelService
    {
        Task<IList<HotelDTO>> GetHotels();

        Task<HotelDTO> GetHotelById(int id);

        Task<Hotel> CreateHotel(CreateHotelDTO hotelDTO, ModelStateDictionary modelState);
    }
}
