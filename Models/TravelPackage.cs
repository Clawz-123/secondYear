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
        public string? Image {get; set;} 
        public string? Price {get; set;} 
        public string? Address {get; set;} 
    }
}