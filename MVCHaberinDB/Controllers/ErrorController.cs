using MVCHaberinDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHaberinDB.Controllers
{
    [EmployeeAuthorize]
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Yetkisiz()
        {
            return View();
        }

    }
}
