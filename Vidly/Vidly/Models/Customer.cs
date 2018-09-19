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
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        //It's called Navigation Property because it allows us to navigate from one type to another
        public MembershipType MembershipType { get; set; }
        //EntityFramework recognizes the foreign key
        public byte MembershipTypeId { get; set; }
    }
}