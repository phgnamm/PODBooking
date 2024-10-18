using Repositories.Entities;
using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.PaymentModels
{
    public class PaymentResponseModel
    {
        public Guid? Code { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
