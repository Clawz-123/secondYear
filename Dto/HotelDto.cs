using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto
{
    public class HotelDto
    {
         public string Name {get; set;} = string.Empty;
            public string Description {get; set;} = string.Empty;
            public string Image {get; set;} = string.Empty;
            public string Price {get; set;} = string.Empty;
            public string Address {get; set;} = string.Empty;
    }
}