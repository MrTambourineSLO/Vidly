using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        //Navigation property to navigate between types
        public MembershipType MembershipType { get; set; }
        //EF know below is FK by convention
        public byte MembershipTypeId { get; set; }
    }
}