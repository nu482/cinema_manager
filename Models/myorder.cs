using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema_Manage.Models
{
    public class myorder
    {
        public string OrderCode { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TenPhim { get; set; }
        
        public string Phong { get; set; }

        public DateTime GioChieu { get; set; }
    }
}