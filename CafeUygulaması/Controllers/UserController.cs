using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeUygulaması.Models;
using CafeUygulaması.Models.ModelClass;
using CafeUygulaması.Models.OperationClass;

namespace CafeUygulaması.Controllers
{
    public class UserController : Controller
    {
        CafeDBEntities db = new CafeDBEntities();
        UserPanelClass upc = new UserPanelClass();

        // GET: User

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bakiye()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bakiye(string Kod)
        {
            SessionModel sm = ControlSession();
            if (sm != null)
            {
                Users u = db.Users.FirstOrDefault(x => x.Kadi == sm.Kadi && x.Sifre == sm.Sifre);
                int a = upc.UseCode(Kod, u);
                if (a == 0)
                    Response.Write("<script>alert('Kod kullanılamadı. Daha önceden kullanılmış olabilir.');</script>");
                else if (a == 1)
                    Response.Write("<script>alert('Kod kullanıldı. Teşekkür ederiz.');</script>");
                else if (a == 2)
                    Response.Write("<script>alert('Bilinmeyen bir hata oluştu. Lütfen tekrar deneyiniz.');</script>");
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Settings()
        {
            SessionModel sm = ControlSession();
            if (sm != null)
            {
                Users u = db.Users.FirstOrDefault(x => x.Kadi == sm.Kadi && x.Sifre == sm.Sifre);
                return View(u);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Settings(UserUpdateModel uum)
        {
            SessionModel sm = ControlSession();
            if (sm != null)
            {
                Users u = db.Users.FirstOrDefault(x => x.Kadi == sm.Kadi && x.Sifre == sm.Sifre);
                uum.Id = u.Id;
                int a = upc.UpdateUser(uum,u.Bakiye);
                if (a == 1)
                {
                    Session.Abandon();
                    return new RedirectResult(@"~/Login/Login?m=1");
                }
                else if (a == 0)
                    Response.Write("<script>alert('Bilinmeyen bir hata oluştu. Lütfen tekrar deneyiniz')</script>");
                return View(u);
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Orders()
        {
            SessionModel sm = ControlSession();
            if (sm != null)
            {
                List<UserOrdersModel> lo = upc.GETUserOrders(sm.Kadi, sm.Sifre);
                return View(lo);
            }
            return RedirectToAction("Login", "Login");
        }


        private SessionModel ControlSession()
        {
            if (Session["Kadi"] != null && Session["Sifre"] != null)
            {
                SessionModel sm = new SessionModel();
                sm.Kadi = Session["Kadi"].ToString();
                sm.Sifre = Session["Sifre"].ToString();
                return sm;
            }
            else
            {
                return null;
            }
        }

    }
}