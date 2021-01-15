using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHaberinDB.Models
{
    public class DirectorAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["yonetim"] != null)
            {
                Director girisYapan = (Director)httpContext.Session["yonetim"];
                if (girisYapan.RoleID == 2 || girisYapan.RoleID == 1)
                {
                    return true;
                }
                httpContext.Response.Redirect("/Error/Yetkisiz");
                return false;
            }
            else
            {
                httpContext.Response.Redirect("/Home/Login");
                return false;
            }
        }
    }
}