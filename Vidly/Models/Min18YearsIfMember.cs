using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 1 || customer.MembershipTypeId == 0)
            {
                return ValidationResult.Success;
            }
            if (customer.Birthday == null)
            {
                return new ValidationResult("Birthday field is required.");
            }
            var age = DateTime.Today.Year-customer.Birthday.Value.Year;
            return (age >= 18
                ? ValidationResult.Success
                : new ValidationResult("You have to be at least 18 to subscribe"));
        }
    }
}