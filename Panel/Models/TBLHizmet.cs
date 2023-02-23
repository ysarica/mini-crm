namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLHizmet")]
    public partial class TBLHizmet
    {
        [Key]
        public int HizmetID { get; set; }

        public int? MüsteriID { get; set; }

        [StringLength(150)]
        public string MüsteriAdi { get; set; }

        [StringLength(150)]
        public string HizmetAdi { get; set; }

        [StringLength(150)]
        public string BaslangicTarihi { get; set; }

        [StringLength(150)]
        public string BitisTarihi { get; set; }

        [StringLength(150)]
        public string HizmetKategoriAd { get; set; }

        public int? HizmetKategoriID { get; set; }

        public int? TemsilciID { get; set; }

        [StringLength(150)]
        public string TemsilciAdi { get; set; }

        [StringLength(150)]
        public string FTPSunucu { get; set; }

        [StringLength(150)]
        public string FTPKullaniciAdi { get; set; }

        [StringLength(150)]
        public string FTPSifre { get; set; }

        [StringLength(150)]
        public string HizmetBedeli { get; set; }

        public string Aciklama { get; set; }

        [StringLength(150)]
        public string Firma { get; set; }

        [StringLength(50)]
        public string OdemeDurum { get; set; }

        [StringLength(50)]
        public string GorulduDurum { get; set; }

        [StringLength(50)]
        public string HizmetDurum { get; set; }

        [StringLength(150)]
        public string OdemeTarihi { get; set; }

        [StringLength(150)]
        public string HizmetBaslangicTarihi { get; set; }

        [StringLength(150)]
        public string HizmetBitisTarihi { get; set; }

        [StringLength(150)]
        public string IslemTarihi { get; set; }
    }
}
