using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.PodModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.PodModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PodService: IPodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreatNewPodsAsync(PodCreateModel podCreateModel)
        {
          var podEntity=_mapper.Map<Pod>(podCreateModel);
            await _unitOfWork.PodRepository.AddAsync(podEntity);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status=true,
                Message="Pod create successfully"
            };
        }

        public async Task<Pagination<PodModel>> GetAllPodsAsync(PodFilterModel filterModel)
        {
            var queryResult = await _unitOfWork.PodRepository.GetAllAsync(
                filter: p => (filterModel.LocationId == null || p.LocationId == filterModel.LocationId) &&
                             (filterModel.DeviceId == null || p.DeviceId == filterModel.DeviceId) &&
                             (filterModel.MinPricePerHour == null || p.PricePerHour >= filterModel.MinPricePerHour) &&
                             (filterModel.MaxPricePerHour == null || p.PricePerHour <= filterModel.MaxPricePerHour) &&
                             (filterModel.MinArea == null || p.Area >= filterModel.MinArea) &&
                             (filterModel.MaxArea == null || p.Area <= filterModel.MaxArea) &&
                             (filterModel.MinCapacity == null || p.Capacity >= filterModel.MinCapacity) &&
                             (filterModel.MaxCapacity == null || p.Capacity <= filterModel.MaxCapacity),
                include: "Location, Device",
                pageIndex: filterModel.PageIndex,
                pageSize: filterModel.PageSize
            );

            var pods = _mapper.Map<List<PodModel>>(queryResult.Data);
            return new Pagination<PodModel>(pods, filterModel.PageIndex, filterModel.PageSize, queryResult.TotalCount);
        }
    }
}
