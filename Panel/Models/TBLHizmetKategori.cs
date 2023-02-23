namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLHizmetKategori")]
    public partial class TBLHizmetKategori
    {
        [Key]
        public int HKategoriID { get; set; }

        [StringLength(150)]
        public string KategoriAd { get; set; }

        [StringLength(150)]
        public string Icon { get; set; }
    }
}
