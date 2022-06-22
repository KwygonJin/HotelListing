using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;

namespace HotelListing.Configurations
{
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>()
                .ForMember(dest => dest.SumForWeek, opt => opt.MapFrom(src => src.Price * 7))
                .ForMember(dest => dest.DescriptionSum, opt => opt.MapFrom(src => "Price * 7 nigths!"))
                .ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
        }
    }
}
