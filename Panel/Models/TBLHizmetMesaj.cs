namespace Panel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBLHizmetMesaj")]
    public partial class TBLHizmetMesaj
    {
        [Key]
        public int HMID { get; set; }

        public int? HizmetID { get; set; }

        public string Mesaj { get; set; }

        [StringLength(150)]
        public string Ek { get; set; }

        [StringLength(150)]
        public string Tarih { get; set; }

        [StringLength(50)]
        public string Okundu { get; set; }

        public int? GonderenID { get; set; }
    }
}
