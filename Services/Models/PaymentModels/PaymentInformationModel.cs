using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentModels
{
    public class PaymentInformationModel
    {
        public Guid AccountId { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? Code { get; set; }
    }
}
