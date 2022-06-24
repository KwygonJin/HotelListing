using HotelListing.DTO;
using HotelListing.DTO.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IAccountService
    {
        public Task Register(UserDTO userDTO, ModelStateDictionary modelState);
        public Task<string> Login(LoginUserDTO userDTO);
    }
}
