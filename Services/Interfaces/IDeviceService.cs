using Repositories.Models.DeviceModels;
using Services.Common;
using Services.Models.DeviceModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDeviceService
    {
        Task<ResponseModel> CreateDeviceAsync(DeviceCreateModel deviceCreateModel);
        Task<ResponseDataModel<DeviceModel>> GetDeviceByIdAsync(Guid deviceId);
        Task<Pagination<DeviceModel>> GetAllDeviceAsync(DeviceFilterModel deviceFilterModel);
        Task<ResponseModel> UpdateDeviceAsync(Guid deviceId, DeviceUpdateModel deviceUpdateModel);
        Task<ResponseModel> DeleteDeviceAsync(Guid deviceId);
    }
}
