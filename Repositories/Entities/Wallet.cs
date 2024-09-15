using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        public Double Balance {  get; set; }
        public DateTime LastUpdated { get; set; }
        public int CustomerId;
    }
}
