using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.DTOs
{
    public class RentalDto
    {
        public byte Id { get; set; }
        public byte CustomerId { get; set; }
        public List<int> MovieIds { get; set; } 
}