using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto
{
    public class BookingDto
    {
        public int? BookingId { get; set; }

        public int? DateTime { get; set; }
        public int? UserId { get; set; }

        public int? HotelId { get; set; }

    }
}