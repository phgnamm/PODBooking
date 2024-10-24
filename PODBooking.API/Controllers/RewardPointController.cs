using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Models.RewardPointModels;
using Services.Interfaces;
using Services.Models.RewardPointModels;
using System;
using System.Threading.Tasks;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/rewardpoints")]
    public class RewardPointController : Controller
    {
        private readonly IRewardPointService _rewardPointService;

        public RewardPointController(IRewardPointService rewardPointService)
        {
            _rewardPointService = rewardPointService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRewardPointById(Guid id)
        {
            var result = await _rewardPointService.GetRewardByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRewardPoints([FromQuery] RewardPointFilterModel filterModel)
        {
            try
            {
                var result = await _rewardPointService.GetAllRewardPointsAsync(filterModel);
                var metadata = new
                {
                    result.PageSize,
                    result.CurrentPage,
                    result.TotalPages,
                };

                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRewardPoint(Guid id, [FromBody] RewardPointUpdateModel updateModel)
        {
            var result = await _rewardPointService.UpdateRewardPointAsync(id, updateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRewardPoint(Guid id)
        {
            var result = await _rewardPointService.RemoveRewardPointAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
