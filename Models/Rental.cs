using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeDuCoin.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        
    }
}