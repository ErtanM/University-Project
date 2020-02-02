using GalleryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalleryApp.Data
{
    public class Database : DbContext
    {
        //kurucu fonksiyon olusturduk
        public Database() : base("DefaultConnection")
        {

        }

        //Tablolari entity framework icin tanimladik, hangi class i baz alacagini bilmesi icin
        public DbSet<Marka> Marka { get; set; }
        public DbSet <Model> Model { get; set; }
        public DbSet<ModelDetail> ModelDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
