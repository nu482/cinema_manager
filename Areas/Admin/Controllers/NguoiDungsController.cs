using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Cinema_Manage.Models;
using PagedList;

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class NguoiDungsController : Controller
    {
        private Model1 db = new Model1();

        // GET: NguoiDungs
        public ActionResult Index(int? page)
        {
            var users = db.NguoiDungs.Select(p => p);

            users = users.OrderBy(p => p.UID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize)); ;
        }

        // GET: NguoiDungs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Create
        public ActionResult Create()
        {
            ViewBag.UID = new SelectList(db.KhachHangs, "MaKhachHang", "Ten");
            ViewBag.UID = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NguoiDung nguoiDung)
        {
            if (nguoiDung.Username == null || nguoiDung.Password == null || nguoiDung.VaiTro == null)
                ModelState.AddModelError("", "Điền đầy đủ thông tin");
            else
            {
                var us = db.NguoiDungs.Where(x => x.Username.Trim() == nguoiDung.Username.Trim());
                if (us != null)
                    ModelState.AddModelError("", "Username tồn tại!");
                else
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            nguoiDung.UID = "US0" + db.KhachHangs.Count();
                            db.NguoiDungs.Add(nguoiDung);

                            if (nguoiDung.VaiTro == "4")
                            {
                                KhachHang khachHang = new KhachHang();
                                khachHang.MaKhachHang = nguoiDung.UID;
                                khachHang.Ten = nguoiDung.KhachHang.Ten;
                                khachHang.DiaChi = nguoiDung.KhachHang.DiaChi;
                                khachHang.SoDienThoai = nguoiDung.KhachHang.SoDienThoai;
                            }
                            else
                            {
                                NhanVien nhanVien = new NhanVien();
                                nhanVien.MaNhanVien = nguoiDung.UID;
                                nhanVien.TenNhanVien = nguoiDung.NhanVien.TenNhanVien;
                                nhanVien.Email = nguoiDung.NhanVien.Email;
                                nhanVien.SoDienThoai = nguoiDung.NhanVien.SoDienThoai;
                            }
                            db.SaveChanges();
                            return RedirectToAction("Index");

                        }
                        catch
                        {
                            ModelState.AddModelError("", "Có lỗi khi thêm, thử lại");
                        }
                    }
                }

            }

            ViewBag.UID = new SelectList(db.KhachHangs, "MaKhachHang", "Ten", nguoiDung.UID);
            ViewBag.UID = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", nguoiDung.UID);
            return View(nguoiDung);
        }


        // GET: NguoiDungs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.UID = new SelectList(db.KhachHangs, "MaKhachHang", "Ten", nguoiDung.UID);
            ViewBag.UID = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", nguoiDung.UID);
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NguoiDung nguoiDung)
        {
            if (nguoiDung.Username == null || nguoiDung.Password == null || nguoiDung.VaiTro == null)
                ModelState.AddModelError("", "Điền đầy đủ thông tin");
            else
            {
                if (ModelState.IsValid)
                {
                    var existingUser = db.NguoiDungs.Find(nguoiDung.UID);

                    if (existingUser == null)
                    {
                        return HttpNotFound();
                    }
                    if (nguoiDung.VaiTro == "4")
                    {
                        var khachHang = existingUser.KhachHang ?? new KhachHang();
                        khachHang.MaKhachHang = nguoiDung.UID;
                        khachHang.Ten = nguoiDung.KhachHang.Ten;
                        khachHang.DiaChi = nguoiDung.KhachHang.DiaChi;
                        khachHang.SoDienThoai = nguoiDung.KhachHang.SoDienThoai;

                        if (existingUser.KhachHang == null)
                        {
                            db.KhachHangs.Add(khachHang);
                        }
                        else
                        {
                            db.Entry(existingUser.KhachHang).CurrentValues.SetValues(khachHang);
                        }
                    }
                    else
                    {
                        var nhanVien = existingUser.NhanVien ?? new NhanVien();
                        nhanVien.MaNhanVien = nguoiDung.UID;
                        nhanVien.TenNhanVien = nguoiDung.NhanVien.TenNhanVien;
                        nhanVien.Email = nguoiDung.NhanVien.Email;
                        nhanVien.SoDienThoai = nguoiDung.NhanVien.SoDienThoai;

                        if (existingUser.NhanVien == null)
                        {
                            db.NhanViens.Add(nhanVien);
                        }
                        else
                        {
                            db.Entry(existingUser.NhanVien).CurrentValues.SetValues(nhanVien);
                        }
                    }

                    db.Entry(existingUser).CurrentValues.SetValues(nguoiDung);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.UID = new SelectList(db.KhachHangs, "MaKhachHang", "Ten", nguoiDung.UID);
            ViewBag.UID = new SelectList(db.NhanViens, "MaNhanVien", "TenNhanVien", nguoiDung.UID);
            return View(nguoiDung);
        }

        // GET: NguoiDungs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.Find(id);
            var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKhachHang == id);
            if (khachHang != null)
            {
                db.KhachHangs.Remove(khachHang);
            }

            var nhanVien = db.NhanViens.FirstOrDefault(nv => nv.MaNhanVien == id);
            if (nhanVien != null)
            {
                db.NhanViens.Remove(nhanVien);
            }

            db.NguoiDungs.Remove(nguoiDung);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
