using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public short SignupFees { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
        [Required]
        public string MembershipName { get; set; }

    }
}