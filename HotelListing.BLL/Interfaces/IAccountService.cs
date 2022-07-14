using HotelListing.BLL.DTO.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelListing.BLL.Interfaces;

public interface IAccountService
{
    public Task RegisterAsync(UserDTO userDTO, ModelStateDictionary modelState);
    public Task<string> LoginAsync(LoginUserDTO userDTO);
}