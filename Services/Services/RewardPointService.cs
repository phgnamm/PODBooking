using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.RewardPointModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.ResponseModels;
using Services.Models.RewardPointModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class RewardPointService : IRewardPointService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;

        public RewardPointService(UserManager<Account> userManager,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Pagination<RewardPointModel>> GetAllRewardPointsAsync(RewardPointFilterModel model)
        {
            var queryResult = await _unitOfWork.RewardPointsRepository.GetAllAsync(
         filter: p => (model.Points == 0 || p.Points == model.Points) &&
                      (string.IsNullOrEmpty(model.Description) || p.Description.Contains(model.Description)) &&
                      (model.AccountId == Guid.Empty || p.AccountId == model.AccountId),
         pageIndex: model.PageIndex,
         pageSize: model.PageSize
     );

            var rewardPoints = _mapper.Map<List<RewardPointModel>>(queryResult.Data);
            return new Pagination<RewardPointModel>(rewardPoints, model.PageIndex, model.PageSize, queryResult.TotalCount);
        }

        public async Task<ResponseDataModel<RewardPointModel>> GetRewardByIdAsync(Guid id)
        {
            var rewardPoint = await _unitOfWork.RewardPointsRepository.GetAsync(id); 
            if (rewardPoint == null)
            {
                return new ResponseDataModel<RewardPointModel>
                {
                    Status = false,
                    Message = "Reward point not found"
                };
            }

            var rewardPointModel = _mapper.Map<RewardPointModel>(rewardPoint);
            return new ResponseDataModel<RewardPointModel>
            {
                Status = true,
                Data = rewardPointModel
            };
        }

        public async Task<ResponseModel> RemoveRewardPointAsync(Guid id)
        {
            var rewardPoint = await _unitOfWork.RewardPointsRepository.GetAsync(id);
            if (rewardPoint == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Reward point not found"
                };
            }

            _unitOfWork.RewardPointsRepository.SoftDelete(rewardPoint);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Reward point removed successfully"
            };
        }

        public async Task<ResponseModel> UpdateRewardPointAsync(Guid id, RewardPointUpdateModel model)
        {
            var rewardPoint = await _unitOfWork.RewardPointsRepository.GetAsync(id);
            if (rewardPoint == null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Reward point not found"
                };
            }

            _mapper.Map(model, rewardPoint);
            _unitOfWork.RewardPointsRepository.Update(rewardPoint);
            await _unitOfWork.SaveChangeAsync();
            return new ResponseModel
            {
                Status = true,
                Message = "Reward point updated successfully"
            };
        }
        public async Task<ResponseDataModel<IntWrapper>> GetTotalRewardPointsByAccountIdAsync(Guid accountId)
        {
            var rewardPoints = await _unitOfWork.RewardPointsRepository.GetAllAsync(
                filter: rp => rp.AccountId == accountId && rp.IsDeleted == false);

            if (rewardPoints == null || rewardPoints.Data.Count == 0)
            {
                return new ResponseDataModel<IntWrapper>
                {
                    Status = false,
                    Message = "No active reward points found for this account",
                    Data = null
                };
            }

            int totalPoints = rewardPoints.Data.Sum(rp => rp.Points);
            var account = await _userManager.FindByIdAsync(accountId.ToString());
            var accountName = account.FirstName + " " + account.LastName;   
            return new ResponseDataModel<IntWrapper>
            {
                Status = true,
                Data = new IntWrapper 
                { 
                    Value = totalPoints,
                    AccountName = accountName,
                }
            };
        }




    }
}
