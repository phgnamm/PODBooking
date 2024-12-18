﻿using AutoMapper;
using Repositories.Entities;
using Repositories.Enums;
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
        public async Task<decimal> GetMonthlyRevenueAsync(int month, int year)
        {
            var completedBookings = await _unitOfWork.BookingRepository.GetAllAsync(
                filter: b => b.EndTime.Month == month
                             && b.EndTime.Year == year
                             && b.PaymentStatus == PaymentStatus.Complete
            );

            var totalRevenue = completedBookings.Data.Sum(b => b.TotalPrice);

            return totalRevenue;
        }

        public async Task<List<PodRevenueModel>> GetRevenueByPodAsync()
        {
            var completedBookings = await _unitOfWork.BookingRepository.GetCompletedBookingsAsync();
            var revenueByPod = completedBookings
                .GroupBy(b => b.Pod.Name)
                .Select(g => new PodRevenueModel
                {
                    PodName = g.Key,
                    Revenue = g.Sum(b => b.TotalPrice)
                })
                .ToList();
            return revenueByPod;
        }

        public async Task<List<LocationRevenueModel>> GetRevenueByLocationAsync()
        {
            var completedBookings = await _unitOfWork.BookingRepository.GetCompletedBookingsAsync();
            var revenueByLocation = completedBookings
                .GroupBy(b => b.Pod.Location.Name)
                .Select(g => new LocationRevenueModel
                {
                    LocationName = g.Key,
                    Revenue = g.Sum(b => b.TotalPrice)
                })
                .ToList();
            return revenueByLocation;
        }

        public async Task<List<TopServiceModel>> GetTopUsedServicesAsync()
        {
            var bookingServices = await _unitOfWork.BookingServiceRepository.GetAllAsync("Service");
            var topServices = bookingServices
                .GroupBy(bs => bs.Service.Name)
                .Select(g => new TopServiceModel
                {
                    ServiceName = g.Key,
                    UsageCount = g.Sum(bs => bs.Quantity)
                })
                .OrderByDescending(s => s.UsageCount)
                .Take(5)
                .ToList();
            return topServices;
        }
    }
}
