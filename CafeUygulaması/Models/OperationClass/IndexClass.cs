using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeUygulaması.Models;
using CafeUygulaması.Models.ModelClass;

namespace CafeUygulaması.Models.OperationClass
{
    public class IndexClass
    {
        CafeDBEntities db = new CafeDBEntities();

        public List<IndexProductModel> UrunListesi()
        {
            List<IndexProductModel> lipm = new List<IndexProductModel>();
            var products = db.Products.ToList();

            foreach (var item in products)
            {
                IndexProductModel im = new IndexProductModel();
                im.Ad = item.Ad;
                im.CategoryName = db.Categories.FirstOrDefault(x => x.Id == item.CategoryID).Ad;
                im.Fiyat = item.Fiyat;
                im.ResimYolu = item.ResimYolu;
                im.StokAdet = item.StokAdet;
                lipm.Add(im);
            }

            return lipm;
        }

        public int SiparisOnayla(SiparisProductModel spm, Users u)
        {
            try
            {
                Products urun = db.Products.FirstOrDefault(x => x.Ad == spm.Ad);
                int tutar = Convert.ToInt32(urun.Fiyat) * Convert.ToInt32(spm.Adet);
                if (u.Bakiye == tutar || u.Bakiye > tutar)
                {
                    u.Bakiye = (int?)(Convert.ToInt32(u.Bakiye) - tutar);
                    db.Entry(u).State = System.Data.Entity.EntityState.Modified;
                    Orders o = new Orders();
                    o.Adet = int.Parse(spm.Adet);
                    o.Durum = 0;
                    o.ProductID = urun.Id;
                    o.Tutar = tutar;
                    o.OrderDate = DateTime.Now;
                    o.UserID = u.Id;
                    db.Orders.Add(o);
                    urun.StokAdet = Convert.ToInt32(urun.StokAdet) - int.Parse(spm.Adet);
                    db.Entry(urun).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }
        }


        public string GetAlertMessage(int m)
        {
            if (m == 1)
            {
                return "<script>alert('Siparişiniz tamamlanmıştır. Teşekkür ederiz.')</script>";
            }
            else if (m == 2)
            {
                return "<script>alert('Siparişiniz tamamlanamamıştır. Bakiyenizi kontrol ediniz.')</script>";
            }
            else if (m == 0)
            {
                return "<script>alert('Bir hata oluştu. Lütfen tekrar deneyiniz.')</script>";
            }
            else
            {
                return null;
            }
        }

    }
}