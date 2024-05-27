using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Cinema_Manage.Models
{
    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<GheLC> GheLCs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GheLC>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.MaGhe)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.MaLichChieu)
                .IsFixedLength();
        }
    }
}
