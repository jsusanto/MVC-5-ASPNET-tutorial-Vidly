using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Implement the authentication globally on your web application
            filters.Add(new AuthorizeAttribute());

            //Force to SSL for OATH authentication
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
