using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.ServiceModels
{
    public class ServiceFilterModel : PaginationParameter
    {
        public bool isDelete { get; set; } = false;
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
