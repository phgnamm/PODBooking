using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.BookingModels;
using Services.Models.LocationModels;
using Services.Services;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/bookings")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid booking data.");
            }

            var result = await _bookingService.CreateBookingAsync(model);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookings([FromQuery] BookingFilterModel filterModel)
        {
            try
            {
                var result = await _bookingService.GetAllBookingsAsync(filterModel);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(Guid id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] BookingUpdateModel model)
        {
            var result = await _bookingService.UpdateBookingAsync(id, model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("{podId}/booked-times")]
        public async Task<IActionResult> GetBookedTimesByPod(Guid podId)
        {
            var result = await _bookingService.GetBookedTimesByPod(podId);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add-services-to-booking")]
        public async Task<IActionResult> AddServicesToBookingPod( [FromBody] BookingCreateServiceModel model)
        {
            var result = await _bookingService.AddServicesToBooking(model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
