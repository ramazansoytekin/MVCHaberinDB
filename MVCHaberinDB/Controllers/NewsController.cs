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
    [EmployeeAuthorize]
    public class NewsController : Controller
    {
      
        public NewsDBEntities db = new NewsDBEntities();

        //
        // GET: /News/

        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Category).Include(n => n.Director).Include(n => n.Supplier);
            return View(news.ToList());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(News news, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/uploads/{0}", guid)));

                    news.PhotoPath = guid;
                }
                Director girisYapan = (Director)Session["calisan"];
                news.DirectorID = girisYapan.DirectorID;
                news.PublishDate = DateTime.Now;
                news.isDeleted = true;
                news.ViewPoint = 0;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", news.CategoryID);
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", news.DirectorID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", news.SupplierID);
            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            Session["haber"] = db.News.Find(id);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", news.CategoryID);
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", news.DirectorID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", news.SupplierID);
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/uploads/{0}", guid)));

                    news.PhotoPath = guid;
                }
                else
                {
                    news.PhotoPath = ((News)Session["haber"]).PhotoPath;

                }
                news.DirectorID = ((Director)Session["calisan"]).DirectorID;
                news.PublishDate = ((News)Session["haber"]).PublishDate;
                news.DateOfUpdate = DateTime.Now;
                if (news.ViewPoint == null)
                {
                    news.ViewPoint = 0;
                }
                db.Entry(db.News.Find(news.NewsID)).CurrentValues.SetValues(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", news.CategoryID);
            ViewBag.DirectorID = new SelectList(db.Directors, "DirectorID", "FirstName", news.DirectorID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", news.SupplierID);
            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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