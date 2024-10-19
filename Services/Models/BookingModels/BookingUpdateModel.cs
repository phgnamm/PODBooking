using Repositories.Enums;
using Repositories.Models.BookingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookingModels
{
    public class BookingUpdateModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool UseRewardPoints { get; set; } 
        public List<BookingServiceModelRequest> BookingServices { get; set; } = new List<BookingServiceModelRequest>();
    }
    public class BookingServiceModelRequest
    {
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
    }
}
