using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeUygulaması.Models.ModelClass
{
    public class UserOrdersModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Adet { get; set; }
        public string Tutar { get; set; }
        public string OrderDate { get; set; }
        public string Durum { get; set; }
    }
}