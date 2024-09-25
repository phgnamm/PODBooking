using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.DeviceModels;
using Services.Models.ServiceModels;
using Services.Services;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/services")]
    public class ServiceController : Controller
    {
        public readonly IService _service;
        public ServiceController(IService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewService([FromBody] ServiceCreateModel serviceCreateModel)
        {
            var result = await _service.CreateServiceAsync(serviceCreateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            var result = await _service.GetServiceByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllServices([FromQuery] ServiceFilterModel filterModel)
        {
            try
            {
                var result = await _service.GetAllServiceAsync(filterModel);
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
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceUpdateModel serviceUpdateModel)
        {
            var result = await _service.UpdateServiceAsync(id, serviceUpdateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var result = await _service.DeleteServiceAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
