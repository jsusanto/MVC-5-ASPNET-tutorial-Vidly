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

        //Data annotation to override the default data type
        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        [Display (Name ="Customer Name")]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //It's called Navigation Property because it allows us to navigate from one type to another
        public MembershipType MembershipType { get; set; }

        //EntityFramework recognizes the foreign key
        [Display(Name = "Membership Type")]
        //MembershipTypeId is implicitly as a required field because we don't declare as byte?
        public byte MembershipTypeId { get; set; }

        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
    }
}