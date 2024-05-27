using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Cinema_Manage.Models
{
    public partial class Model3 : DbContext
    {
        public Model3()
            : base("name=Model3")
        {
        }

        public virtual DbSet<LichChieu> LichChieux { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaLichChieu)
                .IsFixedLength();

            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaPhongChieu)
                .IsFixedLength();
        }
    }
}
