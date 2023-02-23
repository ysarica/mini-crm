using Panel.Models;
using Panel.Models.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Panel.Controllers
{
    [PanelSecurity]
    public class PanelController : Controller
    {
        PanelDB db = new PanelDB();
        // GET: Panel
        public ActionResult Index()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            
            ViewBag.HizmetSayi = db.TBLHizmet.Where(x => x.MüsteriID == KullaniciID).Count();

            int ToplamBorc = 0;
            int HizmetMesaj = 0;
            List<TBLHizmet> hizmet = db.TBLHizmet.Where(x => x.MüsteriID == KullaniciID).ToList();
            foreach (var h in hizmet)
            {
                TBLFatura f = db.TBLFatura.Where(x => x.HizmetID == h.HizmetID).SingleOrDefault();
                ToplamBorc = ToplamBorc +Convert.ToInt32(f.KalanTutar);
                HizmetMesaj = HizmetMesaj + db.TBLHizmetMesaj.Where(x => x.HizmetID == -h.HizmetID && x.GonderenID != KullaniciID && x.Okundu == "Okunmadı").Count();


            }
            ViewBag.KalanBorc = ToplamBorc;
            ViewBag.HizmetToplamMesaj = HizmetMesaj;
            

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
            return View();
        }
        public ActionResult Bildirimler()
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
            ViewBag.Gonderilen = Gonderilen;
            ViewBag.Alinan = Alinan;
            ViewBag.ToplamOkunmamis = ToplamOkunmamis;
            return PartialView();
        }
        public ActionResult Hizmetlerim()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLHizmet> hizmet = db.TBLHizmet.Where(x => x.MüsteriID == KullaniciID).ToList();
            return View(hizmet);
        }
        public ActionResult HizmetDetay(int HizmetID)
        {
            Session["HizmetID"] = HizmetID.ToString();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLHizmet hizmet = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            ViewBag.Dosyalar = db.TBLDosya.Where(x => x.SahiplikTipi == "Hizmet" && x.SahiplikID == HizmetID).ToList();
            List<TBLHizmetMesaj> m = db.TBLHizmetMesaj.Where(x => x.HizmetID == HizmetID && x.GonderenID != KullaniciID).ToList();
            if (m.Count > 0)
            {
                foreach (var h in m)
                {
                    h.Okundu = "Görüldü";
                    db.SaveChanges();
                }
            }
            return View(hizmet);
        }
        public ActionResult _OdemeBildirimGonder()
        {
            ViewBag.Bankalar = db.TBLHesaplar.ToList();
            ViewBag.Yontem = db.TBLOdemeYontem.ToList();
            return PartialView();
        }
        [HttpPost]
        public ActionResult _OdemeBildirimGonder(TBLOdemeBildirim o)
        {
            ViewBag.Bankalar = db.TBLHesaplar.ToList();
            ViewBag.Yontem = db.TBLOdemeYontem.ToList();
            int HizmetID = Convert.ToInt32(Session["HizmetID"].ToString());
            TBLHizmet h = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            TBLOdemeBildirim ob = new TBLOdemeBildirim();
            ob.HizmetID = h.HizmetID;
            ob.MusteriID = h.MüsteriID;
            ob.OdemeYontem = o.OdemeYontem;
            ob.AdSoyad = o.AdSoyad;
            ob.OdemeBanka = o.OdemeBanka;
            ob.Durum = "Onaylanma Sürecinde";
            ob.Silindi = "Silinmedi";
            ob.Tutar = o.Tutar;
            ob.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            db.TBLOdemeBildirim.Add(ob);
            db.SaveChanges();

            Response.Redirect("/Panel/HizmetDetay?HizmetID=" + HizmetID);
            return PartialView();
        }
        public ActionResult _HizmetMesajGonder()
        {
            ViewBag.HizmetID = Convert.ToInt32(Session["HizmetID"].ToString());
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _HizmetMesajGonder(TBLHizmetMesaj m, HttpPostedFileBase attachment)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici k = db.TBLKullanici.SingleOrDefault(x => x.KullaniciID == KullaniciID);
            int HizmetID = Convert.ToInt32(Session["HizmetID"].ToString());
            if (m.Mesaj != "" && m.Mesaj != null || m.Ek != "" && m.Ek != null)
            {
                TBLHizmetMesaj mesaj = new TBLHizmetMesaj();
                mesaj.HizmetID = HizmetID;
                mesaj.Mesaj = m.Mesaj;
                mesaj.GonderenID = KullaniciID;
                mesaj.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                mesaj.Okundu = "Okunmadı";
                if (attachment != null)
                {
                    Random r = new Random();
                    string dosyaYolu = "EK" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                    var yuklemeYeri = Path.Combine(Server.MapPath("/Dosyalar/"), dosyaYolu);
                    attachment.SaveAs(yuklemeYeri);
                    mesaj.Ek = "/Dosyalar/" + dosyaYolu;

                    TBLDosya d = new TBLDosya();
                    d.DosyaYolu = "/Dosyalar/" + dosyaYolu;
                    d.DosyaTipi = Path.GetExtension(attachment.FileName);
                    d.DosyaAdi = dosyaYolu;
                    d.DosyaAciklama = "Hizmet Mesaj Dosya Gönderme -TRH:" + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
                    d.YukleyenAdi = k.AdSoyad;
                    d.YukleyenID = k.KullaniciID;
                    d.SahiplikTipi = "Hizmet";
                    d.SahiplikID = HizmetID;
                    d.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                    db.TBLDosya.Add(d);
                }
                db.TBLHizmetMesaj.Add(mesaj);
                db.SaveChanges();
            }
            Response.Redirect("/Panel/HizmetDetay?HizmetID=" + HizmetID);
            return PartialView();
        }
        public ActionResult MesajDetay(int MesajID)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            Session["MesajID"] = MesajID.ToString();
            List<TBLSohbet> sohbet = db.TBLSohbet.Where(x => x.MesajID == MesajID && x.GonderenID != KullaniciID).ToList();
            foreach (var s in sohbet)
            {
                s.Okundu = "Okundu";
            }
            db.SaveChanges();
            Session["MesajID"] = MesajID.ToString();
            return View(db.TBLMesaj.Where(x => x.MesajID == MesajID).SingleOrDefault());
        }
        public ActionResult _MesajGonder()
        {
            return PartialView();
        }

        public ActionResult _MesajYanMenu()
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
            ViewBag.Gonderilen = Gonderilen;
            ViewBag.Alinan = Alinan;
            ViewBag.ToplamOkunmamis = ToplamOkunmamis;
            return PartialView();
        }
        public ActionResult Mesajlar()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLMesaj> mesaj = db.TBLMesaj.Where(x => x.AliciID == KullaniciID && x.Silindi == null).ToList();
            return View(mesaj);
        }
        public ActionResult GonderilenMesaj()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLMesaj> mesaj = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi == null).ToList();
            return View(mesaj);
        }
        public ActionResult SilinenMesaj()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLMesaj> mesaj = db.TBLMesaj.Where(x => (x.GonderenID == KullaniciID || x.AliciID==KullaniciID) && x.Silindi == "Silindi").ToList();
            return View(mesaj);
        }
        public ActionResult MesajSil(int MesajID, string Yol)
        {
            TBLMesaj msj = db.TBLMesaj.Where(x => x.MesajID == MesajID).SingleOrDefault();
            if (msj.Silindi == "Silindi")
            {
                msj.Silindi = null;
            }
            else
            {
                msj.Silindi = "Silindi";
            }

            db.SaveChanges();
            Response.Redirect(Yol);
            return View();
        }
        public ActionResult MesajOlustur()
        {
            return View();
        }
        public ActionResult _MesajOlustur()
        {
            //uykular haram oldu gençliğim bak talanda olldu.
            ViewBag.Kime = db.TBLKullanici.Where(x => x.Durum == "1").ToList();

            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _MesajOlustur(TBLMesaj m, string compose, HttpPostedFileBase attachment)
        {
            ViewBag.Kime = db.TBLKullanici.Where(x => x.Durum == "1").ToList();

            //uykular haram oldu gençliğim bak talanda olldu.
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici gonderici = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            TBLMesaj msj = new TBLMesaj();
            msj.Konu = m.Konu;
            msj.Tarih = (DateTime.Now.ToShortDateString() + "- " + DateTime.Now.ToShortTimeString()).ToString();
            msj.GonderenAdi = gonderici.AdSoyad;
            msj.GonderenID = gonderici.KullaniciID;
            msj.AliciAdi = gonderici.TemsilciAdi;
            msj.AliciID = gonderici.TemsilciID;
            db.TBLMesaj.Add(msj);
            db.SaveChanges();
            int Sonmesaj = db.TBLMesaj.Max(x => x.MesajID);
            TBLSohbet shbt = new TBLSohbet();
            shbt.Mesaj = compose;
            shbt.Tarih = (DateTime.Now.ToShortDateString() + "- " + DateTime.Now.ToShortTimeString()).ToString();
            shbt.GonderenID = gonderici.KullaniciID;
            shbt.GonderenAd = gonderici.AdSoyad;
            shbt.AliciID = gonderici.TemsilciID;
            shbt.AliciAd = gonderici.TemsilciAdi;
            shbt.Okundu = "Okunmadı";
            shbt.MesajID = Sonmesaj;
            if (attachment != null)
            {
                Random r = new Random();
                string dosyaYolu = "EK" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Dosyalar/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                shbt.Ek = "/Dosyalar/" + dosyaYolu;
                shbt.DosyaTipi = Path.GetExtension(attachment.FileName);

                TBLDosya d = new TBLDosya();
                d.DosyaYolu = "/Dosyalar/" + dosyaYolu;
                d.DosyaTipi = Path.GetExtension(attachment.FileName);
                d.DosyaAdi = dosyaYolu;
                d.DosyaAciklama = "Sohbet Mesaj Dosya Gönderme -TRH:" + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
                d.YukleyenAdi = gonderici.AdSoyad;
                d.YukleyenID = gonderici.KullaniciID;
                d.SahiplikTipi = "Mesaj";
                d.SahiplikID = Sonmesaj;
                d.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                db.TBLDosya.Add(d);
            }
            db.TBLSohbet.Add(shbt);
            db.SaveChanges();
            Response.Redirect("/Panel/GonderilenMesaj/");
            return PartialView();
        }
        public ActionResult SohbetSil(int SohbetID, int MesajID)
        {
            TBLSohbet s = db.TBLSohbet.Where(x => x.SohbetID == SohbetID).SingleOrDefault();
            if (s.Silindi == "Silindi")
            {
                s.Silindi = null;
            }
            else
            {
                s.Silindi = "Silindi";
            }

            db.SaveChanges();
            Response.Redirect("/Panel/MesajDetay?MesajID=" + MesajID);
            return View();
        }
        public ActionResult _SohbetGonder()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _SohbetGonder(TBLSohbet m, string compose, HttpPostedFileBase attachment)
        {
            TBLSohbet shbt = new TBLSohbet();
            int MesajID = Convert.ToInt32(Session["MesajID"].ToString());
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLMesaj mesaj = db.TBLMesaj.Where(x => x.MesajID == MesajID).SingleOrDefault();
            if (mesaj.AliciID == KullaniciID)
            {
                TBLKullanici alici = db.TBLKullanici.Where(x => x.KullaniciID == mesaj.GonderenID).SingleOrDefault();
                shbt.AliciID = alici.KullaniciID;
                shbt.AliciAd = alici.AdSoyad;
            }
            else
            {
                TBLKullanici alici = db.TBLKullanici.Where(x => x.KullaniciID == mesaj.AliciID).SingleOrDefault();
                shbt.AliciID = alici.KullaniciID;
                shbt.AliciAd = alici.AdSoyad;
            }
            TBLKullanici gonderici = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();

            shbt.Mesaj = compose;
            shbt.Tarih = (DateTime.Now.ToShortDateString() + "- " + DateTime.Now.ToShortTimeString()).ToString();
            shbt.GonderenID = gonderici.KullaniciID;
            shbt.GonderenAd = gonderici.AdSoyad;

            shbt.Okundu = "Okunmadı";
            shbt.MesajID = MesajID;
            if (attachment != null)
            {
                Random r = new Random();
                string dosyaYolu = "EK" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Dosyalar/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                shbt.Ek = "/Dosyalar/" + dosyaYolu;
                shbt.DosyaTipi = Path.GetExtension(attachment.FileName);

                TBLDosya d = new TBLDosya();
                d.DosyaYolu = "/Dosyalar/" + dosyaYolu;
                d.DosyaTipi = Path.GetExtension(attachment.FileName);
                d.DosyaAdi = dosyaYolu;
                d.DosyaAciklama = "Sohbet Mesaj Dosya Gönderme -TRH:" + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
                d.YukleyenAdi = gonderici.AdSoyad;
                d.YukleyenID = gonderici.KullaniciID;
                d.SahiplikTipi = "Mesaj";
                d.SahiplikID = MesajID;
                d.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                db.TBLDosya.Add(d);
            }
            db.TBLSohbet.Add(shbt);
            db.SaveChanges();
            Response.Redirect("/Panel/MesajDetay?MesajID=" + MesajID);
            return PartialView();
        }
        public ActionResult Taleplerim()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            return View(db.TBLTalep.Where(x=> x.GonderenID==KullaniciID).ToList());
        }
        public ActionResult TalepOlustur()
        {

            ViewBag.HizmetKategori = db.TBLHizmetKategori.ToList();
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TalepOlustur(TBLTalep t, string compose)
        {
            ViewBag.HizmetKategori = db.TBLHizmetKategori.ToList();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            TBLHizmetKategori hk = db.TBLHizmetKategori.Where(x => x.HKategoriID == t.HizmetKategoriID).SingleOrDefault();
            TBLTalep tp = new TBLTalep();
            tp.GonderenAdi = k.AdSoyad;
            tp.GonderenID = k.KullaniciID;
            tp.TalepTip = "Panel Yeni Talep";
            tp.OlusturulmaTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            tp.HizmetKategoriID = hk.HKategoriID;
            tp.HizmetKategoriAdi = hk.KategoriAd;
            tp.HizmetID = null;
            tp.Talep = compose;
            tp.FirmaAdi = t.FirmaAdi;
            tp.AdSoyad = t.AdSoyad;
            tp.Telefon = t.Telefon;
            tp.Mail = t.Mail;
            tp.Okundu = "Okunmadı";
            tp.Durum = "Talep Gönderildi";
            tp.Silindi = "Silinmedi";
            db.TBLTalep.Add(tp);
            db.SaveChanges();
            int SonTalep = db.TBLTalep.Max(x => x.TalepID);
            Response.Redirect("/Panel/TalepDetay?TalepID=" + SonTalep);
            return PartialView();
        }
        public ActionResult TalepDetay(int TalepID)
        {
            Session["TalepID"] = TalepID.ToString();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            ViewBag.Dosyalar = db.TBLDosya.Where(x => x.SahiplikTipi == "Talep" && x.SahiplikID == TalepID).ToList();
            List<TBLTalepMesaj> m = db.TBLTalepMesaj.Where(x => x.TalepID == TalepID && x.GonderenID != KullaniciID).ToList();
            if (m.Count > 0)
            {
                foreach (var h in m)
                {
                    h.Okundu = "Görüldü";
                    db.SaveChanges();
                }
            }
            return View(db.TBLTalep.SingleOrDefault(x => x.TalepID == TalepID));
        }
        [HttpPost]
        public ActionResult TalepDetay(int TalepID, TBLTalep t)
        {
            return View();
        }
        public ActionResult _TalepMesajGonder()
        {
            ViewBag.TalepID = Convert.ToInt32(Session["TalepID"].ToString());
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _TalepMesajGonder(TBLTalepMesaj m, HttpPostedFileBase attachment)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici k = db.TBLKullanici.SingleOrDefault(x => x.KullaniciID == KullaniciID);
            int TalepID = Convert.ToInt32(Session["TalepID"].ToString());
            if (m.Mesaj != "" && m.Mesaj != null || m.Ek != "" && m.Ek != null)
            {
                TBLTalepMesaj mesaj = new TBLTalepMesaj();
                mesaj.TalepID = TalepID;
                mesaj.Mesaj = m.Mesaj;
                mesaj.GonderenID = KullaniciID;
                mesaj.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                mesaj.Okundu = "Okunmadı";
                if (attachment != null)
                {
                    Random r = new Random();
                    string dosyaYolu = "EK" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                    var yuklemeYeri = Path.Combine(Server.MapPath("/Dosyalar/"), dosyaYolu);
                    attachment.SaveAs(yuklemeYeri);
                    mesaj.Ek = "/Dosyalar/" + dosyaYolu;

                    TBLDosya d = new TBLDosya();
                    d.DosyaYolu = "/Dosyalar/" + dosyaYolu;
                    d.DosyaTipi = Path.GetExtension(attachment.FileName);
                    d.DosyaAdi = dosyaYolu;
                    d.DosyaAciklama = "Talep Mesaj Dosya Gönderme -TRH:" + DateTime.Now.ToShortDateString() + DateTime.Now.ToShortTimeString();
                    d.YukleyenAdi = k.AdSoyad;
                    d.YukleyenID = KullaniciID;
                    d.SahiplikTipi = "Talep";
                    d.SahiplikID = TalepID;
                    d.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                    db.TBLDosya.Add(d);
                }
                db.TBLTalepMesaj.Add(mesaj);
                db.SaveChanges();
            }
            Response.Redirect("/Panel/TalepDetay?TalepID=" + TalepID);
            return PartialView();
        }

        public ActionResult _DosyaEkle(TBLDosya d, HttpPostedFileBase attachment,string DonusYolu)
        {
            //kediler rahgat bırakmıyot
            TBLDosya dos = new TBLDosya();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            if (attachment != null)
            {
                dos.SahiplikTipi =d.SahiplikTipi;
                dos.SahiplikID = d.SahiplikID;
                
                dos.DosyaAciklama = d.DosyaAciklama;
                dos.YukleyenAdi = k.AdSoyad;
                dos.YukleyenID = k.KullaniciID;
                Random r = new Random();
                string dosyaYolu = "EK" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Dosyalar/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                dos.DosyaAdi = dosyaYolu;
                dos.DosyaYolu = "/Dosyalar/" + dosyaYolu;
                dos.DosyaTipi = Path.GetExtension(attachment.FileName);
                dos.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                db.TBLDosya.Add(dos);
                db.SaveChanges();
                Response.Redirect(DonusYolu);
            }
            return PartialView();
        }
        //Bildirimler Baslangıc
       
    }
}