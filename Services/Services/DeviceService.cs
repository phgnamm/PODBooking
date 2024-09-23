using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.DeviceModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.DeviceModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeviceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreateDeviceAsync(DeviceCreateModel deviceCreateModel)
        {
            var device = _mapper.Map<Device>(deviceCreateModel);
            await _unitOfWork.DeviceRepository.AddAsync(device);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Create sucessfully"
            };
        }

        public async Task<ResponseModel> DeleteDeviceAsync(Guid deviceId)
        {
            var device = await _unitOfWork.DeviceRepository.GetAsync(deviceId);
            if (device == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Can't find device in database"
                };

            }
            _unitOfWork.DeviceRepository.SoftDelete(device);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Delete successful"
            };
        }

        public async Task<Pagination<DeviceModel>> GetAllDeviceAsync(DeviceFilterModel deviceFilterModel)
        {
            var queryResult = await _unitOfWork.DeviceRepository.GetAllAsync(
         filter: p => (deviceFilterModel.Floor== null || p.Floor == deviceFilterModel.Floor) &&
                      (deviceFilterModel.RoomType == null || p.RoomType.Contains(deviceFilterModel.RoomType)),
         pageIndex: deviceFilterModel.PageIndex,
         pageSize: deviceFilterModel.PageSize
     );

            var devices = _mapper.Map<List<DeviceModel>>(queryResult.Data);
            return new Pagination<DeviceModel>(devices, deviceFilterModel.PageIndex, deviceFilterModel.PageSize, queryResult.TotalCount);
        }

        public async Task<ResponseDataModel<DeviceModel>> GetDeviceByIdAsync(Guid deviceId)
        {
            var device= await _unitOfWork.DeviceRepository.GetAsync(deviceId);
            if (device == null)
            {
                return new ResponseDataModel<DeviceModel>
                {
                    Status = false,
                    Message = "No Device in database"
                };
            }
            var deviceModel = _mapper.Map<DeviceModel>(device);
            return new ResponseDataModel<DeviceModel>
            {
                Status = true,
                Data = deviceModel
            };
        }

        public async Task<ResponseModel> UpdateDeviceAsync(Guid deviceId, DeviceUpdateModel deviceUpdateModel)
        {
             var device = await _unitOfWork.DeviceRepository.GetAsync(deviceId);
            if (device == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Can't find device in database"
                };
            }
            _mapper.Map(deviceUpdateModel, device);
            _unitOfWork.DeviceRepository.Update(device);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Update successfully"
            };
        }
    }
}
