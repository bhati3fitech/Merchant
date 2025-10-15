namespace MerchantUPBP.App_Code
{
    using Entity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;


    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;

            // Check if the user session variable is not set
            if (session.GetString("UserId") == null)
            {
                // Redirect to the login page or any other page you want
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary { { "controller", "Home" }, { "action", "MerchantLogin" } });
            }

        }



    }

}
