using Microsoft.AspNetCore.Identity;

namespace HotelListing.DAL.Entities;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}