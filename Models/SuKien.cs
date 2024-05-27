namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuKien")]
    public partial class SuKien
    {
        [Key]
        [StringLength(10)]
        public string MaSuKien { get; set; }

        public string TieuDe { get; set; }

        public string ChiTiet { get; set; }
        public bool? upbai {  get; set; }
    }
}
