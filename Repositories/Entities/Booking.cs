using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Booking: BaseEntity
    {
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TotalPrice {  get; set; }
        public bool PaymentStatus {  get; set; }
        public BookingStatus status {  get; set; }
        public Guid PodId { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Pod? Pod { get; set; }
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    }
}
