namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLNotlar")]
    public partial class TBLNotlar
    {
        [Key]
        public int NotID { get; set; }

        [StringLength(150)]
        public string NotAciklama { get; set; }

        [StringLength(50)]
        public string NotTür { get; set; }

        public int? KullaniciID { get; set; }
    }
}
