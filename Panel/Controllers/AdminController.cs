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
    [AdminSecurity]
 
    public class AdminController : Controller
    {
        PanelDB db = new PanelDB();
        // GET: Admin  
        public ActionResult MusteriListe()
        {
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();

            return View(db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList());
        }
        public ActionResult MusteriDetay(int KullaniciID)
        {
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmet.Where(x => x.MüsteriID == KullaniciID).ToList();
            ViewBag.GonderilenMesajlar = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID).ToList();
            ViewBag.AlinanMesajlar = db.TBLMesaj.Where(x => x.AliciID == KullaniciID).ToList();
            TBLKullanici kullanici = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            Session["MusteriAdi"] = kullanici.AdSoyad.ToString();
            Session["MusteriID"] = kullanici.KullaniciID.ToString();
            return View(kullanici);
        }
        [HttpPost]
        public ActionResult MusteriDetay(int KullaniciID, TBLKullanici gk)
        {

            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            TBLKullanici temsilci = db.TBLKullanici.Where(x => x.KullaniciID == gk.TemsilciID).SingleOrDefault();
            TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            k.AdSoyad = gk.AdSoyad;
            k.TemsilciID = temsilci.KullaniciID;
            k.TemsilciAdi = temsilci.AdSoyad;
            k.Telefon = gk.Telefon;
            k.Mail = gk.Mail;
            k.Firma = gk.Firma;
            k.Adres = gk.Adres;
            k.Sifre = gk.Sifre;
            k.Aciklama = gk.Aciklama;
            k.il = gk.il;
            k.DogumTarihi = gk.DogumTarihi;
            k.ilce = gk.ilce;
            db.SaveChanges();
            return Redirect("/Admin/MusteriDetay?KullaniciID=" + KullaniciID);
        }
        public ActionResult _MusteriHizmet()
        {
            int MusteriID = Convert.ToInt32(Session["MusteriID"].ToString());
            List<TBLHizmet> hizmetler = db.TBLHizmet.Where(x => x.MüsteriID == MusteriID).ToList();
            return PartialView(hizmetler);
        }
        public ActionResult MusteriEkle()
        {
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult MusteriEkle(TBLKullanici y)
        {
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            TBLKullanici kontrol = db.TBLKullanici.Where(x => x.Mail == y.Mail).SingleOrDefault();

            if (kontrol == null)
            {
                TBLKullanici temsilci = db.TBLKullanici.Where(x => x.KullaniciID == y.TemsilciID).SingleOrDefault();
                TBLKullanici yön = new TBLKullanici();
                yön.AdSoyad = y.AdSoyad;
                yön.TemsilciAdi = temsilci.AdSoyad;
                yön.TemsilciID = temsilci.KullaniciID;
                yön.KullaniciTip = "Müşteri";
                yön.KullaniciTipID = 0;
                yön.Telefon = y.Telefon;
                yön.Mail = y.Mail;
                yön.Adres = y.Adres;
                yön.Firma = y.Firma;
                yön.Resim = "/Resimler/Kullanici/resim-yok.jpg";
                yön.Sifre = y.Sifre;
                yön.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                yön.Aciklama = y.Aciklama;
                yön.Durum = "1";
                yön.il = y.il;
                yön.ilce = y.ilce;
                yön.DogumTarihi = y.DogumTarihi;
                db.TBLKullanici.Add(yön);
                db.SaveChanges();
                return Redirect("/Admin/MusteriListe");
            }
            else
            {
                ViewBag.KHata = 1;
                ModelState.AddModelError("Mail", "Mail Daha Önce Kullanılmış");
            }
            return View();

        }
        public ActionResult MusteriPasif(int KullaniciID)
        {
            TBLKullanici kullanici = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            if (kullanici.Durum == "1")
            {
                kullanici.Durum = "0";
            }
            else
            {
                kullanici.Durum = "1";
            }
            db.SaveChanges();

            return Redirect("/Admin/MusteriDetay?KullaniciID=" + KullaniciID);
        }
        public ActionResult _HizmetEkle()
        {
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            ViewBag.MusteriAdi = Session["MusteriAdi"].ToString();
            ViewBag.MusteriID = Session["MusteriID"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult _HizmetEkle(TBLHizmet hiz)
        {
            int MusteriID = Convert.ToInt32(Session["MusteriID"]);
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            TBLKullanici musteri = db.TBLKullanici.Where(x => x.KullaniciID == MusteriID).SingleOrDefault();
            TBLKullanici temsilci = db.TBLKullanici.Where(x => x.KullaniciID == hiz.TemsilciID).SingleOrDefault();
            TBLHizmetKategori hizmet = db.TBLHizmetKategori.Where(x => x.HKategoriID == hiz.HizmetKategoriID).SingleOrDefault();
            TBLHizmet h = new TBLHizmet();
            h.MüsteriAdi = musteri.AdSoyad;
            h.MüsteriID = musteri.KullaniciID;
            h.HizmetKategoriAd = hizmet.KategoriAd;
            h.HizmetKategoriID = hizmet.HKategoriID;
            h.BaslangicTarihi = hiz.BaslangicTarihi;
            h.BitisTarihi = hiz.BitisTarihi;
            h.HizmetAdi = hiz.HizmetAdi;
            h.TemsilciAdi = temsilci.AdSoyad;
            h.TemsilciID = temsilci.KullaniciID;
            h.FTPKullaniciAdi = hiz.FTPKullaniciAdi;
            h.FTPSifre = hiz.FTPSifre;
            h.FTPSunucu = hiz.FTPSunucu;
            h.HizmetBedeli = hiz.HizmetBedeli;
            h.Aciklama = hiz.Aciklama;
            h.Firma = musteri.Firma;
            h.OdemeDurum = hiz.OdemeDurum;
            if (hiz.OdemeDurum == "Ödendi")
            {
                h.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            }
            h.GorulduDurum = "Görülmedi";
            h.HizmetDurum = "Aktif";
            h.HizmetBaslangicTarihi = hiz.HizmetBaslangicTarihi;
            h.HizmetBitisTarihi = hiz.HizmetBitisTarihi;
            h.IslemTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            db.TBLHizmet.Add(h);
            db.SaveChanges();


            return Redirect("/Admin/MusteriDetay?KullaniciID=" + MusteriID);
        }
        public ActionResult HizmetOdendi(int HizmetID)
        {
            TBLHizmet hizmet = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            hizmet.OdemeDurum = "Ödendi";
            return Redirect("/Admin/MusteriDetay?KullaniciID" + Session["MusteriID"].ToString());
        }
        public ActionResult YetkiliListele()
        {
            ViewBag.Yetki = db.TBLYetki.ToList();
            return View(db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList());
        }
        public ActionResult YetkiliDetay(int KullaniciID)
        {
            ViewBag.Yetki = db.TBLYetki.ToList();
            ViewBag.Hizmetler = db.TBLHizmet.Where(x => x.TemsilciID == KullaniciID).ToList();
            ViewBag.GonderilenMesajlar = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID).ToList();
            Session["YetkiliID"] = KullaniciID;
            return View(db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult YetkiliDetay(int KullaniciID, TBLKullanici gk)
        {
            ViewBag.Yetki = db.TBLYetki.ToList();
            TBLYetki yetki = db.TBLYetki.Where(x => x.YetkiID == gk.KullaniciTipID).SingleOrDefault();
            TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            k.AdSoyad = gk.AdSoyad;
            k.KullaniciTip = yetki.YetkiAd;
            k.KullaniciTipID = yetki.YetkiID;
            k.Telefon = gk.Telefon;
            k.Mail = gk.Mail;
            k.Adres = gk.Adres;
            k.Sifre = gk.Sifre;
            k.Aciklama = gk.Aciklama;
            k.il = gk.il;
            k.ilce = gk.ilce;
            k.DogumTarihi = gk.DogumTarihi;
            db.SaveChanges();
            return Redirect("/Admin/YetkiliDetay?KullaniciID=" + KullaniciID);
        }
        public ActionResult YoneticiEkle()
        {
            ViewBag.Yetki = db.TBLYetki.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult YoneticiEkle(TBLKullanici y)
        {
            ViewBag.Yetki = db.TBLYetki.ToList();
            TBLKullanici kontrol = db.TBLKullanici.Where(x => x.Mail == y.Mail).SingleOrDefault();

            if (kontrol == null)
            {
                TBLYetki yetki = db.TBLYetki.Where(x => x.YetkiID == y.KullaniciTipID).SingleOrDefault();
                TBLKullanici yön = new TBLKullanici();
                yön.AdSoyad = y.AdSoyad;
                yön.KullaniciTip = yetki.YetkiAd;
                yön.KullaniciTipID = yetki.YetkiID;
                yön.TemsilciAdi = "";
                yön.TemsilciID = null;
                yön.Telefon = y.Telefon;
                yön.Mail = y.Mail;
                yön.Adres = y.Adres;
                yön.Resim = "/Resimler/Kullanici/resim-yok.jpg";
                yön.Sifre = y.Sifre;
                yön.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                yön.Aciklama = y.Aciklama;
                yön.il = y.il;
                yön.Durum = "1";
                yön.ilce = y.ilce;
                yön.DogumTarihi = y.DogumTarihi;
                db.TBLKullanici.Add(yön);
                db.SaveChanges();
                ViewBag.Khata = 0;
                return Redirect("/Admin/YetkiliListele");
            }
            else
            {
                ViewBag.KHata = 1;
                ModelState.AddModelError("Mail", "Mail Daha Önce Kullanılmış");
            }
            return View();

        }
        public ActionResult HizmetListe()
        {
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            return View(db.TBLHizmet.ToList());
        }
        public ActionResult HizmetDetay(int HizmetID)
        {
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            Session["HizmetID"] = HizmetID.ToString();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLHizmetMesaj> m = db.TBLHizmetMesaj.Where(x => x.HizmetID == HizmetID && x.GonderenID != KullaniciID).ToList();
            if (m.Count > 0)
            {
                foreach (var h in m)
                {
                    h.Okundu = "Görüldü";
                    db.SaveChanges();
                }
            }
            return View(db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult HizmetDetay(TBLHizmet hiz, int HizmetID)
        {
            bool Ödendimi = false;
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            TBLKullanici musteri = db.TBLKullanici.Where(x => x.KullaniciID == hiz.MüsteriID).SingleOrDefault();
            TBLKullanici temsilci = db.TBLKullanici.Where(x => x.KullaniciID == hiz.TemsilciID).SingleOrDefault();
            TBLHizmetKategori hizmet = db.TBLHizmetKategori.Where(x => x.HKategoriID == hiz.HizmetKategoriID).SingleOrDefault();
            TBLHizmet h = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            h.MüsteriAdi = musteri.AdSoyad;
            h.MüsteriID = musteri.KullaniciID;
            h.HizmetKategoriAd = hizmet.KategoriAd;
            h.HizmetKategoriID = hizmet.HKategoriID;
            h.BaslangicTarihi = hiz.BaslangicTarihi;
            h.BitisTarihi = hiz.BitisTarihi;
            h.HizmetAdi = hiz.HizmetAdi;
            h.TemsilciAdi = temsilci.AdSoyad;
            h.TemsilciID = temsilci.KullaniciID;
            h.FTPKullaniciAdi = hiz.FTPKullaniciAdi;
            h.FTPSifre = hiz.FTPSifre;
            h.HizmetBitisTarihi = hiz.HizmetBitisTarihi;
            h.FTPSunucu = hiz.FTPSunucu;
            h.HizmetBedeli = hiz.HizmetBedeli;
            h.Aciklama = hiz.Aciklama;
            h.Firma = musteri.Firma;
            h.OdemeDurum = hiz.OdemeDurum;
            if (hiz.OdemeDurum == "Ödendi")
            {
                h.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                Ödendimi = true;
            }
            else
            {
                h.OdemeTarihi = "";
                Ödendimi = false;
            }
            h.HizmetBaslangicTarihi = hiz.HizmetBaslangicTarihi;
            h.HizmetBitisTarihi = hiz.HizmetBitisTarihi;
            db.SaveChanges();

            int SonHizmetID = db.TBLHizmet.Max(x => x.HizmetID);
            TBLFatura FaturaOlustur = db.TBLFatura.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            FaturaOlustur.HizmetID = HizmetID;
            FaturaOlustur.KullaniciID = musteri.KullaniciID;
            FaturaOlustur.KullaniciAd = musteri.AdSoyad;
            FaturaOlustur.FirmaAdi = musteri.Firma;
            FaturaOlustur.HizmetAdi = hiz.HizmetAdi;
            FaturaOlustur.HizmetKategoriAdi = hizmet.KategoriAd;
            FaturaOlustur.HizmetBedeli = hiz.HizmetBedeli;
            if (Ödendimi == true)
            {
                FaturaOlustur.KalanTutar = "0";
                FaturaOlustur.OdemeDurum = "Ödendi";
                FaturaOlustur.OdemeTarih = (DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString()).ToString();
                FaturaOlustur.OdemeYontem = "Belirtilmedi";
                FaturaOlustur.OdemeTip = "Belirtilmedi";
                TBLOdeme OdemeOlustur = new TBLOdeme();
                OdemeOlustur.HizmetID = HizmetID;
                OdemeOlustur.MusteriID = musteri.KullaniciID;
                OdemeOlustur.HizmetKategoriID = hizmet.HKategoriID;
                OdemeOlustur.HizmetAdi = hiz.HizmetAdi;
                OdemeOlustur.MusteriAdi = musteri.AdSoyad;
                OdemeOlustur.FirmaAdi = musteri.Firma;
                OdemeOlustur.HizmetKategoriAd = hizmet.KategoriAd;
                OdemeOlustur.AlınanOdeme = hiz.HizmetBedeli;
                OdemeOlustur.OdemeTipi = "Belirtilmedi";
                OdemeOlustur.OdemeYontem = "Belirtilmedi";
                OdemeOlustur.OdemeTarihi = (DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString()).ToString();

            }
            else
            {
                FaturaOlustur.KalanTutar = hiz.HizmetBedeli;
                FaturaOlustur.OdemeDurum = "Ödenmedi";
                FaturaOlustur.OdemeTarih = "";
                FaturaOlustur.OdemeYontem = "";
                FaturaOlustur.OdemeTip = "";
                db.SaveChanges();
                List<TBLOdeme> OdemeSil = db.TBLOdeme.Where(x => x.HizmetID == HizmetID).ToList();
                foreach (var item in OdemeSil)
                {
                    TBLOdeme OdemeSilİslem = db.TBLOdeme.Where(x => x.OdemeID == item.OdemeID).SingleOrDefault();
                    db.TBLOdeme.Remove(OdemeSilİslem);
                    db.SaveChanges();
                }

            }
            db.SaveChanges();

            return Redirect("/Admin/HizmetDetay?HizmetID=" + HizmetID);
        }
        public ActionResult HizmetEkle()
        {
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult HizmetEkle(TBLHizmet hiz)
        {
            bool Ödendimi = false;
            ViewBag.Musteriler = db.TBLKullanici.Where(x => x.KullaniciTipID == 0).ToList();
            ViewBag.Hizmetler = db.TBLHizmetKategori.ToList();
            ViewBag.Temsilci = db.TBLKullanici.Where(x => x.KullaniciTipID != 0).ToList();
            TBLKullanici musteri = db.TBLKullanici.Where(x => x.KullaniciID == hiz.MüsteriID).SingleOrDefault();
            TBLKullanici temsilci = db.TBLKullanici.Where(x => x.KullaniciID == hiz.TemsilciID).SingleOrDefault();
            TBLHizmetKategori hizmet = db.TBLHizmetKategori.Where(x => x.HKategoriID == hiz.HizmetKategoriID).SingleOrDefault();
            TBLHizmet h = new TBLHizmet();
            h.MüsteriAdi = musteri.AdSoyad;
            h.MüsteriID = musteri.KullaniciID;
            h.HizmetKategoriAd = hizmet.KategoriAd;
            h.HizmetKategoriID = hizmet.HKategoriID;
            h.BaslangicTarihi = hiz.BaslangicTarihi;
            h.BitisTarihi = hiz.BitisTarihi;
            h.HizmetAdi = hiz.HizmetAdi;
            h.TemsilciAdi = temsilci.AdSoyad;
            h.TemsilciID = temsilci.KullaniciID;
            h.FTPKullaniciAdi = hiz.FTPKullaniciAdi;
            h.FTPSifre = hiz.FTPSifre;
            h.FTPSunucu = hiz.FTPSunucu;
            h.HizmetBedeli = hiz.HizmetBedeli;
            h.Aciklama = hiz.Aciklama;
            h.Firma = musteri.Firma;
            h.OdemeDurum = hiz.OdemeDurum;
            if (hiz.OdemeDurum == "Ödendi")
            {
                h.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                Ödendimi = true;
            }
            h.GorulduDurum = "Görülmedi";
            h.HizmetDurum = "Aktif";
            h.HizmetBaslangicTarihi = hiz.HizmetBaslangicTarihi;
            h.HizmetBitisTarihi = hiz.HizmetBitisTarihi;
            h.IslemTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            db.TBLHizmet.Add(h);



            db.SaveChanges();
            int SonHizmetID = db.TBLHizmet.Max(x => x.HizmetID);
            TBLFatura FaturaOlustur = new TBLFatura();
            FaturaOlustur.HizmetID = SonHizmetID;
            FaturaOlustur.KullaniciID = musteri.KullaniciID;
            FaturaOlustur.KullaniciAd = musteri.AdSoyad;
            FaturaOlustur.FirmaAdi = musteri.Firma;
            FaturaOlustur.HizmetAdi = hiz.HizmetAdi;
            FaturaOlustur.HizmetKategoriAdi = hizmet.KategoriAd;
            FaturaOlustur.HizmetBedeli = hiz.HizmetBedeli;
            if (Ödendimi == true)
            {
                FaturaOlustur.KalanTutar = "0";
                FaturaOlustur.OdemeDurum = "Ödendi";
                FaturaOlustur.OdemeTarih = (DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString()).ToString();
                FaturaOlustur.OdemeYontem = "Belirtilmedi";
                FaturaOlustur.OdemeTip = "Belirtilmedi";
                TBLOdeme OdemeOlustur = new TBLOdeme();
                OdemeOlustur.HizmetID = SonHizmetID;
                OdemeOlustur.MusteriID = musteri.KullaniciID;
                OdemeOlustur.HizmetKategoriID = hizmet.HKategoriID;
                OdemeOlustur.HizmetAdi = hiz.HizmetAdi;
                OdemeOlustur.MusteriAdi = musteri.AdSoyad;
                OdemeOlustur.FirmaAdi = musteri.Firma;
                OdemeOlustur.HizmetKategoriAd = hizmet.KategoriAd;
                OdemeOlustur.AlınanOdeme = hiz.HizmetBedeli;
                OdemeOlustur.OdemeTipi = "Belirtilmedi";
                OdemeOlustur.OdemeYontem = "Belirtilmedi";
                OdemeOlustur.OdemeTarihi = (DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString()).ToString();
                db.TBLOdeme.Add(OdemeOlustur);
                db.SaveChanges();
            }
            else
            {
                FaturaOlustur.KalanTutar = hiz.HizmetBedeli;
                FaturaOlustur.OdemeDurum = "Ödenmedi";



            }
            db.TBLFatura.Add(FaturaOlustur);
            db.SaveChanges();

            return Redirect("/Admin/HizmetListe");
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
                }
                db.TBLHizmetMesaj.Add(mesaj);
                db.SaveChanges();
            }
            Response.Redirect("/Admin/HizmetDetay?HizmetID=" + HizmetID);
            return PartialView();
        }


       
        public ActionResult _OdemeEkle(TBLOdeme o)
        {
            //yorgunluktan ve bitkinlikten pc den kalkıyorum burada ödemeleri ekleyecegim bölüm hadi iyi geceler yada sabahlar
            int HizmetID = Convert.ToInt32(Session["HizmetID"]);
            TBLOdeme odeme = new TBLOdeme();
            TBLHizmet hizmet = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            TBLFatura fatura = db.TBLFatura.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
            odeme.HizmetID = hizmet.HizmetID;
            odeme.MusteriID = hizmet.MüsteriID;
            odeme.HizmetKategoriID = hizmet.HizmetKategoriID;
            odeme.TemsilciID = hizmet.TemsilciID;
            odeme.HizmetAdi = hizmet.HizmetAdi;
            odeme.MusteriAdi = hizmet.MüsteriAdi;
            odeme.FirmaAdi = hizmet.Firma;
            odeme.HizmetKategoriAd = hizmet.HizmetKategoriAd;
            odeme.OdemeYontem = o.OdemeYontem;
            odeme.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            fatura.OdemeTarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            hizmet.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
            if (Convert.ToInt32(o.AlınanOdeme) >= Convert.ToInt32(fatura.KalanTutar))
            {
                odeme.AlınanOdeme = fatura.KalanTutar;
                fatura.KalanTutar = "0";
                fatura.OdemeDurum = "Ödendi";
                hizmet.OdemeDurum = "Ödendi";
                fatura.OdemeTip = "Tamamı";
            }
            else
            {
                int KalanTutar = Convert.ToInt32(fatura.KalanTutar) - Convert.ToInt32(o.AlınanOdeme);
                fatura.KalanTutar = KalanTutar.ToString();
                odeme.AlınanOdeme = o.AlınanOdeme.ToString();
                fatura.OdemeDurum = "Kısmi Ödeme";
                hizmet.OdemeDurum = "Kısmi Ödeme";
                fatura.OdemeTip = "Kısmi Ödeme";
            }
            db.TBLOdeme.Add(odeme);
            db.SaveChanges();

            //Ödeme Alnıyor Kalan Tutar Belirleniyor Fazla Girlirse Alınan Eşitleniyor Kalan Tutara ve Hata Payı Sıfırlanıyor
            Response.Redirect("/Admin/HizmetDetay?HizmetID=" + HizmetID);
            return PartialView();
        }
        public ActionResult BildirimdenOdemeEkle(int OBID, int HizmetID, string Tutar, string OdemeYontem, string Durum)
        {
            int AlınanOdeme = Convert.ToInt32(Tutar);
            if (Durum == "Onaylandı")
            {
                TBLOdemeBildirim odemebildirim = db.TBLOdemeBildirim.Where(x => x.OBID == OBID).SingleOrDefault();
                odemebildirim.Durum = Durum;
                db.SaveChanges();
                //odeme faturaya eklenip hizmete ve faturaya durumunu kaydetmek
                TBLHizmet hizmet = db.TBLHizmet.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
                TBLFatura fatura = db.TBLFatura.Where(x => x.HizmetID == HizmetID).SingleOrDefault();
                TBLOdeme odeme = new TBLOdeme();
                odeme.HizmetID = hizmet.HizmetID;
                odeme.MusteriID = hizmet.MüsteriID;
                odeme.HizmetKategoriID = hizmet.HizmetKategoriID;
                odeme.TemsilciID = hizmet.TemsilciID;
                odeme.HizmetAdi = hizmet.HizmetAdi;
                odeme.MusteriAdi = hizmet.MüsteriAdi;
                odeme.FirmaAdi = hizmet.Firma;
                odeme.HizmetKategoriAd = hizmet.HizmetKategoriAd;
                odeme.OdemeYontem = OdemeYontem;
                odeme.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                fatura.OdemeTarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                hizmet.OdemeTarihi = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                if (AlınanOdeme >= Convert.ToInt32(fatura.KalanTutar))
                {
                    odeme.AlınanOdeme = fatura.KalanTutar;
                    fatura.KalanTutar = "0";
                    fatura.OdemeDurum = "Ödendi";
                    hizmet.OdemeDurum = "Ödendi";
                    fatura.OdemeTip = "Tamamı";
                }
                else
                {
                    int KalanTutar = Convert.ToInt32(fatura.KalanTutar) - AlınanOdeme;
                    fatura.KalanTutar = KalanTutar.ToString();
                    odeme.AlınanOdeme = AlınanOdeme.ToString();
                    fatura.OdemeDurum = "Kısmi Ödeme";
                    hizmet.OdemeDurum = "Kısmi Ödeme";
                    fatura.OdemeTip = "Kısmi Ödeme";
                }
                db.TBLOdeme.Add(odeme);
                db.SaveChanges();

            }
            else if (Durum == "Reddedildi")
            {
                TBLOdemeBildirim odemebildirim = db.TBLOdemeBildirim.Where(x => x.OBID == OBID).SingleOrDefault();
                odemebildirim.Durum = Durum;
                db.SaveChanges();
            }
            else
            {
                TBLOdemeBildirim odemebildirim = db.TBLOdemeBildirim.Where(x => x.OBID == OBID).SingleOrDefault();
                odemebildirim.Silindi = "Silindi";
                db.SaveChanges();
            }
            Response.Redirect("/Admin/HizmetDetay?HizmetID=" + HizmetID);
            return View();
        }
        public ActionResult _YetkiliTemsilcilikleri()
        {
            int YetkiliID = Convert.ToInt32(Session["YetkiliID"].ToString());
            return PartialView(db.TBLHizmet.Where(x => x.TemsilciID == YetkiliID).ToList());
        }
        public ActionResult MesajListe()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            return View(db.TBLMesaj.Where(x => x.AliciID == KullaniciID || x.GonderenID == KullaniciID).OrderByDescending(x => x.Tarih).ToList());
        }
        public ActionResult MesajDetay(int MesajID)
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            Session["MesajID"] = MesajID.ToString();
            List<TBLSohbet> sohbet = db.TBLSohbet.Where(x => x.MesajID == MesajID && x.GonderenID!=KullaniciID).ToList();
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
            List<TBLMesaj> mesaj = db.TBLMesaj.Where(x => x.GonderenID == KullaniciID && x.Silindi==null).ToList();
            return View(mesaj);
        }
        public ActionResult SilinenMesaj()
        {
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            List<TBLMesaj> mesaj = db.TBLMesaj.Where(x => (x.GonderenID == KullaniciID || x.AliciID == KullaniciID) && x.Silindi == "Silindi").ToList();
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
            TBLKullanici alici = db.TBLKullanici.Where(x => x.KullaniciID == m.AliciID).SingleOrDefault();
            TBLMesaj msj = new TBLMesaj();
            msj.Konu = m.Konu;
            msj.Tarih = (DateTime.Now.ToShortDateString() + "- " + DateTime.Now.ToShortTimeString()).ToString();
            msj.GonderenAdi = gonderici.AdSoyad;
            msj.GonderenID = gonderici.KullaniciID;
            msj.AliciAdi = alici.AdSoyad;
            msj.AliciID = alici.KullaniciID;
            db.TBLMesaj.Add(msj);
            db.SaveChanges();
            int Sonmesaj = db.TBLMesaj.Max(x => x.MesajID);
            TBLSohbet shbt = new TBLSohbet();
            shbt.Mesaj = compose;
            shbt.Tarih = (DateTime.Now.ToShortDateString() + "- " + DateTime.Now.ToShortTimeString()).ToString();
            shbt.GonderenID = gonderici.KullaniciID;
            shbt.GonderenAd = gonderici.AdSoyad;
            shbt.AliciID = alici.KullaniciID;
            shbt.AliciAd = alici.AdSoyad;
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
            }
            db.TBLSohbet.Add(shbt);
            db.SaveChanges();
            Response.Redirect("/Admin/GonderilenMesaj/");
            return PartialView();
        }
        public ActionResult SohbetSil(int SohbetID,int MesajID)
        {
            TBLSohbet s = db.TBLSohbet.Where(x => x.SohbetID == SohbetID).SingleOrDefault();
            if (s.Silindi=="Silindi")
            {
                s.Silindi =null;
            }
            else
            {
                s.Silindi = "Silindi";
            }

            db.SaveChanges();
            Response.Redirect("/Admin/MesajDetay?MesajID="+MesajID);
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
            if (mesaj.AliciID==KullaniciID)
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
            }
            db.TBLSohbet.Add(shbt);
            db.SaveChanges();
            Response.Redirect("/Admin/MesajDetay?MesajID=" + MesajID);
            return PartialView();
        }
        public ActionResult GelenTalepler()
        {     
            return View(db.TBLTalep.OrderByDescending(x=> x.TalepID).ToList());
        }
        public ActionResult TalepDetay(int TalepID)
        {
            TBLTalep t = db.TBLTalep.Where(x => x.TalepID == TalepID).SingleOrDefault();
            t.Okundu = "Okundu";
            db.SaveChanges();
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
            return View(t);
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
            Response.Redirect("/Admin/TalepDetay?TalepID=" + TalepID);
            return PartialView();
        }

        public ActionResult _DosyaEkle(TBLDosya d, HttpPostedFileBase attachment, string DonusYolu)
        {
            //kediler rahgat bırakmıyot
            TBLDosya dos = new TBLDosya();
            int KullaniciID = Convert.ToInt32(Session["KullaniciID"].ToString());
            TBLKullanici k = db.TBLKullanici.Where(x => x.KullaniciID == KullaniciID).SingleOrDefault();
            if (attachment != null)
            {
                dos.SahiplikTipi = d.SahiplikTipi;
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

    }
}

//Uğraşlar Sonuç verecekmi bilmiyorum ama devam bakalım