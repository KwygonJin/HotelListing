using HotelListing.Data;
using HotelListing.DTO.Country;
using HotelListing.DTO.Hotel;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTO.Hotel
{
    public class HotelDTO : CreateHotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
        public decimal SumForWeek { get; set; }
        public string DescriptionSum { get; set; }
    }
}
