using Repositories.Enums;
using Repositories.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookingModels
{
    public class BookingCreateModel
    {
        public Guid AccountId { get; set; }
        public Guid PodId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<BookingServiceCreateModel> BookingServices { get; set; } = new List<BookingServiceCreateModel>();
    }
}
