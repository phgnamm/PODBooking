using Microsoft.AspNetCore.Mvc;
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
    }
}
