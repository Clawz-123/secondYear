using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Models
{
    public class Review
    {
        [Key]

        public int Id {get; set;}

        public int?  Rating {get; set;} 

        public string? Comment {get; set;}

        public int? HotelId {get;  set;}

        public Hotel? Hotel {get; set;}

        public int? DateTime { get; set; }
       
        // public int? UserId {get; set;}

        // public User? User {get; set;}

        // public int? TravelId {get; set;}

        // public TravelPackage? TravelPackage {get; set;}

        
    }
}