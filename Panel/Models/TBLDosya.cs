namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLDosya")]
    public partial class TBLDosya
    {
        [Key]
        public int DID { get; set; }

        [StringLength(150)]
        public string DosyaYolu { get; set; }

        [StringLength(50)]
        public string DosyaTipi { get; set; }

        [StringLength(150)]
        public string DosyaAdi { get; set; }

        public string DosyaAciklama { get; set; }

        [StringLength(150)]
        public string YukleyenAdi { get; set; }

        public int? YukleyenID { get; set; }

        [StringLength(150)]
        public string SahiplikTipi { get; set; }

        public int? SahiplikID { get; set; }
        [StringLength(50)]
        public string Tarih { get; set; }
    }
}
