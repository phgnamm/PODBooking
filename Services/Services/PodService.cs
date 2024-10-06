using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.PodModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.DeviceModels;
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

        public async Task<ResponseModel> CreatePodAsync(PodCreateModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Name))
            {
                return new ResponseModel { Status = false, Message = "Invalid input data" };
            }
            var location = await _unitOfWork.LocationRepository.GetAsync(model.LocationId);
            var device = await _unitOfWork.DeviceRepository.GetAsync(model.DeviceId);

            if (location == null || device == null)
            {
                return new ResponseModel { Status = false, Message = "Invalid Location or Device" };
            }

            var pod = _mapper.Map<Pod>(model);
            await _unitOfWork.PodRepository.AddAsync(pod);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel { Status = true, Message = "Pod created successfully" };
        }

        public async Task<ResponseModel> DeletePodAsync(Guid id)
        {
            var pod = await _unitOfWork.PodRepository.GetAsync(id);
            if (pod == null) return new ResponseModel { Status = false, Message = "Pod not found" };

            _unitOfWork.PodRepository.SoftDelete(pod);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel { Status = true, Message = "Pod deleted successfully" };
        }

        public async Task<Pagination<PodModel>> GetAllPodsAsync(PodFilterModel filterModel)
        {
            var queryResult = await _unitOfWork.PodRepository.GetAllAsync(
                filter: p =>( p.IsDeleted == filterModel.isDelete) && (filterModel.LocationId == null || p.LocationId == filterModel.LocationId) &&
                             (filterModel.DeviceId == null || p.DeviceId == filterModel.DeviceId) &&
                             (filterModel.MinPricePerHour == null || p.PricePerHour >= filterModel.MinPricePerHour) &&
                             (filterModel.MaxPricePerHour == null || p.PricePerHour <= filterModel.MaxPricePerHour) &&
                             (filterModel.MinArea == null || p.Area >= filterModel.MinArea) &&
                             (filterModel.MaxArea == null || p.Area <= filterModel.MaxArea) &&
                             (filterModel.MinCapacity == null || p.Capacity >= filterModel.MinCapacity) &&
                             (filterModel.MaxCapacity == null || p.Capacity <= filterModel.MaxCapacity) &&
                             (string.IsNullOrEmpty(filterModel.Floor) || p.Device.Floor == filterModel.Floor),
                include: "Location, Device",
                pageIndex: filterModel.PageIndex,
                pageSize: filterModel.PageSize
            );

            var pods = _mapper.Map<List<PodModel>>(queryResult.Data);
            return new Pagination<PodModel>(pods, filterModel.PageIndex, filterModel.PageSize, queryResult.TotalCount);
        }

        public async Task<ResponseDataModel<PodModel>> GetPodByIdAsync(Guid id)
        {
            var pod = await _unitOfWork.PodRepository.GetAsync(id,include: "Location,Device");
            if (pod == null) return new ResponseDataModel<PodModel> { Status = false, Message = "Pod not found" };
            if(pod.IsDeleted == true) return new ResponseDataModel<PodModel> { Status = false, Message = "Pod is deleted" };

            var podModel = _mapper.Map<PodModel>(pod);
            return new ResponseDataModel<PodModel> { Status = true, Data = podModel };
        }

        public async Task<ResponseModel> UpdatePodAsync(Guid id, PodUpdateModel model)
        {
            var pod = await _unitOfWork.PodRepository.GetAsync(id);
            if (pod == null) return new ResponseModel { Status = false, Message = "Pod not found" };

            var location = await _unitOfWork.LocationRepository.GetAsync(model.LocationId);
            var device = await _unitOfWork.DeviceRepository.GetAsync(model.DeviceId);

            if (location == null || device == null)
            {
                return new ResponseModel { Status = false, Message = "Invalid Location or Device" };
            }

            _mapper.Map(model, pod);
            _unitOfWork.PodRepository.Update(pod);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel { Status = true, Message = "Pod updated successfully" };
        }
    }
}
