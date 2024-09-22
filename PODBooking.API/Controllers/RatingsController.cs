using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;
using Services.Models.RatingModels;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/ratings")]
    public class RatingsController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRatingsByPod([FromQuery] RatingFilterModel model)
        {
            var result = await _ratingService.GetRatingsByPodAsync(model);
            var metadata = new
            {
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRating([FromBody] RatingCreateModel ratingCreateModel)
        {
            var result = await _ratingService.CreateRating(ratingCreateModel);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
