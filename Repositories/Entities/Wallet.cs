using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Wallet: BaseEntity
    {
        public decimal Balance { get; set; }       
        public DateTime LastUpdated { get; set; } 
        public Guid CustomerId { get; set; }        
        public virtual Account? Customer { get; set; }      
    }
}
