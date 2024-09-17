using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class BookingService: BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid BookingId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual Service? Service { get; set; }

    }
}
