using HotelListing.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelListing.Interfaces
{
    public interface IAccountService
    {
        public Task Register(UserDTO userDTO);
        public Task Login(LoginUserDTO userDTO);
    }
}
