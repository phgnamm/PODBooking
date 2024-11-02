using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.DeviceModels;
using Services.Models.LocationModels;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/devices")]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpPost]
        public async Task<IActionResult> CreateNewDevice([FromBody] DeviceCreateModel deviceCreateModel)
        {
            var result = await _deviceService.CreateDeviceAsync(deviceCreateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceById(Guid id)
        {
            var result = await _deviceService.GetDeviceByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDevices([FromQuery] DeviceFilterModel filterModel)
        {
            try
            {
                var result = await _deviceService.GetAllDeviceAsync(filterModel);
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
        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(Guid id, [FromBody] DeviceUpdateModel deviceUpdateModel)
        {
            var result = await _deviceService.UpdateDeviceAsync(id, deviceUpdateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [Authorize(Roles = "Admin,Manager,Staff")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            var result = await _deviceService.DeleteDeviceAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
