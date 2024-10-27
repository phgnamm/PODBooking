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
        [HttpGet("revenue/monthly")]
        public async Task<IActionResult> GetMonthlyRevenue(int month, int year)
        {
            var revenue = await _dashboardService.GetMonthlyRevenueAsync(month, year);
            return Ok(new { Month = month, Year = year, Revenue = revenue });
        }


    }

}
