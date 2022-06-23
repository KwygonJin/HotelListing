using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.Interfaces;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryService> _logger;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, ILogger<CountryService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task<IList<CountryDTO>> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll(include: q => q.Include(x => x.Hotels));
                return _mapper.Map<IList<CountryDTO>>(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountries)}");
                throw new System.NotImplementedException();
            }
        }

        public async Task<CountryDTO> GetCountryById(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id, include: q => q.Include(x => x.Hotels));
                return _mapper.Map<CountryDTO>(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountryById)}");
                throw new System.NotImplementedException();
            }
        }
    }
}
