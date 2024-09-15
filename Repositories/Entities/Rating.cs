using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int CustomerId {  get; set; }
        public int PodId {  get; set; }
        public Pod Pod { get; set; }
    }
}
