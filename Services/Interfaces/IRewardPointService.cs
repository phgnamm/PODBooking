using Repositories.Models.BookingModels;
using Repositories.Models.RewardPointModels;
using Services.Common;
using Services.Models.BookingModels;
using Services.Models.ResponseModels;
using Services.Models.RewardPointModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRewardPointService
    {
        Task<Pagination<RewardPointModel>> GetAllRewardPointsAsync(RewardPointFilterModel model);
        Task<ResponseDataModel<RewardPointModel>> GetRewardByIdAsync(Guid id);
        Task<ResponseModel> UpdateRewardPointAsync(Guid id, RewardPointUpdateModel model);
        Task<ResponseModel> RemoveRewardPointAsync(Guid id);
        Task<ResponseDataModel<IntWrapper>> GetTotalRewardPointsByAccountIdAsync(Guid accountId);
    
}
}
