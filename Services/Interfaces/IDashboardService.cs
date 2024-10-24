﻿using Repositories.Models.PodModels;
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
    }
}
