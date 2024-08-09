using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Models
{
    public class TravelPackage
    {
        [Key]
        
        public int Id {get; set;}

         public string? Name {get; set;} 
        public string? Description {get; set;} 
        public List<string> Image {get; set;} = new List<string>(); 
        public string? Price {get; set;} 
        public bool FreeCancellation {get; set;} = false;
        public bool ReserveNow {get; set;} = false;
        // public ICollection<Review> Reviews {get; set;} = new List<Review>();
    }
}