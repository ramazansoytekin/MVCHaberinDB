using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHaberinDB.Models;

namespace MVCHaberinDB.Controllers
{
    [CeoAuthorize]
    public class DirectorController : Controller
    {
        private NewsDBEntities db = new NewsDBEntities();

        //
        // GET: /Director/

        public ActionResult Index()
        {
            var directors = db.Directors.Include(d => d.Director1).Include(d => d.Role);
            return View(directors.ToList());
        }

        //
        // GET: /Director/Details/5

        public ActionResult Details(int id = 0)
        {
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        //
        // GET: /Director/Create

        public ActionResult Create()
        {
            ViewBag.ReportsTo = new SelectList(db.Directors, "DirectorID", "FirstName");
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName");
            return View();
        }

        //
        // POST: /Director/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/directorPhotos/{0}", guid)));

                    director.Imagepath = guid;
                }
                db.Directors.Add(director);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportsTo = new SelectList(db.Directors, "DirectorID", "FirstName", director.ReportsTo);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", director.RoleID);
            return View(director);
        }

        //
        // GET: /Director/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.ReportsTo = new SelectList(db.Directors, "DirectorID", "FirstName", director.ReportsTo);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", director.RoleID);
            return View(director);
        }

        //
        // POST: /Director/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Director director, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/directorPhotos/{0}", guid)));

                    director.Imagepath = guid;
                }
                else
                {
                    director.Imagepath = ((Director)Session["yonetim"]).Imagepath;
                }
                db.Entry(db.Directors.Find(director.DirectorID)).CurrentValues.SetValues(director);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportsTo = new SelectList(db.Directors, "DirectorID", "FirstName", director.ReportsTo);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleName", director.RoleID);
            return View(director);
        }

        //
        // GET: /Director/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Director director = db.Directors.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        //
        // POST: /Director/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = db.Directors.Find(id);
            db.Directors.Remove(director);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}