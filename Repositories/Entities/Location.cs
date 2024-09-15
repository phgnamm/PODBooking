﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Location : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime TimeOpen { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public virtual ICollection<Pod> Pods { get; set; } = new List<Pod>();
    }
}
