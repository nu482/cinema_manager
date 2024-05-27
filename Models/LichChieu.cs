namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichChieu")]
    public partial class LichChieu
    {
        [Key]
        [StringLength(10)]
        public string MaLichChieu { get; set; }

        [StringLength(10)]
        public string MaPhim { get; set; }

        [StringLength(10)]
        public string MaPhongChieu { get; set; }

        public DateTime ThoiGian { get; set; }

        public virtual Phim Phim { get; set; }

        public virtual PhongChieu PhongChieu { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GheLC> GheLCs { get; set; }
    }
}
