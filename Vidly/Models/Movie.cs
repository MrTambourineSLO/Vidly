using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor;

namespace Vidly.Models
{
    //Because of default route a view can return id of 
    //type string which is invalid and may cause a
    //ModelState validation error
    //[Bind(Exclude = "Id")]
    public class Movie
    {
        
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required]
        public DateTime Added { get; set; }
        
        [Required]
        [Display(Name = "Number in Stock")]
        [Range(1,20)]
        public byte NumberInStock { get; set; }
        [Required]
        public int NumberAvailable { get; set; }
        
        /*Foreign key part*/
        //Navigation property to navigate between types
        public Genre Genre { get; set; }
        
        //EF's FK by convention
        [Required]
        [Display(Name = "Genres")]
        public byte GenreId { get; set; }


    }
}