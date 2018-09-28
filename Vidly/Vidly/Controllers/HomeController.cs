using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Vidly.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        /*
         * to activate OUTPUT CACHING caching on a page to speed up the page during the load
         * VaryByParam - meaning that we will be caching each of the genre on different cache file
         * or you use '*' in order to cache all of them
         * !!!! Use the cache ONLY on the page that shows the heavy lifting query to database
         * 
         * example:
         * [OutputCache(Duration = 50, Location = OutputCacheLocation.Server, VaryByParam = "genre")]
         * if the action takes a parameter - genre then we can cache each of genre by specifying the VaryByParam
         * 
         * How to disable the caching ??
         * You need to put the Duration = 0
         * 
         * [OutputCache(Duration = 0, VaryByParam = "*", NoStore = true)]
         */
        [OutputCache(Duration = 50, Location = OutputCacheLocation.Server, VaryByParam = "genre")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}