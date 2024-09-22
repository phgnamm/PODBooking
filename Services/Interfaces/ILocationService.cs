using Repositories.Models.LocationModels;
using Services.Common;
using Services.Models.LocationModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILocationService
    {
        Task<ResponseModel> CreateLocationAsync(LocationCreateModel locationCreateModel);
        Task<ResponseDataModel<LocationModel>> GetLocationByIdAsync(Guid locationId);
        Task<Pagination<LocationModel>>GetAllLocationAsync(LocationFilterModel locationFilterModel);
    }
}
