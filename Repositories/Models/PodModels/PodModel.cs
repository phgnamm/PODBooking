using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models.PodModels
{
    public class PodModel : BaseEntity
    {
        public string? Name { get; set; }          
        public string? Description { get; set; }   
        public decimal PricePerHour { get; set; }  
        public double Area { get; set; }          
        public int Capacity { get; set; }          
        public Guid LocationId { get; set; }       
        public string? LocationName { get; set; }  

        public Guid DeviceId { get; set; }         
        public string? DeviceType { get; set; }    

        public string? ImageUrl { get; set; }      
    }

}
