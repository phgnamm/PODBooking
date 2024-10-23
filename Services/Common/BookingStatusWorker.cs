using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using Repositories.Entities;
using Repositories.Enums;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class BookingStatusWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailService _emailService;

        public BookingStatusWorker(IServiceProvider serviceProvider, IEmailService emailService)
        {
            _serviceProvider = serviceProvider;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

                    var upcomingBookings = await dbContext.Bookings
                        .Where(b => b.PaymentStatus == PaymentStatus.UpComing)
                        .ToListAsync(stoppingToken);

                    foreach (var booking in upcomingBookings)
                    {
                        if (booking.StartTime.AddDays(-1) <= DateTime.Now)
                        {
                            var account = await userManager.FindByIdAsync(booking.AccountId.ToString());
                            if (account != null)
                            {
                                var subject = "Nhắc nhở đặt phòng sắp tới!";
                                var body = $"Xin chào {account.FirstName},\n\n" +
                                           $"Đặt phòng của bạn sẽ bắt đầu vào ngày {booking.StartTime}. Vui lòng chuẩn bị!";
                                await _emailService.SendEmailAsync(account.Email, subject, body, isBodyHTML: false);
                            }
                        }
                        if (booking.StartTime <= DateTime.Now)
                        {
                            booking.PaymentStatus = PaymentStatus.OnGoing;
                            booking.ModificationDate = DateTime.Now;
                        }
                    }

                    var ongoingBookings = await dbContext.Bookings
                        .Where(b => b.PaymentStatus == PaymentStatus.OnGoing && b.EndTime <= DateTime.Now)
                        .ToListAsync(stoppingToken);

                    foreach (var booking in ongoingBookings)
                    {
                        booking.PaymentStatus = PaymentStatus.Complete;
                        booking.ModificationDate = DateTime.Now;

                        var account = await userManager.FindByIdAsync(booking.AccountId.ToString());
                        if (account != null)
                        {
                            var subject = "Booking hoàn thành!";
                            var body = $"Xin chào {account.FirstName},\n\n" +
                                       $"Đặt phòng của bạn đã hoàn thành vào ngày {booking.EndTime}. " +
                                       "Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!";
                            await _emailService.SendEmailAsync(account.Email, subject, body, isBodyHTML: false);
                        }
                    }

                    if (upcomingBookings.Any() || ongoingBookings.Any())
                    {
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
