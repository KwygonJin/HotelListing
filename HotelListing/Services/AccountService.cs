using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.DTO.Mail;
using HotelListing.DTO.User;
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
        private readonly IMailService _mailService;

        public AccountService(UserManager<ApiUser> userManager,
            ILogger<AccountService> logger,
            IMapper mapper,
            IAuthManager authManager, 
            IMailService mailService)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _mailService = mailService;
        }

        public async Task RegisterAsync(UserDTO userDTO, ModelStateDictionary modelState)
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

            MailRequest request = new MailRequest
            {
                ToEmail = userDTO.Email,
                Subject = "Registration!",
                Body = "You successfully registred!"
            };
            await _mailService.SendEmailAsync(request);
        }

        public async Task<string> LoginAsync(LoginUserDTO userDTO)
        {
            if (!await _authManager.ValidateUserAsync(userDTO))
            {
                _logger.LogError($"Not authorized in the {nameof(LoginAsync)}");
                throw new System.NotImplementedException();
            }
            string token = await _authManager.CreateTokenAsync();
            return token;
        }
    }
}
