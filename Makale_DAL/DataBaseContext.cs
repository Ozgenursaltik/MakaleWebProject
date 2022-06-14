using Makale_Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace Makale_DAL
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<Kullanici> Kullanicilar { get; set; }
        public virtual DbSet<Not> Makaleler { get; set; }
        public virtual DbSet<Yorum> Yorumlar { get; set; }
        public virtual DbSet<Kategori> Kategoriler { get; set; }
        public virtual DbSet<Begeni> Begeniler { get; set; }


        public DataBaseContext()
        {
            Database.SetInitializer(new DBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)  //sql deki cascade iþleminin kodla yapýlmasý.
        {
            modelBuilder.Entity<Not>().HasMany(n => n.Yorumlar).WithRequired(c => c.Makale).WillCascadeOnDelete(true);

            modelBuilder.Entity<Not>().HasMany(n => n.Begeniler).WithRequired(c => c.Makale).WillCascadeOnDelete(true);
        }
    }

    
}