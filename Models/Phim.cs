namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phim")]
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            LichChieux = new HashSet<LichChieu>();
        }

        [Key]
        [StringLength(10)]
        public string MaPhim { get; set; }

        [StringLength(100)]
        public string TenPhim { get; set; }

        [StringLength(10)]
        public string MaTheLoai { get; set; }

        [StringLength(10)]
        public string MaNPP { get; set; }

        [StringLength(100)]
        public string DaoDien { get; set; }

        public TimeSpan? ThoiLuong { get; set; }

        [StringLength(1000)]
        public string Mota { get; set; }

        [StringLength(200)]
        public string Trailer { get; set; }

        [StringLength(100)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichChieu> LichChieux { get; set; }
        public virtual NhaPhanPhoi NhaPhanPhoi { get; set; }
        public virtual TheLoai TheLoai { get; set; }
    }
}
