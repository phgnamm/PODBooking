using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PodModels
{
    public class PodCreateModel
    {
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public int? Area { get; set; }
        public string? Description { get; set; }
        public decimal PricePerHour { get; set; }
        public string? ImageUrl { get; set; }
    }
}
