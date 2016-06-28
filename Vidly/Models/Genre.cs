using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace Vidly.Models
{
    public class Genre
    {
        [Required]
        public byte Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        
        
    }
}
