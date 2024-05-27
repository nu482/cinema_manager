namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ve")]
    public partial class Ve
    {
        [Key]
        [StringLength(10)]
        public string MaVe { get; set; }

        [StringLength(10)]
        public string OderCode { get; set; }

        [StringLength(10)]
        public string MaPhim { get; set; }

        [StringLength(10)]
        public string MaLichChieu { get; set; }

        [StringLength(10)]
        public string MaGhe { get; set; }

        public int? Gia { get; set; }

        public virtual LichChieu LichChieu { get; set; }

        public virtual Oder Oder { get; set; }
    }
}
