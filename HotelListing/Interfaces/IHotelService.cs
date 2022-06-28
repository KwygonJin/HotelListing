using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.DTO.Hotel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IHotelService
    {
        Task<IList<HotelDTO>> GetHotelsAsync();

        Task<IList<HotelDTO>> GetHotelsAsync(RequestParams requestParams);

        Task<HotelDTO> GetHotelByIdAsync(int id);

        Task<Hotel> CreateHotelAsync(CreateHotelDTO hotelDTO);

        Task<Hotel> UpadateHotelAsync(int id, UpdateHotelDTO hotelDTO);

        Task DeleteHotelAsync(int id);
    }
}
