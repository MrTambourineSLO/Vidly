using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Razor;

namespace Vidly.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime Added { get; set; }
        [Required]
        [Display(Name = "Number in Stock")]
        public byte NumberInStock { get; set; }
        /*Foreign key part*/
        //Navigation property to navigate between types
        public Genre Genre { get; set; }
        //EF's FK by convention
        [Required]
        [Display(Name = "Genres")]
        public byte GenreId { get; set; }


    }
}