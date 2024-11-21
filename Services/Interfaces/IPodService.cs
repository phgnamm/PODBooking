using Repositories.Models.PodModels;
using Services.Common;
using Services.Models.PodModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPodService
    {
        Task<Pagination<PodModel>> GetAllPodsAsync(PodFilterModel filterModel);
        Task<ResponseModel> CreatePodAsync(PodCreateModel model);
        Task<ResponseModel> UpdatePodAsync(Guid id, PodUpdateModel model);
        Task<ResponseModel> DeletePodAsync(Guid id);
        Task<ResponseDataModel<PodModel>> GetPodByIdAsync(Guid id);
        Task<ResponseModel> RestorePOD(Guid id);

    }
}
