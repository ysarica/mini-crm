namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLOdeme")]
    public partial class TBLOdeme
    {
        [Key]
        public int OdemeID { get; set; }

        public int? HizmetID { get; set; }

        public int? MusteriID { get; set; }

        public int? HizmetKategoriID { get; set; }

        public int? TemsilciID { get; set; }

        [StringLength(150)]
        public string HizmetAdi { get; set; }

        [StringLength(150)]
        public string MusteriAdi { get; set; }

        [StringLength(150)]
        public string FirmaAdi { get; set; }

        [StringLength(150)]
        public string HizmetKategoriAd { get; set; }

        [StringLength(150)]
        public string AlınanOdeme { get; set; }

        [StringLength(150)]
        public string OdemeTipi { get; set; }

        [StringLength(150)]
        public string OdemeYontem { get; set; }

        [StringLength(150)]
        public string OdemeTarihi { get; set; }
    }
}
