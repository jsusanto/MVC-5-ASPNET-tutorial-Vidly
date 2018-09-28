using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Runtime.Caching;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                /*
                 If we don't pass a new Customer object in New method, the validation summary will show validation error on Id
                 The Id field is required.
                 */
                Customer = new Customer(), 
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            /*
             ModelState property to get access to validation data
             */
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            /*
             if we use public ActionResult Create(NewCustomerViewModel viewModel)
             The objects returned would be Customer **have value** and MembershipType **null**

             By using public ActionResult Create(Customer customer)
             ASP.NET is clever enough to assign directly to customer
             it's called model binding
             */

            if( customer.Id == 0)
            {
                _context.Customers.Add(customer); //this syntax will save data in the memory
            }
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);

                //update the properties
                /*
                 * TryUpdateModel(customerInDb);
                 * 
                 * this approach used by the official from Microsoft .NET 
                 * The properties will be updated based on the key pairs.
                 * 
                 * Drawback:
                 * - it will open the security hole in your application why because in reality, you don't want all users
                 * to update all properties.
                 * it should be just particular user who has certain level access will be able to update the properties
                 * Potential malicious user to update and add additional key - value pairs
                 * 
                 * Now the work around that Microsoft shows you is even worse
                 * They advise to add the whitelist argument to flag which fields will be updated.
                 * 
                 * TryUpdateModel(customerInDb, "", new string[] { "Name", "Email" });
                 * 
                 * What will happen?
                 * Your application will break the next day you update the field name
                 * 
                 * The bottom line is that you shouldn't blindly read or request all data in the object
                 * To enhance a better approach (security), you need to create another Model that you allow to update the object 
                 * Dto: Data transfer object
                 * public ActionResult Save(UpdateCustomerDto customer)
                 */
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges(); //it will execute / persist to database

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();

            //when this line's executed, EntityFramework won't query to database and it's called deferred execution
            //The query gets executed when we iterate the customers object
            //var customers = _context.Customers;

            //Eager loading is to load object and related object to avoid the error when calling the related object 
            //Include - we need to declare new namespace System.Data.Entity

            /* We don't need to pass this one if we're using DataTable
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
            */


            /* 
             * Data Caching
             * Example that we want to cache the list of Genre
             * Use Data caching if ONLY that we want to optimise the performance, don't use the cache blindly 
             * because it will increase your application memory and the unnecessary code complexity
             */

            if (MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }

            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;

            return View(); //because we're using DataTables
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
        
        public IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Jeffry Susanto" },
                new Customer { Id = 2, Name = "Susanto Jeffry" }
            };
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}