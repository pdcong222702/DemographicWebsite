﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demographic_Website.ModelViews
{
    public class Authentication:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("IdentificationCode") == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller","Authencation" },
                    {"Action","Login" }
                });
            }
        }
    }
}
