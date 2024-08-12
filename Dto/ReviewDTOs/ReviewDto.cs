using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto
{
    public class ReviewDto
    {
        public int? Rating { get; set; }

        public string? Comment { get; set; }

        public int? HotelId { get; set; }
        public int? DateTime { get; set; }
        // public int? UserId {get; set;}


    }
}