using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Panel.Models;

namespace Panel.Controllers
{
    public class GirisController : Controller
    {
        PanelDB db = new PanelDB();
        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TBLKullanici k)
        {
            string yol = "";
            TBLKullanici kullanici = db.TBLKullanici.Where(x => x.Mail == k.Mail && x.Sifre == k.Sifre).SingleOrDefault();
            ViewBag.Hata = "";
            if (kullanici != null)
            {
                Session["KullaniciID"] = kullanici.KullaniciID.ToString();
                Session["Foto"] = kullanici.Resim.ToString();
                Session["AdSoyad"] = kullanici.AdSoyad.ToString();
                Session["KullaniciTipID"] = kullanici.KullaniciTipID.ToString();
                Session["KullaniciTip"] = kullanici.KullaniciTip.ToString();
                Session["Durum"] = kullanici.Durum.ToString();
                if (kullanici.KullaniciTipID == 0)
                {
                     yol = "/Panel/Index/";
                }
                else if (kullanici.KullaniciTipID == 1)
                {
                    yol = "/Admin/HizmetListe/";
                }
                else
                {
                    //Müşteri Ve Yönetici Dışı Kullanıcı Yönlendirme Aktif Değil
                }
            }
            else
            {
                yol = "/Giris/Index/";
                ViewBag.Hata = "Hatalı Kullanıcı Adı Ve Şifre";
            }
            return Redirect(yol);
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            Response.Redirect("/Giris/Index");
            return View();
        }
    }
}