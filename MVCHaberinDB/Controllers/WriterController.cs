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
    [DirectorAuthorize]
    public class WriterController : Controller
    {
        private NewsDBEntities db = new NewsDBEntities();

        //
        // GET: /Writer/

        public ActionResult Index()
        {
            return View(db.Writers.ToList());
        }

        //
        // GET: /Writer/Details/5

        public ActionResult Details(int id = 0)
        {
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        //
        // GET: /Writer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Writer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Writer writer, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/writerPhotos/{0}", guid)));
                    writer.ImagePath = guid;
                    db.Writers.Add(writer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(writer);
        }

        //
        // GET: /Writer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            Session["yazar"] = db.Writers.Find(id);
            return View(writer);
        }

        //
        // POST: /Writer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Writer writer, HttpPostedFileBase fluResim)
        {
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/writerPhotos/{0}", guid)));

                    writer.ImagePath = guid;
                }
                else
                {
                    writer.ImagePath = ((Writer)Session["yazar"]).ImagePath;
                }
                db.Entry(db.Writers.Find(writer.WriterID)).CurrentValues.SetValues(writer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        //
        // GET: /Writer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        //
        // POST: /Writer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Writer writer = db.Writers.Find(id);
            db.Writers.Remove(writer);
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