using Repositories.Enums;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.DeviceModels
{
    public class DeviceFilterModel : PaginationParameter
    {
        public bool isDelete { get; set; } = false;
        public string? RoomType { get; set; }
        public string? Floor { get; set; }
        public DeviceStatus? Status { get; set; }
    }
}
