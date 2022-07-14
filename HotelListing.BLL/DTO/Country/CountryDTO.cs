using HotelListing.BLL.DTO.Hotel;

namespace HotelListing.BLL.DTO.Country
{
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> Hotels { get; set; }
    }
}
