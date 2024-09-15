using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public float TotalPrice {  get; set; }
        public bool PaymentStatus {  get; set; }
        public PaymentMethod status {  get; set; }
        public int PodId {  get; set; }
        public Pod Pod { get; set; }
        public int AccountId {  get; set; }
        public ICollection<BookingService> BookingServices { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
