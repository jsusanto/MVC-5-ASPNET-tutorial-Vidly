using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        /*Shortcut type: prop - tab - tab*/
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleasedDate { get; set; }
        public byte NumberInStock { get; set; }

    }
}