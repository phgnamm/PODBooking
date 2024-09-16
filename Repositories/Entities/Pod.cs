using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Pod: BaseEntity
    {
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string? Description { get; set; }
        public decimal PricePerHour { get; set; }
        public string? Image { get; set; }

        public Guid LocationId { get; set; }
        public virtual Location? Location { get; set; }
        public Guid DeviceId { get; set; }
        public virtual Device? Device { get; set; } 
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
