using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Service : BaseEntity
    {
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    }
}
