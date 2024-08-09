using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto
{
    public class TravelPackageDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<string> Image {get; set;} = new List<string>(); 
        public string? Price { get; set; }
        public bool FreeCancellation { get; set; } = false;
        public bool ReserveNow { get; set; } = false;
    }
}