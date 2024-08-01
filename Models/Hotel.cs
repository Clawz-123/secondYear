using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace secondYear.Models
{
    public class Hotel
    {
        [Key]
        public int Id {get; set;}

        public string Name {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public string Image {get; set;} = string.Empty;
        public string Price {get; set;} = string.Empty;
        public string Address {get; set;} = string.Empty;
    }
}