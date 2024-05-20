using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;

namespace CafeDuCoin.Models
{
    public class User
    {
        public int Id { get; set; }


        public string? Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public  string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

        public List<Rental> Rentals { get; set; }
        public User()
        {
            Rentals = new List<Rental>();
        }
    }
}