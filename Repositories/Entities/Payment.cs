using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public float amount;
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime DateTime { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int BookingId {  get; set; }
        public Booking Booking { get; set; }
        public int WalletId {  get; set; }
        public Wallet Wallet { get; set; }
    }
}
