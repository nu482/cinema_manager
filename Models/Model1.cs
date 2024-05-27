using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Cinema_Manage.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Combo> Comboes { get; set; }
        public virtual DbSet<Combo_Oder> Combo_Oder { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<GheLC> GheLCs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LichChieu> LichChieux { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<Oder> Oders { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<PhongChieu> PhongChieux { get; set; }
        public virtual DbSet<SuKien> SuKiens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<Ve> Ves { get; set; }
        public virtual DbSet<NhaPhanPhoi> NhaPhanPhois { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Combo>()
                .Property(e => e.MaCombo)
                .IsFixedLength();

            //modelBuilder.Entity<Combo>()
            //    .HasMany(e => e.Combo_Oder)
            //    .WithRequired(e => e.Combo)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Combo_Oder>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<Combo_Oder>()
                .Property(e => e.OderCode)
                .IsFixedLength();

            modelBuilder.Entity<Combo_Oder>()
                .Property(e => e.MaCombo)
                .IsFixedLength();

            modelBuilder.Entity<Ghe>()
                .Property(e => e.MaGhe)
                .IsFixedLength();

            modelBuilder.Entity<Ghe>()
                .Property(e => e.MaPhong)
                .IsFixedLength();

            modelBuilder.Entity<Ghe>()
                .Property(e => e.SoGhe)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.ID)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.MaGhe)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.MaPhong)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.MaLichChieu)
                .IsFixedLength();

            modelBuilder.Entity<GheLC>()
                .Property(e => e.SoGhe)
                .IsFixedLength();

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.OderCode)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaLichChieu)
                .IsFixedLength();

            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<LichChieu>()
                .Property(e => e.MaPhongChieu)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.UID)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.VaiTro)
                .IsFixedLength();

            modelBuilder.Entity<NguoiDung>()
                .HasOptional(e => e.KhachHang)
                .WithRequired(e => e.NguoiDung);

            modelBuilder.Entity<NguoiDung>()
                .HasOptional(e => e.NhanVien)
                .WithRequired(e => e.NguoiDung);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNhanVien)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoDienThoai)
                .IsFixedLength();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsFixedLength();

            modelBuilder.Entity<Oder>()
                .Property(e => e.OderCode)
                .IsFixedLength();

            modelBuilder.Entity<Oder>()
                .Property(e => e.MaKhachHang)
                .IsFixedLength();

            modelBuilder.Entity<Oder>()
                .HasMany(e => e.Combo_Oder)
                .WithRequired(e => e.Oder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<Phim>()
                .Property(e => e.MaTheLoai)
                .IsFixedLength();

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.MaPhong)
                .IsFixedLength();

            modelBuilder.Entity<PhongChieu>()
                .Property(e => e.TenPhong)
                .IsFixedLength();;

            modelBuilder.Entity<PhongChieu>()
                .HasMany(e => e.LichChieux)
                .WithOptional(e => e.PhongChieu)
                .HasForeignKey(e => e.MaPhongChieu);

            modelBuilder.Entity<SuKien>()
                .Property(e => e.MaSuKien)
                .IsFixedLength();

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.MaTheLoai)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaVe)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.OderCode)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaPhim)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaLichChieu)
                .IsFixedLength();

            modelBuilder.Entity<Ve>()
                .Property(e => e.MaGhe)
                .IsFixedLength();

            modelBuilder.Entity<NhaPhanPhoi>()
               .Property(e => e.ID)
               .IsFixedLength();

            modelBuilder.Entity<NhaPhanPhoi>()
                .HasMany(e => e.Phims)
                .WithOptional(e => e.NhaPhanPhoi)
                .HasForeignKey(e => e.MaNPP);
        }
    }
}
