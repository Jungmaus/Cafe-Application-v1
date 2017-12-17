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
    public class LoginController : Controller
    {
        CafeDBEntities db = new CafeDBEntities();
        // GET: Login
        public ActionResult Login(int? m)
        {
            if(m != null && m == 1)
            {
                Response.Write("<script>alert('Bilgileriniz başarıyla güncellenmiştir.')</script>");
            }
            SessionModel sm = ControlSession();
            if (sm != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel lm)
        {
            LoginClass l = new LoginClass();
            int i = l.LoginControl(lm);
            if (i == 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                CreateSession(lm.Kadi, lm.Sifre);
                return RedirectToAction("Index", "Home");
            }
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
            return null;
        }

        private void CreateSession(string kadi, string sifre)
        {
            Session["Kadi"] = kadi;
            Session["Sifre"] = sifre;
        }

    }
}