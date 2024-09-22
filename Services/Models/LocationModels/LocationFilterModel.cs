using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.LocationModels
{
    public class LocationFilterModel:PaginationParameter
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
