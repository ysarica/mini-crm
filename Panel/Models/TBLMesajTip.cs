namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLMesajTip")]
    public partial class TBLMesajTip
    {
        [Key]
        public int MTipID { get; set; }

        [StringLength(150)]
        public string MTipAdi { get; set; }
    }
}
