using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.BookingModels
{
    public class BookingModel : BaseEntity
    {
        public Guid Id { get; set; }
        public string? PodName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? LocationAddress { get; set; }
        public string? Capacity { get; set; }
        public string? Area { get; set; }
        public Guid PodId { get; set; }
        public List<BookingServiceModel> BookingServices { get; set; } = new List<BookingServiceModel>();
    }
}
