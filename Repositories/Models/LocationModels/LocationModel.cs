using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.LocationModels
{
    public class LocationModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime TimeOpen { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
    }
}
