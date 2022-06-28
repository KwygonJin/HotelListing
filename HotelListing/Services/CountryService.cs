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
        public async Task<IList<CountryDTO>> GetCountriesAsync()
        {
            var countries = await _unitOfWork.Countries.GetAllAsync(include: q => q.Include(x => x.Hotels));
            return _mapper.Map<IList<CountryDTO>>(countries);
        }

        public async Task<IList<CountryDTO>> GetCountriesAsync(RequestParams requestParams)
        {
            var countries = await _unitOfWork.Countries.GetPagedAsync(requestParams, q => q.Include(x => x.Hotels));
            return _mapper.Map<IList<CountryDTO>>(countries);
        }

        public async Task<CountryDTO> GetCountryByIdAsync(int id)
        {
            var country = await _unitOfWork.Countries.GetAsync(q => q.Id == id, include: q => q.Include(x => x.Hotels));
            if (country == null)
                throw new KeyNotFoundException();

            return _mapper.Map<CountryDTO>(country);
        }

        //some com
        public async Task<Country> CreateCountryAsync(CreateCountryDTO CountryDTO)
        {
            var country = _mapper.Map<Country>(CountryDTO);
            await _unitOfWork.Countries.InsertAsync(country);
            await _unitOfWork.SaveAsync();

            return country;
        }

        public async Task<Country> UpdateCountryAsync(int id, UpdateCountryDTO countryDTO)
        {
            var country = await _unitOfWork.Countries.GetAsync(q => q.Id == id);
            if (country == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in the {nameof(UpdateCountryAsync)}");
                throw new KeyNotFoundException();
            }

            _mapper.Map(countryDTO, country);
            _unitOfWork.Countries.Update(country);
            await _unitOfWork.SaveAsync();

            return country;
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _unitOfWork.Countries.GetAsync(q => q.Id == id);
            if (country == null)
            {
                _logger.LogError($"Invalid Delete attempt in the {nameof(DeleteCountryAsync)}");
                throw new KeyNotFoundException();
            }

            await _unitOfWork.Countries.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
