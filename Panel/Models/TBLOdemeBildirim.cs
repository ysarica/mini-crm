namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLOdemeBildirim")]
    public partial class TBLOdemeBildirim
    {
        [Key]
        public int OBID { get; set; }

        public int? HizmetID { get; set; }

        public int? MusteriID { get; set; }

        [StringLength(150)]
        public string OdemeYontem { get; set; }

        [StringLength(50)]
        public string Tutar { get; set; }

        [StringLength(50)]
        public string Tarih { get; set; }

        [StringLength(150)]
        public string OdemeBanka { get; set; }

        [StringLength(50)]
        public string AdSoyad { get; set; }

        [StringLength(50)]
        public string Durum { get; set; }

        [StringLength(50)]
        public string Silindi { get; set; }
    }
}
