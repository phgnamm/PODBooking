using Repositories.Models.BookingModels;
using Services.Common;
using Services.Models.BookingModels;
using Services.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookingService
    {
        Task<ResponseModel> CreateBookingAsync(BookingCreateModel model);
        Task<Pagination<BookingModel>> GetAllBookingsAsync(BookingFilterModel model);
        Task<ResponseDataModel<BookingModel>> GetBookingByIdAsync(Guid bookingId);
    }
}
