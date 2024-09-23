using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.DeviceModels
{
    public class DeviceFilterModel:PaginationParameter
    {
        public string? RoomType { get; set; }
        public string? Floor { get; set; }
    }
}
