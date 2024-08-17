using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto.UserDTOs
{
    public class UpdatePassword
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? Update_Password { get; set;}

    }
}