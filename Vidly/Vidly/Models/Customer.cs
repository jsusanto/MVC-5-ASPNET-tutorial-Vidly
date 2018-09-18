using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        //It's called Navigation Property because it allows us to navigate from one type to another
        public MembershipType MembershipType { get; set; }
        //EntityFramework recognizes the foreign key
        public byte MembershipTypeId { get; set; }
    }
}