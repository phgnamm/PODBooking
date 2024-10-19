using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class RewardPoints : BaseEntity
    {
        public Guid AccountId { get; set; }
        public int Points { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public Account? Account { get; set; }
    }
}
