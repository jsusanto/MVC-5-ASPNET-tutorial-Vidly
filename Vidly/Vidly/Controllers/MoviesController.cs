﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        //int? to make it nullable
        //This Index method was used for demonstration purposes
        /*
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }*/

        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer { Name = "John Smith" },
                new Customer { Name = "Marry Williams" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        //The parameter (int id) will map against the RouteConfig 
        //url: "{controller}/{action}/{id}" ==> therefore we can't change this one in this scenario
        public ActionResult Edit(int id)
        {
            return Content("id " + id);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleasedDate(int year, int month)
        {
            return Content(year + " / " + month);
        }

        public ViewResult index()
        {
            var movies = getMovies();

            return View(movies);
        }

        public IEnumerable<Movie> getMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek!" },
                new Movie { Id = 2, Name = "Too Fast Too Furious" }
            };
        }

        public ActionResult Details(int Id)
        {
            var movie = getMovies().SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}