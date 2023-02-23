namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLMesaj")]
    public partial class TBLMesaj
    {
        [Key]
        public int MesajID { get; set; }

        public string Konu { get; set; }

        [StringLength(150)]
        public string Tarih { get; set; }

        [StringLength(150)]
        public string GonderenAdi { get; set; }

        public int? GonderenID { get; set; }

        [StringLength(150)]
        public string AliciAdi { get; set; }

        public int? AliciID { get; set; }

        [StringLength(150)]
        public string MTipAdi { get; set; }

        public int? MTipID { get; set; }

        [StringLength(50)]
        public string Silindi { get; set; }
    }
}
