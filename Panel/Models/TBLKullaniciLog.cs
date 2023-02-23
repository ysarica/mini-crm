namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLKullaniciLog")]
    public partial class TBLKullaniciLog
    {
        [Key]
        public int LogID { get; set; }

        public int? KullaniciID { get; set; }

        [StringLength(150)]
        public string KullaniciAdi { get; set; }

        public string İslem { get; set; }

        [StringLength(150)]
        public string Tarih { get; set; }
    }
}
