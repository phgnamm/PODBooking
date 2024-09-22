using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.RatingModels
{
    public class RatingModel : BaseEntity
    {
        public int RatingValue { get; set; }
        public string? Comments { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }

        public Guid PodId { get; set; }
        public string? PodName { get; set; }

        public List<RatingCommentModel> CommentsList { get; set; } = new List<RatingCommentModel>();
    }
}
