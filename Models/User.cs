using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }

        public string?Image {get; set;}
         public string? Email {get; set;}
        public string? Password {get; set;}

        public string? CoverImage {get; set;}

        public string? Biodata {get; set;}

        // public ICollection<Booking> Bookings {get; set;} =new List<Booking>();
        // public ICollection<Review> Reviews {get; set;} =new List<Review>();
    }
}