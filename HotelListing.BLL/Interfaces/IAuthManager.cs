using HotelListing.BLL.DTO.User;

namespace HotelListing.BLL.Interfaces;

public interface IAuthManager
{
    Task<bool> ValidateUserAsync(LoginUserDTO userDTO);
    Task<string> CreateTokenAsync();
}