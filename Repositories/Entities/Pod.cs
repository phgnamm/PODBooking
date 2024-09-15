using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Pod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public string Image { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<Device> Devices { get; set; }
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
