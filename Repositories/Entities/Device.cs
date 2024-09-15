using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Device : BaseEntity
    {
        public string? Name { get; set; }
        public DeviceStatus Status { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableUntil { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();

    }

}
