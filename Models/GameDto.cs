using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeDuCoin.Models
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
}