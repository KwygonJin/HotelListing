using HotelListing.DTO.Hotel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTO.Country
{
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> Hotels { get; set; }
    }
}
