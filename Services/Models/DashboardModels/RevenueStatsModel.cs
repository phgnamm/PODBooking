using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.DashboardModels
{
    public class RevenueStatsModel
    {
        public decimal TotalRevenue { get; set; }
        public List<BestSellingPodModel> BestSellingPods { get; set; }
    }
    public class BestSellingPodModel
    {
        public Guid PodId { get; set; }
        public string PodName { get; set; }
        public int TotalBookings { get; set; }
        public decimal Revenue { get; set; }
    }
}
