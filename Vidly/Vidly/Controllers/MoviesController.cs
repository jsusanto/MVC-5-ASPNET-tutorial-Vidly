﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        //int? to make it nullable
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            return View(movie);
        }

        //The parameter (int id) will map against the RouteConfig 
        //url: "{controller}/{action}/{id}" ==> therefore we can't change this one in this scenario
        public ActionResult Edit(int id)
        {
            return Content("id " + id);
        }

        public ActionResult ByReleasedDate(int year, int month)
        {
            return Content(year + " / " + month);
        }
    }
}