﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            /* 
             * using .Single because we assume that the customer will be sending the right customer id from drop down list
             * if we're building a public API that can be used by various different application, we should us SingleOrDefault
             * 
             * var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
             * if (customer == null)
             *   return BadRequest("Invalid Customer ID");
             */

            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            /* 
             * To load multiple movies
             * The following code is the translation of 
             * SELECT * FROM Movies WHERE Id in (1,2,3); 
             */
            var movies = _context.Movies.Where
                (m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                // Movie is not available, this one has to be added to avoid malicious attach from other users.
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
