using Panel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Panel.Controllers
{
    public class BildirimController : Controller
    {
        PanelDB db = new PanelDB();

        // GET: Bildirim
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _MenuMesajSayisi()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            int ToplamOkunmamis = 0, Gonderilen = 0, Alinan = 0;

            List<TBLMesaj> GonderilenMesaj = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi == null).ToList();
            foreach (var gm in GonderilenMesaj)
            {
                Gonderilen = Gonderilen + db.TBLSohbet.Where(x => x.MesajID == gm.MesajID && x.GonderenID == gm.AliciID && x.Okundu == "Okunmadı").Count();
            }
            List<TBLMesaj> AlinanMesaj = db.TBLMesaj.Where(x => x.AliciID == KullaniciID && x.Silindi == null).ToList();
            foreach (var amsj in AlinanMesaj)
            {
                Alinan = Alinan + db.TBLSohbet.Where(x => x.MesajID == amsj.MesajID && x.AliciID == amsj.AliciID && x.Okundu == "Okunmadı").Count();
            }
            ToplamOkunmamis = Gonderilen + Alinan;
            ViewBag.ToplamOkunmamis = ToplamOkunmamis;
            return PartialView();
        }
        public ActionResult _MesajBildirim()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            int ToplamOkunmamis = 0, Gonderilen = 0, Alinan = 0;
        
            List<TBLMesaj> GonderilenMesaj = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi == null).ToList();
            foreach (var gm in GonderilenMesaj)
            {
                Gonderilen = Gonderilen + db.TBLSohbet.Where(x => x.MesajID == gm.MesajID && x.GonderenID == gm.AliciID && x.Okundu == "Okunmadı").Count();
            }
            List<TBLMesaj> AlinanMesaj = db.TBLMesaj.Where(x => x.AliciID == KullaniciID && x.Silindi == null).ToList();
            foreach (var amsj in AlinanMesaj)
            {
                Alinan = Alinan + db.TBLSohbet.Where(x => x.MesajID == amsj.MesajID && x.AliciID == amsj.AliciID && x.Okundu == "Okunmadı").Count();
            }    
            List<TBLMesaj> Mesajlar= db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi == null || x.AliciID == KullaniciID && x.Silindi == null).ToList();
            foreach (var m in Mesajlar)
            {
               List<TBLSohbet> s= db.TBLSohbet.Where(x => x.MesajID == m.MesajID && x.AliciID == m.AliciID && x.Okundu == "Okunmadı" || x.MesajID == m.MesajID && x.AliciID == m.AliciID && x.Okundu == "Okunmadı").ToList();

            }
            ToplamOkunmamis = Gonderilen + Alinan;
            ViewBag.ToplamOkunmamis = ToplamOkunmamis;
            return PartialView();
        }
        public ActionResult _DigerBildirim()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            int HizmetMesaj = db.TBLHizmetMesaj.Where(x => x.GonderenID != KullaniciID && x.Okundu=="Okunmadı").Count();
            int TalepMesaj = db.TBLTalepMesaj.Where(x => x.GonderenID != KullaniciID && x.Okundu == "Okunmadı").Count();
            int Toplam = HizmetMesaj + TalepMesaj;
            ViewBag.Toplam = Toplam;
            ViewBag.Hizmet = HizmetMesaj;
            ViewBag.Talep = TalepMesaj;
            return PartialView();
        }
        public ActionResult _AdminTalepBildirim()
        {
            ViewBag.AdminTalepBildirim = db.TBLTalep.Where(x => x.Okundu == "Okunmadı").Count();
            return PartialView();
        }
    }
}