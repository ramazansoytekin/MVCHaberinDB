using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHaberinDB.Models
{
    public class EmployeeAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["calisan"] != null)
            {
                Director girisYapan = (Director)httpContext.Session["calisan"];
                if (girisYapan.RoleID == 4 || girisYapan.RoleID == 1)
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