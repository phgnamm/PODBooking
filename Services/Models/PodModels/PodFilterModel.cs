using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PodModels
{
    public class PodFilterModel : PaginationParameter
    {
        public bool isDelete { get; set; } = false;
        public Guid? LocationId { get; set; }
        public Guid? DeviceId { get; set; }
        public decimal? MinPricePerHour { get; set; }
        public decimal? MaxPricePerHour { get; set; }
        public double? MinArea { get; set; }
        public double? MaxArea { get; set; }
        public int? MinCapacity { get; set; }
        public int? MaxCapacity { get; set; }
    }

}
