using ProjectEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace LibraryProject.Filter
{
    public class AuthenticationFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public AuthenticationFilter()
        {
            RequireAdminRole = false;
        }

        public bool RequireAdminRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = (User)HttpContext.Current.Session["LoggedUser"];

            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
            /*user.IsAdmin==true*/
            if (RequireAdminRole == true && user.Authority == false)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
                return;
            }
        }
    }
}