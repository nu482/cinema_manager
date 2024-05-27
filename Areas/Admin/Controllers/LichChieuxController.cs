using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using Cinema_Manage.Models;
using PagedList;

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class LichChieuxController : Controller
    {
        private Model1 db = new Model1();

        // GET: LichChieux
        public ActionResult Index(int? page)
        {
            var lich = db.LichChieux.Select(p => p);

            lich = lich.OrderBy(p => p.MaLichChieu);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(lich.ToPagedList(pageNumber, pageSize));
        }

        // GET: LichChieux/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            return View(lichChieu);
        }

        // GET: LichChieux/Create
        public ActionResult Create()
        {
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim");
            ViewBag.MaPhongChieu = new SelectList(db.PhongChieux, "MaPhong", "TenPhong");
            return View();
        }

        // POST: LichChieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLichChieu,MaPhim,MaPhongChieu,ThoiGian")] LichChieu lichChieu)
        {
            lichChieu.MaLichChieu = "LC0" + db.LichChieux.Count();
            if (lichChieu.MaPhim == null || lichChieu.MaPhongChieu == null)
                ModelState.AddModelError("", "Điền đầy đủ thông tin");
            else
            {
                if (ModelState.IsValid)
                {
                    db.LichChieux.Add(lichChieu);
                    var danhSachGhePhongChieu = db.Ghes.Where(x => x.MaPhong.Trim() == lichChieu.MaPhongChieu.Trim()).ToList();
                    if(lichChieu.MaLichChieu != null)
                    foreach (var ghe in danhSachGhePhongChieu)
                    {
                        var gheMoi = new GheLC
                        {
                            ID = ghe.MaGhe.Trim() + lichChieu.MaLichChieu.Trim(),
                            MaGhe = ghe.MaGhe,
                            MaLichChieu = lichChieu.MaLichChieu,
                            MaPhong = lichChieu.MaPhongChieu,
                            TinhTrang = false,
                            LoaiGhe = ghe.LoaiGhe,
                            SoGhe = ghe.SoGhe
                        };
                        if (ghe.LoaiGhe.Trim() == "1")
                        {
                            gheMoi.GiaGhe = 40000;
                        }
                        else if (ghe.LoaiGhe.Trim() == "2")
                        {
                            gheMoi.GiaGhe = 60000;
                        }
                        db.GheLCs.Add(gheMoi);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi Thêm lịch chiếu. Vui lòng thử lại sau.");           
            }
 
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", lichChieu.MaPhim);
            ViewBag.MaPhongChieu = new SelectList(db.PhongChieux, "MaPhong", "TenPhong", lichChieu.MaPhongChieu);
            return View(lichChieu);
        }

        // GET: LichChieux/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", lichChieu.MaPhim);
            ViewBag.MaPhongChieu = new SelectList(db.PhongChieux, "MaPhong", "TenPhong", lichChieu.MaPhongChieu);
            return View(lichChieu);
        }

        // POST: LichChieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLichChieu,MaPhim,MaPhongChieu,ThoiGian")] LichChieu lichChieu)
        {
            if (ModelState.IsValid)
            {
                var gheCu = db.GheLCs.Where(g => g.MaLichChieu == lichChieu.MaLichChieu);
                db.GheLCs.RemoveRange(gheCu);
                var danhSachGhePhongChieu = db.Ghes.Where(x => x.MaPhong.Trim() == lichChieu.MaPhongChieu.Trim()).ToList();
                foreach (var ghe in danhSachGhePhongChieu)
                {
                    var gheMoi = new GheLC
                    {
                        ID = ghe.MaGhe.Trim() + lichChieu.MaLichChieu.Trim(),
                        MaGhe = ghe.MaGhe,
                        MaLichChieu = lichChieu.MaLichChieu,
                        MaPhong = lichChieu.MaPhongChieu,
                        TinhTrang = false,
                        LoaiGhe = ghe.LoaiGhe,
                        SoGhe = ghe.SoGhe
                    };
                    if (ghe.LoaiGhe.Trim() == "1")
                    {
                        gheMoi.GiaGhe = 40000;
                    }
                    else if (ghe.LoaiGhe.Trim() == "2")
                    {
                        gheMoi.GiaGhe = 60000;
                    }
                    db.GheLCs.Add(gheMoi);
                }
                db.Entry(lichChieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhim = new SelectList(db.Phims, "MaPhim", "TenPhim", lichChieu.MaPhim);
            ViewBag.MaPhongChieu = new SelectList(db.PhongChieux, "MaPhong", "TenPhong", lichChieu.MaPhongChieu);
            return View(lichChieu);
        }

        // GET: LichChieux/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LichChieu lichChieu = db.LichChieux.Find(id);
            if (lichChieu == null)
            {
                return HttpNotFound();
            }
            return View(lichChieu);
        }

        // POST: LichChieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LichChieu lichChieu = db.LichChieux.Find(id);
            var ghe = db.GheLCs.Where(x => x.MaLichChieu.Trim() == lichChieu.MaLichChieu.Trim()).ToList();
            foreach (var ghes in ghe)
            {
                db.GheLCs.Remove(ghes);
            }
            db.LichChieux.Remove(lichChieu);
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
