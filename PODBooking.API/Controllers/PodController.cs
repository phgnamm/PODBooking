using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.PodModels;

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
    }
}
