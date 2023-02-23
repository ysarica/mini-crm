using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panel.Models.Login
{
    public class AdminSecurity : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            PanelDB db = new PanelDB();

            if (httpContext.Session["KullaniciID"] != null && httpContext.Session["Durum"].ToString() == "1" && Convert.ToInt32(httpContext.Session["KullaniciTipID"])==1)
            {
                return true;
            }
            else
            {
                httpContext.Response.Redirect("/Giris/Index");
                return false;
            }

        }
    }
}