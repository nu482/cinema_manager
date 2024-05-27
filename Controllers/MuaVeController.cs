using Cinema_Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using Cinema_Manage.Models.Payment;
using System.Data.Odbc;

namespace Cinema_Manage.Controllers
{
    public class MuaVeController : Controller
    {
        // GET: MuaVe
        private Model1 db = new Model1();
        public ActionResult Odr(string maPhim)
        {
            var phim = db.Phims.Include("LichChieux").FirstOrDefault(x => x.MaPhim == maPhim);
            if (phim != null)
            {
                phim.LichChieux = phim.LichChieux.OrderBy(lc => lc.ThoiGian).ToList();
            }
            return View(phim);
        }
        public ActionResult ChonGhe(string maLichChieu)
        {
            if (maLichChieu == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lichChieu = db.LichChieux.Find(maLichChieu);
            var bapNuoc = db.Comboes.ToList();
            ViewBag.Combos = bapNuoc;
            return View(lichChieu);
        }

        [HttpPost]
        public ActionResult SaveOrder(OrderData orders)
        {
            Random rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            try
            {
                foreach (var seat in orders.SelectedSeats)
                {
                    // Tìm ghế trong cơ sở dữ liệu
                    var ghe = db.GheLCs.FirstOrDefault(x => x.ID.Trim() == seat.id.Trim());
                    if (ghe != null)
                    {
                        // Cập nhật OderCode
                        ghe.OderCode = orders.OrderCode;
                        if (ghe.TinhTrang == true)
                            return Json(new { success = false });
                        else
                            ghe.TinhTrang = true;
                        db.Entry(ghe).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                if (orders.SelectedCombos != null)
                    foreach (var combo in orders.SelectedCombos)
                    {
                        var cb = new Combo_Oder
                        {
                            ID = "CB" + new string(Enumerable.Repeat(chars, 4)
                                                .Select(s => s[rand.Next(s.Length)]).ToArray()),
                            MaCombo = combo.code,
                            OderCode = orders.OrderCode,
                            SoLuong = combo.quantity
                        };
                        db.Combo_Oder.Add(cb);

                    }
                var od = new Oder
                {
                    OderCode = orders.OrderCode,
                    MaKhachHang = orders.MaKhachHang,
                    TongTien = orders.TongTien,
                    ThoiGian = DateTime.Now,
                    TrangThai = false
                };
                db.Oders.Add(od);
                db.SaveChanges();
                string url = UrlPayment(2, orders.OrderCode);
                return Json(new { success = true, redirectUrl = url });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult MyTickets(string maKH)
        {
            List<myorder> orders = new List<myorder>();

            var query = db.Oders
                          .Where(x => x.MaKhachHang.Trim() == maKH.Trim())
                          .Select(o => new
                          {
                              OrderCode = o.OderCode,
                              ThoiGian = o.ThoiGian.Value,
                              TenPhim = db.LichChieux
                                          .Where(lc => lc.MaLichChieu == o.GheLCs.FirstOrDefault().MaLichChieu)
                                          .Select(lc => lc.Phim.TenPhim)
                                          .FirstOrDefault(),
                              Phong = db.LichChieux
                                          .Where(lc => lc.MaLichChieu == o.GheLCs.FirstOrDefault().MaLichChieu)
                                          .Select(lc => lc.PhongChieu.TenPhong)
                                          .FirstOrDefault(),
                              GioChieu = db.LichChieux
                                          .Where(lc => lc.MaLichChieu == o.GheLCs.FirstOrDefault().MaLichChieu)
                                          .Select(lc => lc.ThoiGian)
                                          .FirstOrDefault(),

                          })
                          .Distinct()
                          .ToList();
            foreach (var item in query)
            {
                myorder order = new myorder
                {
                    OrderCode = item.OrderCode,
                    ThoiGian = item.ThoiGian,
                    TenPhim = item.TenPhim,
                    Phong = item.Phong,
                    GioChieu = item.GioChieu
                };
                orders.Add(order);
            }
            return View(orders);
        }

        public ActionResult ChiTietOrder(string orderId)
        {
            var order = db.Oders.FirstOrDefault(o => o.OderCode == orderId);
            if (order == null)
            {
                return HttpNotFound();
            }
            var bapNuoc = db.Comboes.ToList();
            ViewBag.Combos = bapNuoc;
            return View(order);
        }

        public ActionResult vnPayreturn()
        {
            return View();
        }
        public string UrlPayment(int typePay, string OdCode)
        {
            var urlpayment = "";
            var order = db.Oders.FirstOrDefault(x => x.OderCode.Trim() == OdCode.Trim());
            var Price = (long)order.TongTien * 100;
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString());
            if (typePay == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (typePay == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (typePay == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.ThoiGian.Value.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OderCode);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OderCode.ToString());

            urlpayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return urlpayment;
        }
    }
    public class OrderData
    {
        public int TongTien { get; set; }
        public string MaKhachHang { get; set; }
        public string OrderCode { get; set; }
        public List<SelectedSeatDTO> SelectedSeats { get; set; }
        public List<SelectedComboDTO> SelectedCombos { get; set; }
    }

    public class SelectedSeatDTO
    {
        public string id { get; set; }
    }

    public class SelectedComboDTO
    {
        public string code { get; set; }
        public int quantity { get; set; }
    }
}