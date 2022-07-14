using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelListing.BLL.DTO.User;
using HotelListing.BLL.Interfaces;
using HotelListing.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing.BLL.Services;

public class AuthManager : IAuthManager

{
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;
    private ApiUser _user;

    public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateTokenAsync()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials _signingCredentials, List<Claim> _claims)
    {
        var jwtSettings = _configuration.GetSection("Jwt");

        DateTime dateTimeExpired =
            DateTime.Now.AddMinutes(Convert.ToInt32((string?)jwtSettings.GetSection("lifetime").Value));

        var token = new JwtSecurityToken(
            jwtSettings.GetSection("Issuer").Value,
            claims: _claims,
            expires: dateTimeExpired,
            signingCredentials: _signingCredentials
        );

        return token;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Environment.GetEnvironmentVariable(_configuration.GetSection("Jwt").GetSection("VariableName").Value);
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUserAsync(LoginUserDTO userDTO)
    {
        _user = await _userManager.FindByNameAsync(userDTO.Email);
        return _user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password);
    }

    private async Task<List<Claim>> GetClaimsAsync()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}