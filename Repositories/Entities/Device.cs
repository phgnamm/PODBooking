using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationDevice { get; set; }
        public string Status { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }
        public string Image { get; set; }

        public int PodId { get; set; }
        public Pod Pod { get; set; }
    }

}
