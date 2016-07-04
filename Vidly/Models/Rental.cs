using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.DTOs;

namespace Vidly.Models
{
    public class Rental
    {
        public byte Id { get; set; }    
        public byte CustomerId { get; set; }
        public List<int> MovieIds { get; set; }
        
    }
}