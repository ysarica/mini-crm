namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLTalep")]
    public partial class TBLTalep
    {
        [Key]
        public int TalepID { get; set; }

        [StringLength(150)]
        public string GonderenAdi { get; set; }

        public int? GonderenID { get; set; }

        [StringLength(50)]
        public string TalepTip { get; set; }

        [StringLength(150)]
        public string OlusturulmaTarihi { get; set; }

        public int? HizmetKategoriID { get; set; }

        [StringLength(150)]
        public string HizmetKategoriAdi { get; set; }

        public int? HizmetID { get; set; }

        public string Talep { get; set; }

        [StringLength(150)]
        public string Logo { get; set; }

        [StringLength(150)]
        public string FirmaAdi { get; set; }

        [StringLength(150)]
        public string AdSoyad { get; set; }

        [StringLength(150)]
        public string Telefon { get; set; }

        [StringLength(150)]
        public string Mail { get; set; }

        [StringLength(50)]
        public string Okundu { get; set; }

        [StringLength(50)]
        public string Durum { get; set; }

        [StringLength(50)]
        public string Silindi { get; set; }
    }
}
