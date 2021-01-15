using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHaberinDB.Models
{
    public class CeoAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["ceo"] != null)
            {
                Director girisYapan = (Director)httpContext.Session["ceo"];
                if (girisYapan.RoleID == 1)
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