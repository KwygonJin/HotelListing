using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO;
using HotelListing.Interfaces;
using HotelListing.IRepository;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HotelService> _logger;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, ILogger<HotelService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IList<HotelDTO>> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll(include: q => q.Include(x => x.Country));
                return _mapper.Map<IList<HotelDTO>>(hotels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotels)}");
                throw new System.NotImplementedException();
            }
        }

        public async Task<HotelDTO> GetHotelById(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(q => q.Id == id, include: q => q.Include(x => x.Country));
                return _mapper.Map<HotelDTO>(hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetHotelById)}");
                throw new System.NotImplementedException();
            }
        }

        public async Task<Hotel> CreateHotel(CreateHotelDTO HotelDTO, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateHotel)}");
                throw new System.NotImplementedException();
            }
            try
            {
                var hotel = _mapper.Map<Hotel>(HotelDTO);
                await _unitOfWork.Hotels.Insert(hotel);
                await _unitOfWork.Save();
                
                return hotel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CreateHotel)}");
                throw new System.NotImplementedException();
            }
        }
    }
}
