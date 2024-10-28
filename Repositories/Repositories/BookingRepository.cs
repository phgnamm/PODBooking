using Microsoft.EntityFrameworkCore;
using Repositories.Common;
using Repositories.Entities;
using Repositories.Enums;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext dbContext, IClaimsService claimsService) : base(dbContext, claimsService)
        {
        }

        public async Task<IEnumerable<Booking>> GetCompletedBookingsAsync()
        {
            return await _dbSet.Where(b => b.PaymentStatus == PaymentStatus.Complete).Include(b => b.Pod)
            .ToListAsync();
        }
    }
}
