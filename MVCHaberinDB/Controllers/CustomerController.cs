using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCHaberinDB.Models;


namespace MVCHaberinDB.Controllers
{
    [UserAuthorize]
    public class CustomerController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();

        public ActionResult Index()
        {

            return View(db.News.ToList());

        }

        public ActionResult ListNewsByCategory(int cID = 0)
        {
            
            if (cID == 0 || cID > db.Categories.Count() + 1)
            {
                ViewBag.Message = "Görüntülenmek için herhangi bir kriter seçilmedi.";
                List<News> nList = new List<News>();

                return View(nList);
            }
            else if (cID == 3)
            {
                //Manşetler için 
                List<News> nList = db.News.Where(n => n.isBigNew == true).OrderByDescending(n => n.PublishDate).ToList();

                return View(nList);
            }
            else
            {
                List<News> nList = db.News.Where(n => n.CategoryID == cID).OrderByDescending(n => n.PublishDate).ToList();

                return View(nList);
            }

        }

        public ActionResult NewsDetail(int nID = 0)
        {

            if (nID != 0 && db.News.Any(n => n.NewsID == nID))
            {
                News haber = db.News.FirstOrDefault(n => n.NewsID == nID);


                haber.ViewPoint++;
                db.SaveChanges();

                Session["haber"] = haber;

                List<Comment> yorumListesi = db.Comments.Where(c => c.NewsID == nID).ToList();

                List<Yorum> yList = yorumListesi.Select(c => new Yorum
                {
                    YorumID = c.CommentID,
                    YorumMetni = c.CommentText,
                    KullaniciAdi = c.User.FirstName,
                    KullaniciSoyadi = c.User.LastName,
                    KullaniciID = c.UserID,
                    HaberID = c.NewsID
                }).ToList();

                return View(Tuple.Create<List<Yorum>, Comment, News>(yList, new Comment(), haber));
            }
            else
            {
                ViewBag.Message = "Görüntülenmek için herhangi bir haber seçilmedi.";
                News haber = new News();

                List<Yorum> yList = new List<Yorum>();


                return View(Tuple.Create<List<Yorum>, Comment, News>(yList, new Comment(), haber));
            }
        }

        //yorum ekleme viewı ajaxla
        [HttpPost]
        public JsonResult Create([Bind(Prefix = "Item2")] Comment yorum)
        {
            if (yorum.CommentText != null)
            {
                User girisYapan = (User)Session["oturum"];
                yorum.UserID = girisYapan.UserID;
                yorum.NewsID = ((News)Session["haber"]).NewsID;
                db.Comments.Add(yorum);
                db.SaveChanges();

                List<Comment> yorumListesi = db.Comments.Where(c => c.NewsID == yorum.NewsID).ToList();
                if (yorumListesi.Count == 1)
                {
                    Yorum yeni = new Yorum();
                    yeni.HaberID = ((News)Session["haber"]).NewsID;
                    yeni.KullaniciAdi = girisYapan.FirstName;
                    yeni.KullaniciSoyadi = girisYapan.LastName;
                    yeni.KullaniciID = girisYapan.UserID;
                    yeni.YorumID = yorum.CommentID;
                    yeni.YorumMetni = yorum.CommentText;

                    List<Yorum> yList = new List<Yorum>();
                    yList.Add(yeni);
                    return Json(yList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<Yorum> yList = yorumListesi.Select(c => new Yorum
                    {
                        YorumID = c.CommentID,
                        YorumMetni = c.CommentText,
                        KullaniciAdi = c.User.FirstName,
                        KullaniciSoyadi=c.User.LastName,
                        KullaniciID = c.UserID,
                        HaberID = c.NewsID
                    }).ToList();

                    return Json(yList, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                User girisYapan = (User)Session["oturum"];
                yorum.UserID = girisYapan.UserID;
                yorum.NewsID = ((News)Session["haber"]).NewsID;
                List<Comment> yorumListesi = db.Comments.Where(c => c.NewsID == yorum.NewsID).ToList();
                List<Yorum> yList = yorumListesi.Select(c => new Yorum
                {
                    YorumID = c.CommentID,
                    YorumMetni = c.CommentText,
                    KullaniciAdi = c.User.FirstName,
                    KullaniciSoyadi = c.User.LastName,
                    KullaniciID = c.UserID,
                    HaberID = c.NewsID
                }).ToList();

                return Json(yList, JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase fluResim)
        {
            if (user.Email != null && user.Password != null && user.FirstName != null && user.LastName != null)
            {
                User girisYapan = (User)Session["oturum"];
            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                    fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/userPhotos/{0}", guid)));

                    user.ImagePath = guid;
                }
                user.RoleID = 5;
                user.MembershipDate = girisYapan.MembershipDate;
                db.Entry(db.Users.Find(user.UserID)).CurrentValues.SetValues(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            return View(user);
        }

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            Session["oturum"] = null;
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public object ylist { get; set; }
    }
}
