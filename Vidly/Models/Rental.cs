using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.DTOs;

namespace Vidly.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public Customer Customer { get; set; }

        
    }
}