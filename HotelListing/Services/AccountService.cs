using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.Interfaces;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountService(UserManager<ApiUser> userManager,
            ILogger<AccountService> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }

        public async Task Login(LoginUserDTO userDTO, ModelStateDictionary modelState)
        {

        }

        public async Task Register(UserDTO userDTO, ModelStateDictionary modelState)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            if (!modelState.IsValid)
            {
                throw new System.NotImplementedException();
            }
            
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                    }
                    throw new System.NotImplementedException();
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                throw new System.NotImplementedException();
            }
        }
    }
}
