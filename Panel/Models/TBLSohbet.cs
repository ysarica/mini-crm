namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLSohbet")]
    public partial class TBLSohbet
    {
        [Key]
        public int SohbetID { get; set; }

        public int? MesajID { get; set; }

        public string Mesaj { get; set; }

        [StringLength(150)]
        public string Ek { get; set; }

        [StringLength(50)]
        public string Tarih { get; set; }

        public int? GonderenID { get; set; }

        [StringLength(150)]
        public string GonderenAd { get; set; }

        public int? AliciID { get; set; }

        [StringLength(150)]
        public string AliciAd { get; set; }

        [StringLength(50)]
        public string Okundu { get; set; }

        [StringLength(50)]
        public string Silindi { get; set; }

        [StringLength(50)]
        public string DosyaTipi { get; set; }
    }
}
