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

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class PhimsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Phims
        public ActionResult Index(string searchString, int? page)
        {
            var phims = db.Phims.Select(p => p);
            if (!String.IsNullOrEmpty(searchString))
            {
                phims = phims.Where(p => p.TenPhim.Contains(searchString));
            }
            phims = phims.OrderBy(p => p.MaPhim);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(phims.ToPagedList(pageNumber, pageSize));
        }

        // GET: Phims/Details/5
        public ActionResult Details(string id)
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

        // GET: Phims/Create
        public ActionResult Create()
        {
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai");
            ViewBag.MaNPP = new SelectList(db.NhaPhanPhois, "ID", "TenNPP");
            return View();
        }

        // POST: Phims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhim,TenPhim,MaTheLoai,MaNPP,DaoDien,ThoiLuong,Mota,Trailer")] Phim phim, HttpPostedFileBase HinhAnhPhim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (HinhAnhPhim != null && HinhAnhPhim.ContentLength > 0)
                    {
                        var tenTepAnh = Path.GetFileName(HinhAnhPhim.FileName);
                        var duongDanLuutru = "/Image/" + tenTepAnh;
                        var duongDanToanBo = Server.MapPath(duongDanLuutru);

                        HinhAnhPhim.SaveAs(duongDanToanBo);

                        phim.HinhAnh = duongDanLuutru;
                    }
                    db.Phims.Add(phim);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm phim. Vui lòng thử lại sau.");
                }
            }

            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            ViewBag.MaNPP = new SelectList(db.NhaPhanPhois, "ID", "TenNPP", phim.MaNPP);
            return View(phim);
        }

        // GET: Phims/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }

        // POST: Phims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhim,TenPhim,MaTheLoai,DaoDien,ThoiLuong,Mota,Trailer")] Phim phim, HttpPostedFileBase HinhAnhPhim)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (HinhAnhPhim == null || HinhAnhPhim.ContentLength <= 0)
                    {
                    }
                    else
                    {
                        var tenTepAnh = Path.GetFileName(HinhAnhPhim.FileName);
                        var duongDanLuutru = "/Image/" + tenTepAnh;
                        var duongDanToanBo = Server.MapPath(duongDanLuutru);

                        // Lưu tệp ảnh vào thư mục Image
                        HinhAnhPhim.SaveAs(duongDanToanBo);

                        // Gán đường dẫn của tệp ảnh cho thuộc tính HinhAnh của đối tượng Phim
                        phim.HinhAnh = duongDanLuutru;
                    }

                    db.Entry(phim).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm phim. Vui lòng thử lại sau.");
                }

            }
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", phim.MaTheLoai);
            return View(phim);
        }

        // GET: Phims/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Phims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var lichChieusToDelete = db.LichChieux.Where(l => l.MaPhim == id);
            db.LichChieux.RemoveRange(lichChieusToDelete);
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
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
