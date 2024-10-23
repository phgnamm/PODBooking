using AutoMapper;
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

        public RewardPointService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<RewardPointModel>> GetAllRewardPointsAsync(RewardPointFilterModel model)
        {
            var queryResult = await _unitOfWork.RewardPointsRepository.GetAllAsync(
                filter: p => (model.Points == 0 || p.Points == model.Points) &&
                             (string.IsNullOrEmpty(model.Description) || p.Description.Contains(model.Description)),
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
    }
}
