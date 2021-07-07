using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Models
{
    public class GameInputModel
    {
        [Required(ErrorMessage = "The game's name is mandatory.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The game's name must have at least one character.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The publisher's name is mandatory.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The publisher's name must have at least one character.")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "The game's genre is mandatory.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "The game's genre must have at least one character.")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "The release year is mandatory.")]
        public int ReleaseYear { get; set; }
        [Range(0, 1000, ErrorMessage = "The game's price should be less than $1,000.00")]
        public double Price { get; set; }
    }
}
