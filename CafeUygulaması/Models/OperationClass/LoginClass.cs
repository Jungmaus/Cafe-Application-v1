using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CafeUygulaması.Models;
using System.Web.Mvc;

namespace CafeUygulaması.Models.OperationClass
{
    public class LoginClass
    {
        CafeDBEntities db = new CafeDBEntities();

        public int LoginControl(ModelClass.LoginModel gl)
        {
            if(gl != null)
            {
                Users u = db.Users.FirstOrDefault(x => x.Kadi == gl.Kadi && x.Sifre == gl.Sifre);
                if (u != null)
                {
                    return 1;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }



    }
}