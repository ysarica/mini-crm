namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLHesaplar")]
    public partial class TBLHesaplar
    {
        [Key]
        public int HID { get; set; }

        [StringLength(150)]
        public string BankaAdi { get; set; }

        [StringLength(150)]
        public string Iban { get; set; }

        [StringLength(150)]
        public string AdSoyad { get; set; }

        [StringLength(150)]
        public string Logo { get; set; }
    }
}
