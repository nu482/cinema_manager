using Cinema_Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class AdminSiteController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            List<thongkengay> tk = new List<thongkengay>();
            var orders = db.Oders.ToList();
            var revenueByDate = orders.GroupBy(o => o.ThoiGian.Value.Date)
                                       .Select(g => new { Ngay = g.Key, TotalRevenue = g.Sum(o => o.TongTien.Value) })
                                       .ToList();
            foreach (var item in revenueByDate)
            {
                thongkengay thongke = new thongkengay
                {
                    Ngay = item.Ngay,
                    TotalRevenue = item.TotalRevenue,
                };
                tk.Add(thongke);
            }
            return View(tk);
            
        }
    }
}