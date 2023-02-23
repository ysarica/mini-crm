using Panel.Models;
using Panel.Models.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panel.Controllers
{
    [DigerSecurity]
    public class DigerController : Controller
    {
        PanelDB db = new PanelDB();
        // GET: Diger
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profil()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            return View(db.TBLKullanici.SingleOrDefault(x=> x.KullaniciID==KullaniciID));
        }
        [HttpPost]
        public ActionResult Profil(TBLKullanici k,string nsifre)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici ku = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            ku.AdSoyad = k.AdSoyad;
            ku.Telefon = k.Telefon;
            ku.Adres = k.Adres;
            ku.Mail = k.Mail;   
            db.SaveChanges();
            return View(db.TBLKullanici.SingleOrDefault(x => x.KullaniciID == KullaniciID));
        }
        public ActionResult _ResimDegistir(HttpPostedFileBase attachment)
        {
            if (attachment != null)
            {
                int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
                TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
                Random r = new Random();
                string dosyaYolu = "RS" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Resimler/Kullanici/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                k.Resim = "/Resimler/Kullanici/" + dosyaYolu;
                db.SaveChanges();
                Session["Foto"]= "/Resimler/Kullanici/" + dosyaYolu;
            }
            Response.Redirect("/Diger/Profil/");
            return PartialView();
        }
        public ActionResult _SifreDegistir(TBLKullanici k,string npw)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici ku = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            if (ku.Sifre == k.Sifre)
            {
                ku.Sifre = npw;
                db.SaveChanges();
            }
            Response.Redirect("/Diger/Profil/");
            return PartialView();
        }
        public ActionResult _Bildirimler()
        {
            return View();
        }
        public ActionResult _MenuMesajBildirim()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            int ToplamOkunmamis = 0, Gonderilen = 0, Alinan = 0;

            List<TBLMesaj> GonderilenMesaj = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi == null).ToList();
            foreach (var gm in GonderilenMesaj)
            {
                Gonderilen=Gonderilen+db.TBLSohbet.Where(x => x.MesajID == gm.MesajID && x.GonderenID == gm.AliciID && x.Okundu == "Okunmadı").Count();
            }
            List<TBLMesaj> AlinanMesaj = db.TBLMesaj.Where(x => x.AliciID == KullaniciID && x.Silindi == null).ToList();
            foreach (var amsj in AlinanMesaj)
            {
                Alinan = Alinan + db.TBLSohbet.Where(x => x.MesajID == amsj.MesajID && x.AliciID == amsj.AliciID && x.Okundu == "Okunmadı" ).Count();
            }
            ToplamOkunmamis = Gonderilen + Alinan;
            ViewBag.Gonderilen = Gonderilen;
            ViewBag.Alinan = Alinan;
            ViewBag.ToplamOkunmamis = ToplamOkunmamis;

            return View();
        }
    }
}