using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeUygulaması.Models.ModelClass
{
    public class IndexProductModel
    {
        public string Ad { get; set; }
        public string CategoryName { get; set; }
        public string ResimYolu { get; set; }
        public int? Fiyat { get; set; }
        public int? StokAdet { get; set; }
    }
}