using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookingModels
{
    public class BookingCreateServiceModel
    {
        public Guid BookingId { get; set; }
        public List<BookingServiceCreateModel> BookingServices { get; set; } = new List<BookingServiceCreateModel>();
    }
}
