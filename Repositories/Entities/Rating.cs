using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Rating : BaseEntity
    {
        public int RatingValue { get; set; }
        public string? Comments { get; set; }

        public Guid CustomerId { get; set; }
        public virtual Account? Customer { get; set; }

        public Guid PodId { get; set; }
        public virtual Pod? Pod { get; set; }

        public ICollection<RatingComment> CommentsList { get; set; } = new List<RatingComment>();
    }
}
