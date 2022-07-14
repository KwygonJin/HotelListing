﻿using AutoMapper;
using HotelListing.BLL.DTO.Hotel;
using HotelListing.BLL.Interfaces;
using HotelListing.DAL.Entities;
using HotelListing.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelListing.BLL.Services;

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

    public async Task<IList<HotelDTO>> GetHotelsAsync()
    {
        var hotels = await _unitOfWork.Hotels.GetAllAsync(include: q => q.Include(x => x.Country));
        return _mapper.Map<IList<HotelDTO>>(hotels);
    }


    public async Task<HotelDTO> GetHotelByIdAsync(int id)
    {
        var hotel = await _unitOfWork.Hotels.GetAsync(q => q.Id == id, q => q.Include(x => x.Country));
        if (hotel == null)
        {
            _logger.LogError($"Invalid UPDATE attempt in the {nameof(UpadateHotelAsync)}");
            throw new KeyNotFoundException();
        }

        return _mapper.Map<HotelDTO>(hotel);
    }

    public async Task<Hotel> CreateHotelAsync(CreateHotelDTO HotelDTO)
    {
        var hotel = _mapper.Map<Hotel>(HotelDTO);
        await _unitOfWork.Hotels.InsertAsync(hotel);
        await _unitOfWork.SaveAsync();

        return hotel;
    }

    public async Task<Hotel> UpadateHotelAsync(int id, UpdateHotelDTO hotelDTO)
    {
        var hotel = await _unitOfWork.Hotels.GetAsync(q => q.Id == id);
        if (hotel == null)
        {
            _logger.LogError($"Invalid UPDATE attempt in the {nameof(UpadateHotelAsync)}");
            throw new KeyNotFoundException();
        }

        _mapper.Map(hotelDTO, hotel);
        _unitOfWork.Hotels.Update(hotel);
        await _unitOfWork.SaveAsync();

        return hotel;
    }

    public async Task DeleteHotelAsync(int id)
    {
        var hotel = await _unitOfWork.Hotels.GetAsync(q => q.Id == id);
        if (hotel == null)
        {
            _logger.LogError($"Invalid Delete attempt in the {nameof(DeleteHotelAsync)}");
            throw new KeyNotFoundException();
        }

        await _unitOfWork.Hotels.DeleteAsync(id);
        await _unitOfWork.SaveAsync();
    }
}