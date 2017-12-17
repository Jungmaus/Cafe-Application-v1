using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeUygulaması.Models.ModelClass
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string Kadi { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}