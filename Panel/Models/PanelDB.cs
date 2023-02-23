namespace Panel.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PanelDB : DbContext
    {
        public PanelDB()
            : base("name=PanelDB")
        {
        }

        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TBLDosya> TBLDosya { get; set; }
        public virtual DbSet<TBLFatura> TBLFatura { get; set; }
        public virtual DbSet<TBLHesaplar> TBLHesaplar { get; set; }
        public virtual DbSet<TBLHizmet> TBLHizmet { get; set; }
        public virtual DbSet<TBLHizmetKategori> TBLHizmetKategori { get; set; }
        public virtual DbSet<TBLHizmetMesaj> TBLHizmetMesaj { get; set; }
        public virtual DbSet<TBLIslemler> TBLIslemler { get; set; }
        public virtual DbSet<TBLKullanici> TBLKullanici { get; set; }
        public virtual DbSet<TBLKullaniciLog> TBLKullaniciLog { get; set; }
        public virtual DbSet<TBLMesaj> TBLMesaj { get; set; }
        public virtual DbSet<TBLMesajTip> TBLMesajTip { get; set; }
        public virtual DbSet<TBLNotlar> TBLNotlar { get; set; }
        public virtual DbSet<TBLOdeme> TBLOdeme { get; set; }
        public virtual DbSet<TBLOdemeBildirim> TBLOdemeBildirim { get; set; }
        public virtual DbSet<TBLOdemeYontem> TBLOdemeYontem { get; set; }
        public virtual DbSet<TBLSohbet> TBLSohbet { get; set; }
        public virtual DbSet<TBLTalep> TBLTalep { get; set; }
        public virtual DbSet<TBLTalepMesaj> TBLTalepMesaj { get; set; }
        public virtual DbSet<TBLYetki> TBLYetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
