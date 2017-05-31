using System;
using System.ComponentModel.DataAnnotations;

namespace VideothekData.Models
{
    internal class Min18AsMemberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Customer customer = (Customer) validationContext.ObjectInstance;

            if (customer.MembershipId == 0 || customer.MembershipId == 1)
                return ValidationResult.Success;

            if (!customer.Birthday.HasValue)
                return new ValidationResult("Birthday is required");


            int age = DateTime.Now.Year - customer.Birthday.Value.Year;
            if (age >= 18)
                return ValidationResult.Success;
            else
                return new ValidationResult("Customer should be at least 18 to go on a Membership");
        }
    }
}