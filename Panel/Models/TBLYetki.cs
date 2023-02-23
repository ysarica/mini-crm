namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLYetki")]
    public partial class TBLYetki
    {
        [Key]
        public int YetkiID { get; set; }

        [StringLength(150)]
        public string YetkiAd { get; set; }

        [StringLength(150)]
        public string YetkiIcon { get; set; }
    }
}
