﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
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

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            /*
            Since we delegate a reference to a method, we need to remove the last ()
            It will be
            _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            instead of
            _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>)();
            */
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            //return the standard HTTP 404 response
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map < Customer, CustomerDto >(customer);
        }

        // POST /api/customers
        // Normally it will just return the type of its customer as the common convention
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //It will have an ID that it generated by Database, therefore we need to pass back the ID from Customer to Dto
            customerDto.Id = customer.Id;

            return customerDto;
        }
        
        /*
         Alternatively, we can just use PostCustomer if we dont want to use DataAnnotation
         example:
         public Customer PostCustomer()
         {

         }
         ** recommended not to use this approach because it will create an issue when it comes to code refactoring.
         */

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //All the manual mapping will be replaced by the Mapper object
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            /*
            customerInDb.Name = customerDto.Name;
            customerInDb.BirthDate = customerDto.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customerDto.MembershipTypeId;
            */
            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();
        }
    }
}
