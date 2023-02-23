namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLIslemler")]
    public partial class TBLIslemler
    {
        [Key]
        public int IslemID { get; set; }

        [StringLength(150)]
        public string IslemAdi { get; set; }

        public int? HizmetID { get; set; }

        [StringLength(150)]
        public string HarcananZaman { get; set; }

        [StringLength(150)]
        public string Fiyat { get; set; }

        [StringLength(150)]
        public string TemsilciID { get; set; }

        [StringLength(150)]
        public string TemsilciAdi { get; set; }

        [StringLength(150)]
        public string Aciklama { get; set; }
    }
}
