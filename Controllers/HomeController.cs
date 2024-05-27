using Cinema_Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using Cinema_Manage.Models;
using PagedList;
using Antlr.Runtime.Tree;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace Cinema_Manage.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        //Index
        public ActionResult Index()
        {
            DateTime today = DateTime.Now;
            var danhSachPhim = db.Phims
                                .Where(p => p.LichChieux.Any(lc => lc.ThoiGian >= today))
                                .Take(4)
                                .ToList();
            return View(danhSachPhim);
        }



        //PHIM
        public ActionResult Phim(string searchString, int? page)
        {
            var phims = db.Phims.Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                phims = phims.Where(p => p.TenPhim.Contains(searchString));
            }
            phims = phims.OrderBy(p => p.MaPhim);
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            return View(phims.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult PhimDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        //LICH CHIEU
        public ActionResult Lich()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Lich(DateTime selectedDate)
        {
            var nextDate = selectedDate.AddDays(1);
            var bayGio = DateTime.Now;
            var danhSachPhim = db.Phims
                                 .Where(p => p.LichChieux.Any(lc => lc.ThoiGian >= selectedDate && lc.ThoiGian < nextDate && lc.ThoiGian >= bayGio))
                                 .ToList();
            if (danhSachPhim == null || !danhSachPhim.Any())
            {
                Debug.WriteLine("Danh sách phim rỗng hoặc không có phim nào được tìm thấy.");
                return View();
            }

            return View(danhSachPhim);
        }

        public ActionResult SuKien()
        {
            var sukien = db.SuKiens.Where(x => x.upbai == true).ToList();
            return View(sukien);
        }
        public ActionResult SKDetail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuKien suKien = db.SuKiens.Find(id);
            if (suKien == null)
            {
                return HttpNotFound();
            }
            return View(suKien);
        }
        //LOGIN
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = db.NguoiDungs.FirstOrDefault(u => u.Username.Trim() == username);
            if (user == null || password == null)
            {
                ModelState.AddModelError("", "Vui lòng điền đầy đủ thông tin!");
                return View();
            }
            if (user != null)
            {
                if (user.Password.Trim() == password.ToString())
                {
                    if (user.VaiTro.Trim() == "1")
                    {
                        Session["user"] = user;
                        return RedirectToAction("Index", "AdminSite", new { area = "Admin" });
                    }
                    if (user.VaiTro.Trim() == "2")
                    {
                        Session["user"] = user;
                        return RedirectToAction("Index", "AdminSite", new { area = "Admin" });
                    }
                    else if (user.VaiTro.Trim() == "4")
                    {
                        Session["user"] = user;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!");
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }


        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(NguoiDung nguoiDung)
        {
            if (string.IsNullOrWhiteSpace(nguoiDung.Username) ||
                string.IsNullOrWhiteSpace(nguoiDung.Password) ||
                string.IsNullOrWhiteSpace(nguoiDung.KhachHang.Ten) ||
                string.IsNullOrWhiteSpace(nguoiDung.KhachHang.DiaChi) ||
                string.IsNullOrWhiteSpace(nguoiDung.KhachHang.SoDienThoai))
            {
                ViewBag.ErrorMessage = "Vui lòng điền đầy đủ thông tin.";
                return View("DangKy", nguoiDung);
            }
            if (db.NguoiDungs.Any(u => u.Username == nguoiDung.Username))
            {
                ViewBag.ErrorMessage = "Username đã tồn tại.";
                return View("DangKy", nguoiDung);
            }    
            nguoiDung.VaiTro = "4";
            nguoiDung.UID = "US" + db.NguoiDungs.Count();
            db.NguoiDungs.Add(nguoiDung);
            db.SaveChanges();
            KhachHang khachHang = new KhachHang();
            khachHang.MaKhachHang = nguoiDung.UID;
            khachHang.Ten = nguoiDung.KhachHang.Ten;
            khachHang.DiaChi = nguoiDung.KhachHang.DiaChi;
            khachHang.SoDienThoai = nguoiDung.KhachHang.SoDienThoai;
            return View("Login");
        }

    }

}