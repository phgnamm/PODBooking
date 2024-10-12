using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookingModels
{
    public class BookingServiceCreateModel
    {
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
    }
}
