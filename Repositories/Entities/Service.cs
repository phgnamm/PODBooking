using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price {  get; set; }
        public int RoomId {  get; set; }
        public ICollection<BookingService> BookingServices { get; set; }
    }
}
