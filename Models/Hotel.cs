using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using secondYear.Dto;


namespace secondYear.Models
{
    public class Hotel
    {
        [Key]
        public int Id {get; set;}

            public string Name {get; set;} = string.Empty;
            public string Description {get; set;} = string.Empty;
            public List<string> Image {get; set;} = new List<string>();
            public string Price {get; set;} = string.Empty;
            // public string Address {get; set;} = string.Empty;

            public bool FreeCancellation {get; set;} = false;
            public bool ReserveNow {get; set;} = false;

            public ICollection<Review> Reviews {get; set;} = new List<Review>();

       

        // public ICollection<Booking> Bookings {get; set;} = new List<Booking>();
    }
}