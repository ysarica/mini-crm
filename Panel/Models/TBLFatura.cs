namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLFatura")]
    public partial class TBLFatura
    {
        [Key]
        public int FaturaID { get; set; }

        public int? HizmetID { get; set; }

        public int? KullaniciID { get; set; }

        [StringLength(150)]
        public string KullaniciAd { get; set; }

        [StringLength(150)]
        public string FirmaAdi { get; set; }

        [StringLength(150)]
        public string HizmetAdi { get; set; }

        [StringLength(150)]
        public string HizmetKategoriAdi { get; set; }

        [StringLength(50)]
        public string HizmetBedeli { get; set; }

        [StringLength(50)]
        public string KalanTutar { get; set; }

        [StringLength(150)]
        public string OdemeDurum { get; set; }

        [StringLength(50)]
        public string OdemeTarih { get; set; }

        [StringLength(50)]
        public string OdemeYontem { get; set; }

        [StringLength(50)]
        public string OdemeTip { get; set; }
    }
}
