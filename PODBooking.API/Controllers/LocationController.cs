using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.LocationModels;
using Services.Models.PodModels;
using Services.Services;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/locations")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewLocation([FromBody] LocationCreateModel locationCreateModel)
        {
            var result = await _locationService.CreateLocationAsync(locationCreateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var result = await _locationService.GetLocationByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLocations([FromQuery] LocationFilterModel filterModel)
        {
            try
            {
                var result = await _locationService.GetAllLocationAsync(filterModel);
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
        public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] LocationUpdateModel locationUpdateModel)
        {
            var result = await _locationService.UpdateLocationAsync(id, locationUpdateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var result = await _locationService.DeleteLocationAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

}
