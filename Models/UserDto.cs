using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CafeDuCoin.Models
{
    public class UserDto
    {

    
        public string? Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string? Email { get; set; }

        [Required]
         public string? Password { get; set; }

    }
}