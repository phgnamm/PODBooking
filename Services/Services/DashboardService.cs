using AutoMapper;
using Repositories.Entities;
using Repositories.Interfaces;
using Repositories.Models.PodModels;
using Services.Interfaces;
using Services.Models.DashboardModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RevenueStatsModel> GetRevenueStatsAsync()
        {
            var completedBookings = await _unitOfWork.BookingRepository.GetCompletedBookingsAsync();
            var totalRevenue = completedBookings.Sum(b => b.TotalPrice);
            var bestSellingPods = completedBookings
                .GroupBy(b => b.PodId)
                .Select(g => new BestSellingPodModel
                {
                    PodId = g.Key,
                    PodName = g.First().Pod.Name,
                    TotalBookings = g.Count(),
                    Revenue = g.Sum(b => b.TotalPrice)
                })
                .OrderByDescending(p => p.Revenue)
                .Take(5)
                .ToList();
            var podList = await _unitOfWork.PodRepository.GetAllAsync("");
            var locationList = await _unitOfWork.LocationRepository.GetAllAsync("");
            var deviceList = await _unitOfWork.DeviceRepository.GetAllAsync("");
            var accountList = await _unitOfWork.AccountRepository.GetAllAsync();
            return new RevenueStatsModel
            {
                TotalRevenue = totalRevenue,
                BestSellingPods = bestSellingPods,
                PodCount = podList.Count(),
                LocationCount = locationList.Count(),
                DeviceCount = deviceList.Count(),
                AccountCount = accountList.TotalCount
            };
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var completedBookings = await _unitOfWork.BookingRepository.GetCompletedBookingsAsync();
            return completedBookings.Sum(b => b.TotalPrice);
        }
    }
}
