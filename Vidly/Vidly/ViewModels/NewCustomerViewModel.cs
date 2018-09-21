using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewCustomerViewModel
    {
        /*
            We're using IEnumerable instead of List because in this class we don't need the methods provided by List
            such as, adding, removing an object and others
            We just need to iterate the object 
        */
        //public List<MembershipType> MembershipTypes { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}