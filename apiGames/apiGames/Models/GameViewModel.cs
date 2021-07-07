using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Models
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public double Price { get; set; }
    }
}
