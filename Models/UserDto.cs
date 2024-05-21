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
        public string? Email { get; set; }
         public string? Password { get; set; }

    }
}