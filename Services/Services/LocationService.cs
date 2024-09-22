﻿using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.LocationModels;
using Repositories.Models.PodModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.LocationModels;
using Services.Models.PodModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreateLocationAsync(LocationCreateModel locationCreateModel)
        {
            var location = _mapper.Map<Location>(locationCreateModel);
            await _unitOfWork.LocationRepository.AddAsync(location);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Create sucessfully"
            };
        }

        public async Task<Pagination<LocationModel>> GetAllLocationAsync(LocationFilterModel locationFilterModel)
        {
            var queryResult = await _unitOfWork.LocationRepository.GetAllAsync(
         filter: p => (locationFilterModel.Address == null || p.Address.Contains(locationFilterModel.Address)) &&
                      (locationFilterModel.Name == null || p.Name.Contains( locationFilterModel.Name)),
         pageIndex: locationFilterModel.PageIndex,
         pageSize: locationFilterModel.PageSize
     );

            var locations = _mapper.Map<List<LocationModel>>(queryResult.Data);
            return new Pagination<LocationModel>(locations, locationFilterModel.PageIndex, locationFilterModel.PageSize, queryResult.TotalCount);
        }

        public async Task<ResponseDataModel<LocationModel>> GetLocationByIdAsync(Guid locationId)
        {
            var location = await _unitOfWork.LocationRepository.GetAsync(locationId);
            if (location == null)
            {
                return new ResponseDataModel<LocationModel>
                {
                    Status = false,
                    Message = "No Location in database"
                };
            }
            var locationModel = _mapper.Map<LocationModel>(location);
            return new ResponseDataModel<LocationModel>
            {
                Status = true,
                Data = locationModel
            };
        }

    }
}