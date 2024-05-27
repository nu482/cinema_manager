namespace Cinema_Manage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ghe")]
    public partial class Ghe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ghe()
        {
            Ves = new HashSet<Ve>();
        }

        [Key]
        [StringLength(10)]
        public string MaGhe { get; set; }

        [StringLength(10)]
        public string MaPhong { get; set; }

        [StringLength(50)]
        public string LoaiGhe { get; set; }

        [StringLength(10)]
        public string SoGhe { get; set; }

        public int GiaGhe
        {
            get
            {
                if(LoaiGhe != null)
                {
                    if (LoaiGhe.Trim() == "1")
                    {
                        return 40000;
                    }
                    else if (LoaiGhe.Trim() == "2")
                    {
                        return 60000;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
                
            }
        }


        public bool? TinhTrang { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve> Ves { get; set; }

        [ForeignKey("MaPhong")]
        public virtual PhongChieu PhongChieu { get; set; }
    }
}
