using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeDuCoin.Models
{
    public class RentalDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public GameDto Game { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}