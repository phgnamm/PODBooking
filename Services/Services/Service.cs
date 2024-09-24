using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
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

        public Task<ResponseModel> DeleteServiceAsync(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<ServiceModel>> GetAllServiceAsync(ServiceFilterModel serviceFilterModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDataModel<ServiceModel>> GetServiceByIdAsync(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> UpdateServiceAsync(Guid serviceId, ServiceUpdateModel serviceUpdateModel)
        {
            throw new NotImplementedException();
        }
    }
}
