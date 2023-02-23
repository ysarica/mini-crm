namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLKullanici")]
    public partial class TBLKullanici
    {
        [Key]
        public int KullaniciID { get; set; }

        [StringLength(150)]
        public string AdSoyad { get; set; }

        [StringLength(150)]
        public string KullaniciTip { get; set; }

        [StringLength(150)]
        public string Telefon { get; set; }

        [StringLength(150)]
        public string Mail { get; set; }

        public string Adres { get; set; }

        [StringLength(150)]
        public string Resim { get; set; }

        [StringLength(150)]
        public string Sifre { get; set; }

        public int? KullaniciTipID { get; set; }

        [StringLength(150)]
        public string TemsilciAdi { get; set; }

        public int? TemsilciID { get; set; }

        [StringLength(50)]
        public string Tarih { get; set; }

        public string Aciklama { get; set; }

        [StringLength(150)]
        public string Firma { get; set; }

        [StringLength(50)]
        public string Durum { get; set; }

        [StringLength(150)]
        public string il { get; set; }

        [StringLength(150)]
        public string ilce { get; set; }

        [StringLength(150)]
        public string DogumTarihi { get; set; }
    }
}
