using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowerShop.Models
{
    public class ManagerAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["Username"] != null)//有Session直接返回
                return;
            else
                filterContext.HttpContext.Response.Redirect("/Manager/Login");
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<script>alert('请和管理员联系')</script>");
            filterContext.HttpContext.Response.Redirect("/Manager/Login");
        }
    }
}