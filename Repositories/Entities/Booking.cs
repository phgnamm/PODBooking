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
        public Guid Code { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice {  get; set; }
        public PaymentStatus PaymentStatus {  get; set; }
        public PaymentMethod PaymentMethod {  get; set; }
        public Guid PodId { get; set; }
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Pod? Pod { get; set; }
        public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
