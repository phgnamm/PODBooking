using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace PODBooking.API.Controllers
{
    [Route("api/v1/dashboard")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("revenue-stats")]
        public async Task<IActionResult> GetRevenueStats()
        {
            var stats = await _dashboardService.GetRevenueStatsAsync();
            return Ok(stats);
        }

    }

}
