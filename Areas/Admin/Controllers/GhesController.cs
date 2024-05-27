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
    public class GhesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Ghes
        public ActionResult Index(int? page)
        {
            var users = db.Ghes.Select(p => p);
            users = users.OrderBy(p => p.MaGhe);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Ghes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // GET: Ghes/Create
        public ActionResult Create()
        {

            ViewBag.PhongChieuList = new SelectList(db.PhongChieux, "MaPhong", "TenPhong");
            return View();
        }

        // POST: Ghes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGhe,MaPhong,LoaiGhe,SoGhe,GiaGhe,TinhTrang")] Ghe ghe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ghe.TinhTrang == null)
                    {
                        ghe.TinhTrang = false;
                    }
                    int soLuongGhe = db.Ghes.Count() + 1;
                    if (soLuongGhe < 10)
                        ghe.MaGhe = "G00" + soLuongGhe;
                    else if (soLuongGhe < 100)
                        ghe.MaGhe = "G0" + soLuongGhe;
                    else
                        ghe.MaGhe = "G" + soLuongGhe;
                    db.Ghes.Add(ghe);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm Ghế. Vui lòng thử lại sau.");
                }

            }
            ViewBag.PhongChieuList = new SelectList(db.PhongChieux, "MaPhong", "TenPhong");
            return View(ghe);

        }

        // GET: Ghes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            ViewBag.PhongChieuList = new SelectList(db.PhongChieux, "MaPhong", "TenPhong");
            return View(ghe);
        }

        // POST: Ghes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGhe,MaPhong,LoaiGhe,SoGhe,GiaGhe,TinhTrang")] Ghe ghe)
        {
            if (ghe.MaPhong == null || ghe.LoaiGhe == null || ghe.SoGhe == null || ghe.GiaGhe == null)
                ModelState.AddModelError("", "Điền đầy đủ thông tin.");
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ghe).State = EntityState.Modified;
                    db.SaveChanges();
                    var danhSachGhePhongChieu = db.GheLCs.Where(x => x.MaGhe.Trim() == ghe.MaGhe.Trim()).ToList();
                    foreach (var gheLC in danhSachGhePhongChieu)
                    {
                        gheLC.MaGhe = ghe.MaGhe;
                        gheLC.MaPhong = ghe.MaPhong;
                        gheLC.LoaiGhe = ghe.LoaiGhe;
                        gheLC.SoGhe = ghe.SoGhe;
                        if (ghe.LoaiGhe.Trim() == "1")
                        {
                            gheLC.GiaGhe = 40000;
                        }
                        else if (ghe.LoaiGhe.Trim() == "2")
                        {
                            gheLC.GiaGhe = 60000;
                        }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi sửa Ghế. Vui lòng thử lại sau.");
            }



            ViewBag.PhongChieuList = new SelectList(db.PhongChieux, "MaPhong", "TenPhong");
            return View(ghe);
        }

        // GET: Ghes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ghe ghe = db.Ghes.Find(id);
            if (ghe == null)
            {
                return HttpNotFound();
            }
            return View(ghe);
        }

        // POST: Ghes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Ghe ghe = db.Ghes.Find(id);
            db.Ghes.Remove(ghe);
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
