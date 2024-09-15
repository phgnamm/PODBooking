using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Payment: BaseEntity
    {
        public float amount;
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime DateTime { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid BookingId {  get; set; }
        public virtual Booking? Booking { get; set; }
        public Guid WalletId {  get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
