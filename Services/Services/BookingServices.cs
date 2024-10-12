using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories.Entities;
using Repositories.Enums;
using Repositories.Interfaces;
using Repositories.Models.BookingModels;
using Services.Common;
using Services.Interfaces;
using Services.Models.BookingModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BookingServices : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Account> _userManager;


        public BookingServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Account> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        private async Task<ResponseModel> CheckRoomAvailabilityAsync(Guid podId, DateTime startTime, DateTime endTime)
        {
            var overlappingBookings = await _unitOfWork.BookingRepository.GetAllAsync(
            filter: b => b.PodId == podId && b.PaymentStatus != PaymentStatus.Canceled &&
                     ((b.StartTime <= startTime && b.EndTime > startTime) ||
                      (b.StartTime < endTime && b.EndTime >= endTime) ||
                      (b.StartTime >= startTime && b.EndTime <= endTime)));
            if (overlappingBookings.Data.Any())
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Room is already booked in the selected time range.",
                };
            }

            return new ResponseModel { Status = true, Message = "Room is available for booking." };
        }

        public async Task<ResponseModel> CreateBookingAsync(BookingCreateModel model)
        {
            var roomAvailability = await CheckRoomAvailabilityAsync(model.PodId, model.StartTime, model.EndTime);
            if (!roomAvailability.Status)
            {
                return roomAvailability;
            }

            var account = await _userManager.FindByIdAsync(model.AccountId.ToString());
            var pod = await _unitOfWork.PodRepository.GetAsync(model.PodId);
            if (account == null || pod == null)
            {
                return new ResponseModel { Status = false, Message = "Invalid account or pod" };
            }

            var totalHours = (model.EndTime - model.StartTime).TotalHours;
            var totalPrice = (decimal)totalHours * pod.PricePerHour;

            decimal totalServicePrice = 0;
            var bookingServices = new List<BookingService>();
            foreach (var serviceModel in model.BookingServices)
            {
                var service = await _unitOfWork.ServiceRepository.GetAsync(serviceModel.ServiceId);
                if (service != null)
                {
                    var serviceTotalPrice = service.UnitPrice * serviceModel.Quantity;
                    totalServicePrice += serviceTotalPrice;

                    bookingServices.Add(new BookingService
                    {
                        ServiceId = serviceModel.ServiceId,
                        Quantity = serviceModel.Quantity,
                        TotalPrice = serviceTotalPrice,
                        ImageUrl = service.ImageUrl
                    });
                }
            }

            totalPrice += totalServicePrice;

            var booking = new Booking
            {
                Code = Guid.NewGuid(),
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                TotalPrice = totalPrice,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = model.PaymentMethod,
                PodId = pod.Id,
                AccountId = account.Id,
                BookingServices = bookingServices
            };

            await _unitOfWork.BookingRepository.AddAsync(booking);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseModel
            {
                Status = true,
                Message = "Booking created successfully"
            };
        }

        public async Task<Pagination<BookingModel>> GetAllBookingsAsync(BookingFilterModel model)
        {
            var queryResult = await _unitOfWork.BookingRepository.GetAllAsync(
                filter: b => (b.IsDeleted == model.isDelete) &&
                             (model.PodId == null || b.PodId == model.PodId) &&
                             (model.AccountId == null || b.AccountId == model.AccountId) &&
                             (model.StartTime == null || b.StartTime >= model.StartTime) &&
                             (model.EndTime == null || b.EndTime <= model.EndTime) &&
                             (model.PaymentStatus == null || b.PaymentStatus == model.PaymentStatus) &&
                             (model.PaymentMethod == null || b.PaymentMethod == model.PaymentMethod),
                include: "Pod.Location,BookingServices.Service",
                pageIndex: model.PageIndex,
                pageSize: model.PageSize
            );

            var bookings = _mapper.Map<List<BookingModel>>(queryResult.Data);

            return new Pagination<BookingModel>(bookings, model.PageIndex, model.PageSize, queryResult.TotalCount);
        }
        public async Task<ResponseDataModel<BookingModel>> GetBookingByIdAsync(Guid bookingId)
        {
            var bookingEntity = await _unitOfWork.BookingRepository.GetAsync( bookingId,
                include: "Pod.Location,BookingServices.Service"
            );

            if (bookingEntity == null)
            {
                return new ResponseDataModel<BookingModel>
                {
                    Status = false,
                    Message = "Booking not found",
                    Data = null
                };
            }
            var bookingModel = _mapper.Map<BookingModel>(bookingEntity);
            return new ResponseDataModel<BookingModel>
            {
                Status = true,
                Message = "Booking retrieved successfully",
                Data = bookingModel
            };
        }




    }
}
