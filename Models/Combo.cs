namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Combo")]
    public partial class Combo
    {
 
        [Key]
        [StringLength(10)]
        public string MaCombo { get; set; }

        [StringLength(100)]
        public string ChiTiet { get; set; }

        public int? Gia { get; set; }

    }
}
