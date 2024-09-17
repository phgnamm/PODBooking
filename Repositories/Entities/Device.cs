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
        public string? RoomType { get; set; }
        public string? Floor { get; set; }
        public DeviceStatus Status { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();

    }

}
