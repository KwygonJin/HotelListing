using HotelListing.BLL.DTO.Hotel;
using HotelListing.DAL.Entities;

namespace HotelListing.BLL.Interfaces;

public interface IHotelService
{
    Task<IList<HotelDTO>> GetHotelsAsync();

    Task<HotelDTO> GetHotelByIdAsync(int id);

    Task<Hotel> CreateHotelAsync(CreateHotelDTO hotelDTO);

    Task<Hotel> UpadateHotelAsync(int id, UpdateHotelDTO hotelDTO);

    Task DeleteHotelAsync(int id);
}