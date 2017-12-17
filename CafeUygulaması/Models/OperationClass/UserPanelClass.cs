using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeUygulaması.Models;
using CafeUygulaması.Models.ModelClass;
using CafeUygulaması.Models.OperationClass;

namespace CafeUygulaması.Models.OperationClass
{
    public class UserPanelClass
    {
        CafeDBEntities db = new CafeDBEntities();

        public List<UserOrdersModel> GETUserOrders(string kadi, string sifre)
        {
            List<UserOrdersModel> lo = new List<UserOrdersModel>();
            Users u = db.Users.FirstOrDefault(x => x.Kadi == kadi && x.Sifre == sifre);
            var orderlist = db.Orders.Where(x => x.UserID == u.Id);
            foreach (var item in orderlist)
            {
                UserOrdersModel uom = new UserOrdersModel();
                uom.Id = item.Id;
                uom.ProductName = db.Products.FirstOrDefault(x => x.Id == item.ProductID).Ad;
                uom.Tutar = item.Tutar.ToString();
                uom.OrderDate = item.OrderDate.ToString();
                uom.Durum = item.Durum.ToString();
                uom.Adet = item.Adet.ToString();
                int? cID = db.Products.FirstOrDefault(x => x.Ad == uom.ProductName).CategoryID;
                uom.CategoryName = db.Categories.FirstOrDefault(x => x.Id == cID).Ad;
                lo.Add(uom);
            }
            return lo;
        }

        public int UseCode(string kod, Users u)
        {
            try
            {
                if (kod != null && u != null)
                {
                    Codes c = db.Codes.FirstOrDefault(x => x.Kod == kod);
                    if (c != null)
                    {
                        if (c.Statu == 0)
                        {
                            u.Bakiye = u.Bakiye + c.Deger;
                            db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                            c.UserID = u.Id;
                            c.Statu = 1;
                            db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return 1;
                        }
                    }
                }
                return 0;
            }
            catch
            {
                return 2;
            }
        }



        public int UpdateUser(UserUpdateModel uum,int? Bakiye)
        {
            try
            {
                Users u = new Users();
                u.Id = uum.Id;
                u.Ad = uum.Ad;
                u.Soyad = uum.Soyad;
                u.Kadi = uum.Kadi;
                u.Sifre = uum.Sifre;
                u.Bakiye = Bakiye;
                db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }



    }
}