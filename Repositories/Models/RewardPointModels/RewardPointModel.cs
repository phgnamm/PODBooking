using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.RewardPointModels
{
    public class RewardPointModel:BaseEntity
    {
        public Guid AccountId { get; set; }
        public int Points { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
    }
}
