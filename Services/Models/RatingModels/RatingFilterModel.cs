using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.RatingModels
{
    public class RatingFilterModel : PaginationParameter
    {
        public Guid? PodId { get; set; }
        public Guid? AccountId { get; set; }
        public int? RatingValue { get; set; }
    }
}
