using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Dto
{
    public class UserDto
    {
        
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}