using Repositories.Models.DeviceModels;
using Repositories.Models.ServiceModels;
using Services.Common;
using Services.Models.DeviceModels;
using Services.Models.ResponseModels;
using Services.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IService
    {
        Task<ResponseModel> CreateServiceAsync(ServiceCreateModel serviceCreateModel);
        Task<ResponseDataModel<ServiceModel>> GetServiceByIdAsync(Guid serviceId);
        Task<Pagination<ServiceModel>> GetAllServiceAsync(ServiceFilterModel serviceFilterModel);
        Task<ResponseModel> UpdateServiceAsync(Guid serviceId, ServiceUpdateModel serviceUpdateModel);
        Task<ResponseModel> DeleteServiceAsync(Guid serviceId);
    }
}
