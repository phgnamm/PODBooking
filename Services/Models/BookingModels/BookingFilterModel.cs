using Repositories.Enums;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.BookingModels
{
    public class BookingFilterModel : PaginationParameter
    {
        public Guid? PodId { get; set; }
        public Guid? AccountId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public bool isDelete { get; set; } = false;


    }
}
