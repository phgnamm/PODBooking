using Repositories.Models.PodModels;
using Services.Models.DashboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDashboardService
    {
        Task<RevenueStatsModel> GetRevenueStatsAsync();
        Task<decimal> GetMonthlyRevenueAsync(int month, int year);
        Task<List<PodRevenueModel>> GetRevenueByPodAsync();
        Task<List<LocationRevenueModel>> GetRevenueByLocationAsync();
        Task<List<TopServiceModel>> GetTopUsedServicesAsync();
    }
}
