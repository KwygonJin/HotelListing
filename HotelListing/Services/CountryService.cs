using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.DTO.Country;
using HotelListing.Interfaces;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public async Task<Country> CreateCountry(CreateCountryDTO CountryDTO)
        {
            try
            {
                var country = _mapper.Map<Country>(CountryDTO);
                await _unitOfWork.Countries.Insert(country);
                await _unitOfWork.Save();

                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateCountry)}");
                throw new System.NotImplementedException();
            }
        }

        public async Task<Country> UpdateCountry(int id, UpdateCountryDTO countryDTO)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in the {nameof(UpdateCountry)}");
                    throw new System.NotImplementedException();
                }

                _mapper.Map(countryDTO, country);
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.Save();

                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateCountry)}");
                throw new System.NotImplementedException();
            }
        }

        public async Task DeleteCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(q => q.Id == id);
                if (country == null)
                {
                    _logger.LogError($"Invalid Delete attempt in the {nameof(DeleteCountry)}");
                    throw new System.NotImplementedException();
                }

                await _unitOfWork.Countries.Delete(id);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteCountry)}");
                throw new System.NotImplementedException();
            }
        }
    }
}
