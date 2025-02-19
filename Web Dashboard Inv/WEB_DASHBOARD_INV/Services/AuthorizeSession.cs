using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_DASHBOARD_INV.Services
{
    public class AuthorizeSession : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["token"] == null || httpContext.Session["user_id"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                // Otherwise redirect to your specific authorized area
                filterContext.Result = new RedirectResult(Properties.Settings.Default.ErrorRidirect);
            }
        }
    }
}