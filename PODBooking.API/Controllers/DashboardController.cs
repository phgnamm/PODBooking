using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models.DashboardModels;
using Services.Models.ResponseModels;

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
        [Authorize(Roles = "Admin")]
        [HttpGet("revenue-stats")]
        public async Task<IActionResult> GetRevenueStats()
        {
            var stats = await _dashboardService.GetRevenueStatsAsync();
            return Ok(stats);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("revenue/monthly")]
        public async Task<IActionResult> GetMonthlyRevenue(int month, int year)
        {
            var revenue = await _dashboardService.GetMonthlyRevenueAsync(month, year);
            return Ok(new { Month = month, Year = year, Revenue = revenue });
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("revenue-by-pod")]
        public async Task<IActionResult> GetRevenueByPod()
        {
            var result = await _dashboardService.GetRevenueByPodAsync();
            return Ok(new ResponseDataModel<List<PodRevenueModel>> { Status = true, Message = "Success", Data = result });
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("revenue-by-location")]
        public async Task<IActionResult> GetRevenueByLocation()
        {
            var result = await _dashboardService.GetRevenueByLocationAsync();
            return Ok(new ResponseDataModel<List<LocationRevenueModel>> { Status = true, Message = "Success", Data = result });
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("top-used-services")]
        public async Task<IActionResult> GetTopUsedServices()
        {
            var result = await _dashboardService.GetTopUsedServicesAsync();
            return Ok(new ResponseDataModel<List<TopServiceModel>> { Status = true, Message = "Success", Data = result });
        }


    }

}
