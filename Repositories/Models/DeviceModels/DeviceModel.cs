using Repositories.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.DeviceModels
{
    public class DeviceModel
    {
        public string? RoomType { get; set; }
        public string? Floor { get; set; }
        public DeviceStatus Status { get; set; }
        public string? ImageUrl { get; set; }
    }
}
