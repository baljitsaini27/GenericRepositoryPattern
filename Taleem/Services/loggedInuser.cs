using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taleem.Controllers;

namespace Taleem.Services
{
    public class loggedInuser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!filterContext.ActionDescriptor.DisplayName.Contains(".Login") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".Logout") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".ForgotPassword") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".ForgotPasswordConfirmation") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".ResetPassword") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".ResetPasswordConfirmation") &&
            //    !filterContext.ActionDescriptor.DisplayName.Contains(".Register") 
            //    )
            //{
            //    var user = filterContext.HttpContext.User;
            //    if (user.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Email))
            //    {
            //        //user.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Name).FirstOrDefault().Value
            //        //user.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).FirstOrDefault().Value
            //    }
            //    else
            //    {
            //        filterContext.Result = new RedirectResult("~/Account/Logout");
            //    }
            //}
        }
    }
}
