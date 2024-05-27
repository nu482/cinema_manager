namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GheLC")]
    public partial class GheLC
    {
        [Key]
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MaGhe { get; set; }

        [StringLength(10)]
        public string MaPhong { get; set; }

        [StringLength(10)]
        public string OderCode { get; set; }
        [Required]
        [StringLength(10)]
        public string MaLichChieu { get; set; }

        [StringLength(50)]
        public string LoaiGhe { get; set; }

        [StringLength(10)]
        public string SoGhe { get; set; }

        public int? GiaGhe { get; set; }

        public bool? TinhTrang { get; set; }

        
    }
}
