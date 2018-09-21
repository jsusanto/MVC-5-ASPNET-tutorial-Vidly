using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember: ValidationAttribute
    {
        /* 
         * There are 2 IsValid methods, but we choose the second one because it has more access to the properties
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
        */

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*
             validationContext.ObjectInstance object gives the access to customer object, but we need to cast to Customer object
             */
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("Birthdate is required.");

            var age = DateTime.Now.Year - customer.BirthDate.Value.Year;

            return (age >= 18) ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}