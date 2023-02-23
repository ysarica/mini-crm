namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLOdemeYontem")]
    public partial class TBLOdemeYontem
    {
        [Key]
        public int OYID { get; set; }

        [StringLength(150)]
        public string Yontem { get; set; }
    }
}
