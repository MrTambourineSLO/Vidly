using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class MovieDto
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime Added { get; set; }

        [Required]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        //EF's FK by convention
        [Required]
        [Display(Name = "Genres")]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }
    }
}