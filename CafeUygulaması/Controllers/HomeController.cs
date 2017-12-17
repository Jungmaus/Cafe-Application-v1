using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CafeUygulaması.Models;
using CafeUygulaması.Models.OperationClass;
using CafeUygulaması.Models.ModelClass;

namespace CafeUygulaması.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CafeDBEntities db = new CafeDBEntities();


        public ActionResult Index(int? m)
        {
            IndexClass ic = new IndexClass();
            if (m != null)
            {
                string msg = ic.GetAlertMessage(Convert.ToInt32(m));
                Response.Write(msg);
            }

            SessionModel sm = ControlSession();
            if (sm != null)
            {
                string adsoyad = GetNameSurnameBakiye();
                if (adsoyad != null)
                {
                    Response.Write("<label style='color:red;'>Hoşgeldiniz; " + adsoyad + " </label>");
                }
                List<IndexProductModel> lipm = ic.UrunListesi();
                return View(lipm);
            }
                return RedirectToAction("Login");
        }


        public ActionResult SiparisOnayla(SiparisProductModel spm)
        {
            IndexClass ic = new IndexClass();
            if (spm != null)
            {
                SessionModel sm = ControlSession();
                if (sm != null)
                {
                    Users u = db.Users.FirstOrDefault(x => x.Kadi == sm.Kadi && x.Sifre == sm.Sifre);
                    if (u != null)
                    {
                        int a = ic.SiparisOnayla(spm, u);
                        if (a == 1)
                            return new RedirectResult(@"~/Home/Index?m=1");
                        else if (a == 2)
                            return new RedirectResult(@"~/Home/Index?m=2");
                        else if (a == 0)
                            return new RedirectResult(@"~/Home/Index?m=0");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
                return RedirectToAction("Index");
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

        private string GetNameSurnameBakiye()
        {
            if (Session["Kadi"] != null)
            {
                string kadi = Session["Kadi"].ToString();
                Users u = db.Users.FirstOrDefault(x => x.Kadi == kadi);
                return u.Kadi + " " + u.Sifre + "\n" + " <label style='margin-left:30px;font-size:20px;'> Bakiye : " + u.Bakiye + " TL </label>";
            }
            return null;
        }


    }
}