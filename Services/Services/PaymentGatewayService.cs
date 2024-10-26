using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.Entities;
using Repositories.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<Account> _userManager;

        public PaymentGatewayService(UserManager<Account> userManager, IConfiguration configuration, AppDbContext dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<string> CreatePaymentUrlVnpay(string bookingCode, HttpContext httpContext)
        {
            // Parse bookingCode thành Guid
            if (!Guid.TryParse(bookingCode, out var bookingGuid))
            {
                throw new Exception("Invalid booking code.");
            }

            // Lấy thông tin booking từ database dựa trên GUID
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Code == bookingGuid);
            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }

            // Tạo URL thanh toán với giá trị TotalPrice từ booking
            var pay = new VnPayLibrary();
            var urlCallBack = $"{_configuration["Vnpay:ReturnUrl"]}{bookingCode}";
            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)booking.TotalPrice * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(httpContext));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"Khach hang: {booking.AccountId} thanh toan hoa don {bookingCode}");
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", bookingCode);

            var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
            return paymentUrl;
        }

        public async Task HandlePaymentSuccess(string bookingCode)
        {
            if (!Guid.TryParse(bookingCode, out var bookingGuid))
            {
                throw new Exception("Invalid booking code.");
            }

            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Code == bookingGuid);
            if (booking == null)
            {
                throw new Exception("Booking not found.");
            }
            booking.PaymentStatus = PaymentStatus.UpComing;
            booking.ModificationDate = DateTime.Now;
            _dbContext.Bookings.Update(booking);

            //var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == booking.AccountId);
            var account = await _userManager.FindByIdAsync(booking.AccountId.ToString());
            if (account != null)
            {
                int rewardPoints = 100; 
                var reward = new RewardPoints
                {
                    AccountId = account.Id,
                    Points = rewardPoints,
                    TransactionDate = DateTime.Now,
                    Description = $"Received {rewardPoints} points for a payment of {booking.TotalPrice} on booking {bookingCode}"
                };

                _dbContext.RewardPoints.Add(reward);
            }

            await _dbContext.SaveChangesAsync();
        }


    }

}
