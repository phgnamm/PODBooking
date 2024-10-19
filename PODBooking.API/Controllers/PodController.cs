using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Entities;
using Services.Interfaces;
using Services.Models.DeviceModels;
using Services.Models.PodModels;
using Services.Models.RatingModels;
using Services.Services;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/pods")]
    public class PodController : Controller
    {
        private readonly IPodService _podService;

        public PodController(IPodService podService)
        {
            _podService = podService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPods([FromQuery] PodFilterModel filterModel)
        {
            try
            {
                var result = await _podService.GetAllPodsAsync(filterModel);
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
        [HttpPost]
        public async Task<IActionResult> CreateNewPod([FromBody] PodCreateModel podCreateModel)
        {
            var result = await _podService.CreatePodAsync(podCreateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePod(Guid id, [FromBody] PodUpdateModel model)
        {
            var result = await _podService.UpdatePodAsync(id, model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePod(Guid id)
        {
            var result = await _podService.DeletePodAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodById(Guid id)
        {
            var result = await _podService.GetPodByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
