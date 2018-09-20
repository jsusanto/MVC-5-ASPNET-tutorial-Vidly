﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

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
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();

            //when this line's executed, EntityFramework won't query to database and it's called deferred execution
            //The query gets executed when we iterate the customers object
            //var customers = _context.Customers;

            //Eager loading is to load object and related object to avoid the error when calling the related object 
            //Include - we need to declare new namespace System.Data.Entity
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
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
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}