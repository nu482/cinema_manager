namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using static Cinema_Manage.Models.Oder;

    public partial class Oder
    {
        [Key]
        [StringLength(10)]
        public string OderCode { get; set; }

        [StringLength(10)]
        public string MaKhachHang { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Combo_Oder> Combo_Oder { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GheLC> GheLCs { get; set; }

        public bool TrangThai { get; set; }
    }
}
