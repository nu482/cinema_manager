namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Combo_Oder
    {
        [StringLength(10)]
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        public string OderCode { get; set; }

        [Required]
        [StringLength(10)]
        public string MaCombo { get; set; }

        public int? SoLuong { get; set; }

        public virtual Combo Combo { get; set; }

        public virtual Oder Oder { get; set; }
    }
}
