using HotelListing.BLL.DTO.Country;

namespace HotelListing.BLL.DTO.Hotel;

public class HotelDTO : CreateHotelDTO
{
    public int Id { get; set; }
    public CountryDTO Country { get; set; }
    public decimal SumForWeek { get; set; }
    public string DescriptionSum { get; set; }
}