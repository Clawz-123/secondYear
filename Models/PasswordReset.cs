using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondYear.Models
{
    public class PasswordReset
    {
         [Key]
        public int Id { get; set; }

        public string? Token { get; set; }
    }
}