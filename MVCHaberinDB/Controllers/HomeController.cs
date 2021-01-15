using MVCHaberinDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHaberinDB.Controllers
{
    public class HomeController : Controller
    {
        NewsDBEntities db = new NewsDBEntities();


        public ActionResult Index()
        {
                
                return View(db.News.ToList());
        }

        public ActionResult ListNewsByCategory(int cID = 0)
        {
            if (cID == 0 || cID > db.Categories.Count()+1)
            {
                ViewBag.Message = "Görüntülenmek için herhangi bir kriter seçilmedi.";
                List<News> nList = new List<News>();

                return View(nList);
            }
            else if (cID == 3)
            {
                List<News> nList = db.News.Where(n => n.isBigNew == true).OrderByDescending(n => n.PublishDate).ToList();

                return View(nList);
            }
            else
            {
                List<News> nList = db.News.Where(n => n.CategoryID == cID).OrderByDescending(n => n.PublishDate).ToList();

                return View(nList);
            }

        }

        public ActionResult Login(User _model)
        {
            if (_model.Email != null || _model.Password != null)
            {
                if (db.Users.Any(u => u.Email == _model.Email && u.Password == _model.Password))
                {
                    User girisYapan = db.Users.Where(u => u.Email == _model.Email && u.Password == _model.Password).FirstOrDefault();
                    Session["oturum"] = girisYapan;

                    return RedirectToAction("Index","Customer");

                }

                else if (db.Directors.Any(d => d.Email == _model.Email && d.Password == _model.Password))
                {
                    Director girisYapan = db.Directors.Where(d => d.Email == _model.Email && d.Password == _model.Password).FirstOrDefault();
                    if (girisYapan.RoleID == 2)
                    {
                        Session["yonetim"] = girisYapan;
                        return RedirectToAction("Index", "Writer");
                    }
                    else if (girisYapan.RoleID == 4)
                    {
                        Session["calisan"] = girisYapan;
                    }
                    else
                    {
                        Session["ceo"] = girisYapan;
                        Session["yonetim"] = girisYapan;
                        Session["calisan"] = girisYapan;
                    }
                    
                    return RedirectToAction("Index", "News");
                }
                else
                {
                    ViewBag.Message = "E-Mail ve Şifrenizi Kontrol Edin ve Lütfen Tekrar Deneyin";
                    return View(_model);
                }
            }
            ViewBag.Message = "E-Mail & Şifre Girişi Yapınız!";
            return View();

        }

        public ActionResult SingUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SingUp(User _model, HttpPostedFileBase fluResim)
        {
            if (_model.Email != null && _model.Password != null && _model.FirstName != null && _model.LastName != null)
            {
                if (db.Users.Any(u => u.Email == _model.Email) || (db.Directors.Any(d => d.Email == _model.Email)))
                {
                    ViewBag.Message = "Farklı Bir Mail Adresi Deneyin";
                    return View(_model);
                }

                else
                {
                    if (fluResim != null)
                    {
                        string guid = Guid.NewGuid().ToString().Replace('-', '_').ToLower() + "." + fluResim.ContentType.Split('/')[1];
                        fluResim.SaveAs(Server.MapPath(string.Format("~/Content/images/userPhotos/{0}", guid)));

                        _model.ImagePath = guid;
                    }

                    _model.RoleID = 5;
                    _model.MembershipDate = DateTime.Now;
                    db.Users.Add(_model);
                    db.SaveChanges();

                    return RedirectToAction("Login");
                }
            }

            ViewBag.Message = "Gerekli Alanları(Ad & Soyad & E-Mail & Password) doldurunuz";

            return View(_model);
        }

        public ActionResult NewsDetail(int nID=0)
        {
            if (nID != 0 && db.News.Any(n => n.NewsID == nID))
            {
                News haber = db.News.FirstOrDefault(n => n.NewsID == nID);
                haber.ViewPoint++;
                db.SaveChanges();

                return View(haber);
            }
            else
            {
                ViewBag.Message = "Görüntülenmek için herhangi bir haber seçilmedi.";
                News haber = new News();

                return View(haber);
            }

        }

        public ActionResult Exit()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }


    }
}
