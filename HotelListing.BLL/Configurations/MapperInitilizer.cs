using AutoMapper;
using HotelListing.BLL.DTO.Country;
using HotelListing.BLL.DTO.Hotel;
using HotelListing.BLL.DTO.User;
using HotelListing.DAL.Entities;

namespace HotelListing.BLL.Configurations;

public class MapperInitilizer : Profile
{
    private static int NumberDaysOfWeek = 7;

    public MapperInitilizer()
    {
        CreateMap<Country, CountryDTO>().ReverseMap();
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
        CreateMap<Hotel, HotelDTO>()
            .ForMember(dest => dest.SumForWeek, opt => opt.MapFrom(src => src.Price * NumberDaysOfWeek))
            .ForMember(dest => dest.DescriptionSum, opt => opt.MapFrom(src => $"Price * {NumberDaysOfWeek} nigths!"))
            .ReverseMap();
        CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        CreateMap<ApiUser, UserDTO>()
            .ReverseMap()
            .ForPath(s => s.UserName, opt => opt.MapFrom(src => src.Email));
    }
}