using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto.HotelDTOs
{
    public class GetHotelDTOs
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
            public List<string> Image {get; set;} = new List<string>();
        public string Price { get; set; } = string.Empty;
        public bool FreeCancellation { get; set; } = false;
        public bool ReserveNow { get; set; } = false;
    }
}