using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema_Manage.Models;
using PagedList;

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class OdersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/Oders
        public ActionResult Index(int? page)
        {
            var oders = db.Oders.Select(p => p);

            oders = oders.OrderBy(p => p.OderCode);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(oders.ToPagedList(pageNumber, pageSize)); ;
        }
        // GET: Admin/Oders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            var lc = db.LichChieux.ToList();
            ViewBag.lich = lc;
            var bapNuoc = db.Comboes.ToList();
            ViewBag.Combos = bapNuoc;
            if (oder == null)
            {
                return HttpNotFound();
            }
            return View(oder);
        }

        // GET: Admin/Oders/Create
        public ActionResult Create()
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ten");
            return View();
        }

        // POST: Admin/Oders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   

        // GET: Admin/Oders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            if (oder == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ten", oder.MaKhachHang);
            return View(oder);
        }

        // POST: Admin/Oders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OderCode,MaKhachHang,ThoiGian,TongTien,TrangThai")] Oder oder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhachHang = new SelectList(db.KhachHangs, "MaKhachHang", "Ten", oder.MaKhachHang);
            return View(oder);
        }

        // GET: Admin/Oders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oder oder = db.Oders.Find(id);
            if (oder == null)
            {
                return HttpNotFound();
            }
            return View(oder);
        }

        // POST: Admin/Oders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Oder oder = db.Oders.Find(id);
            var cb = db.Combo_Oder.Where(b => b.OderCode.Trim() == id.Trim()).ToList();
            foreach (var cbItem in cb) 
            {
                db.Combo_Oder.Remove(cbItem);
            }          
            var ghe = db.GheLCs.Where(g => g.OderCode.Trim() == id.Trim()).ToList();
            foreach(var gheItem in ghe)
            {
                gheItem.OderCode = null;
                gheItem.TinhTrang = false;
                db.Entry(gheItem).State = EntityState.Modified;
            }
            db.Oders.Remove(oder);
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
