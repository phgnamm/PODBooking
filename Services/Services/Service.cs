using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.DeviceModels;
using Repositories.Models.ServiceModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.DeviceModels;
using Services.Models.ResponseModels;
using Services.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class Service : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public Service(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel> CreateServiceAsync(ServiceCreateModel serviceCreateModel)
        {
            var service = _mapper.Map<Repositories.Entities.Service>(serviceCreateModel);
            await _unitOfWork.ServiceRepository.AddAsync(service);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Create sucessfully"
            };
        }

        public async Task<ResponseModel> DeleteServiceAsync(Guid serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetAsync(serviceId);
            if (service == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Can't find service in database"
                };

            }
            _unitOfWork.ServiceRepository.SoftDelete(service);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Delete successful"
            };
        }

        public async Task<Pagination<ServiceModel>> GetAllServiceAsync(ServiceFilterModel serviceFilterModel)
        {
            var queryResult = await _unitOfWork.ServiceRepository.GetAllAsync(
         filter: p => (p.IsDeleted == serviceFilterModel.isDelete) && (serviceFilterModel.UnitPrice == 0 || p.UnitPrice == serviceFilterModel.UnitPrice) &&
                      (serviceFilterModel.Name == null || p.Name.Contains(serviceFilterModel.Name)),
         pageIndex: serviceFilterModel.PageIndex,
         pageSize: serviceFilterModel.PageSize
     );

            var services = _mapper.Map<List<ServiceModel>>(queryResult.Data);
            return new Pagination<ServiceModel>(services, serviceFilterModel.PageIndex, serviceFilterModel.PageSize, queryResult.TotalCount);
        }

        public async Task<ResponseDataModel<ServiceModel>> GetServiceByIdAsync(Guid serviceId)
        {
            var service = await _unitOfWork.ServiceRepository.GetAsync(serviceId);
            if (service == null)
            {
                return new ResponseDataModel<ServiceModel>
                {
                    Status = false,
                    Message = "No Service in database"
                };
            }
            if(service.IsDeleted == true)
            {
                return new ResponseDataModel<ServiceModel>
                {
                    Status = false,
                    Message = "Service is deleted"
                };
            }
            var serviceModel = _mapper.Map<ServiceModel>(service);
            return new ResponseDataModel<ServiceModel>
            {
                Status = true,
                Data = serviceModel
            };
        }

        public async Task<ResponseModel> UpdateServiceAsync(Guid serviceId, ServiceUpdateModel serviceUpdateModel)
        {
            var service = await _unitOfWork.ServiceRepository.GetAsync(serviceId);
            if (service == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Can't find service in database"
                };
            }
            _mapper.Map(serviceUpdateModel, service);
            _unitOfWork.ServiceRepository.Update(service);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Update successfully"
            };
        }

    }
}
